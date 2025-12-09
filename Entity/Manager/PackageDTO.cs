using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Manager
{
    public class PackageDTO
    {
        public Guid Id { get; set; }
        public string Carrier { get; set; } = "";
        public string TrackingNumber { get; set; } = "";
        public string Recipient { get; set; } = "";
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
