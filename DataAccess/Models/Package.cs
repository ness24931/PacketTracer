using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.ComponentModel.DataAnnotations;

// Models/PackageStatus.cs
public enum PackageStatus
{
    Ingresado = 0,
    EnTransito = 1,
    ParaEntregar = 2,
    Entregado = 3,
    Excepcion = 4,
    Cancelado = 5
}


namespace DataAccess.Models
{
    public class Package
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(100)]
        public string Carrier { get; set; } = ""; // e.g., "Amazon", "eBay", "UPS"

        [Required, MaxLength(100)]
        public string TrackingNumber { get; set; } = "";

        [MaxLength(200)]
        public string RecipientName { get; set; } = "";

        [MaxLength(500)]
        public string RecipientAddress { get; set; } = "";

        public PackageStatus Status { get; set; } = PackageStatus.Ingresado;

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedAt { get; set; }
    }

}
