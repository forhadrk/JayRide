using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JayrideModel
{
    public class LocationDetailsDBModel
    {
        //public string From { get; set; }
        //public string To { get; set; }
        public List<ListingsDBModel> Listings { get; set; }
    }
}
