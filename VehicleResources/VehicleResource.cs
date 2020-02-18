using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleResources
{
    public class VehicleResource
    {
        public string Id { get; set; }

        public VehicleStatusResource Status { get; set; }
    }
}
