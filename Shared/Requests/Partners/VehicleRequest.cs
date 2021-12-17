using System;

namespace Shared.Requests.Partners
{
    public class VehicleRequest
    {
        public Guid Id { get; set; }
        public Guid PartnerId { get; set; }
        public string Name { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Colour { get; set; }
        public string LicensePlate { get; set; }
        public string Description { get; set; }
        public string VINNo { get; set; }
        public DateTime LicenseExpiry { get; set; }
        public int Mileage { get; set; }
    }
}
