using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurants2GD.Models
{
    public class Cusomer:User
    {
        public string FirstName
        { get; set; }

        public string LastName
        { get; set; }

        public string Email
        { get; set; }

        public string Mobile
        { get; set; }

        public double BalanceAmount
        { get; set; }
        public double FrozenAmount
        { get; set; }
    }
}