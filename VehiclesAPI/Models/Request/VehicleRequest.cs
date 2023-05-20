namespace VehiclesAPI.Models.Request
{
    public class VehicleRequest
    {
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string ImageUrl { get; set; }
        public string displacement { get; set; }
        public string MaxSpeed { get; set; }
        public double length { get; set; }
        public double witdh { get; set; }
        public double height { get; set; }
    }
}
