using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurants2GD.Models
{
    public class Restaurant
    {
        public int Restaurantkey
        { get; set; }
        public string Name
        { get; set; }

        public string Discount
        { get; set; }
    }
}