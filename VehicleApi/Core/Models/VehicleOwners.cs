using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehiclesApi.Core.Models
{
    public class VehicleOwners
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "vehicleId")]
        public string VehicleId { get; set; }

        [JsonProperty(PropertyName = "customerId")]
        public Guid CustomerId { get; set; }

        [JsonProperty(PropertyName = "registrationNumber")]
        public string RegistrationNumber { get; set; }
    }
}
