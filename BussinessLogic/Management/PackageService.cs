using DataAccess.Context;
using DataAccess.Management;
using Entity.Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLogic.Management
{
    public class PackageService(PackageDA da)
    {
        private readonly PackageDA _da = da;

        public async Task<PackageDTO?> GetById(Guid id) => await _da.GetByIdAsync(id);

        public async Task<List<PackageDTO>> GetAll(string? trackId) => await _da.SearchAsync(trackId);

        public async Task<PackageDTO?> GetByTracking(string trackingNumber) => await _da.GetByTrackingAsync(trackingNumber);
    }
}
