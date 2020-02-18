using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleResources
{
    public class VehicleOwnersResource
    {
        public string Id { get; set; }
        public string VehicleId { get; set; }
        public Guid CustomerId { get; set; }
        public string RegistrationNumber { get; set; }
    }
}
