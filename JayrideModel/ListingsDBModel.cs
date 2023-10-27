using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JayrideModel
{
    public class ListingsDBModel
    {
        public string Name { get; set; }
        public double PricePerPassenger { get; set; }
        public VehicleTypeDBModel VehicleType { get; set; }
        public double TotalPrice { get; set; }
    }
}
