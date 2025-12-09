using DataAccess.Context;
using DataAccess.Models;
using Entity.Manager;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Management
{
    public class PackageDA(ApplicationDbContext db)
    {

        private readonly ApplicationDbContext _db = db;

        public async Task<PackageDTO?> GetByIdAsync(Guid id)
        {
            var resp = await _db.Packages.FindAsync(id);

            if (resp != null)
            {
                return new PackageDTO
                {
                    Id = resp.Id,
                    Carrier = resp.Carrier,
                    CreatedAt = resp.CreatedAt,
                    Recipient = resp.RecipientName,
                    TrackingNumber = resp.TrackingNumber,
                    Status = Enum.GetName(resp.Status) ?? ""
                };
            }

            return null;
        }

        public async Task<PackageDTO?> GetByTrackingAsync(string trackingNumber)
        {
            var res = await _db.Packages.FirstOrDefaultAsync(p => p.TrackingNumber == trackingNumber);
            return res != null ? new PackageDTO
            {
                Id = res.Id,
                Carrier = res.Carrier,
                CreatedAt = res.CreatedAt,
                Recipient = res.RecipientName,
                TrackingNumber = res.TrackingNumber,
                Status = Enum.GetName(res.Status) ?? ""
            } : null;
        }

        public async Task<List<PackageDTO>> SearchAsync(string? q, int page = 1, int pageSize = 20)
        {
            var query = _db.Packages.AsQueryable();
            if (!string.IsNullOrWhiteSpace(q))
            {
                q = q.Trim();
                query = query.Where(p => p.TrackingNumber.Contains(q) ||
                                         p.Carrier.Contains(q));
            }
            query = query.OrderByDescending(p => p.LastUpdatedAt ?? p.CreatedAt)
                         .Skip((page - 1) * pageSize)
                         .Take(pageSize);

            var res = await query.ToListAsync();

            return [.. res.Select(resp => new PackageDTO
            {
                Id = resp.Id,
                Carrier = resp.Carrier,
                CreatedAt = resp.CreatedAt,
                Recipient = resp.RecipientName,
                TrackingNumber = resp.TrackingNumber,
                Status = Enum.GetName(resp.Status) ?? ""
            })];
        }

        public async Task<Package> CreateAsync(Package p)
        {
            p.CreatedAt = DateTime.UtcNow;
            p.LastUpdatedAt = DateTime.UtcNow;
            _db.Packages.Add(p);
            await _db.SaveChangesAsync();
            return p;
        }

        public async Task<Package> UpdateAsync(Package p)
        {
            p.LastUpdatedAt = DateTime.UtcNow;
            _db.Packages.Update(p);
            await _db.SaveChangesAsync();
            return p;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _db.Packages.FindAsync(id);
            if (entity != null)
            {
                _db.Packages.Remove(entity);
                await _db.SaveChangesAsync();
            }
        }
    }
}