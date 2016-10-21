using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurants2GD.Models
{
    public class Transaction
    {
        public string TransactionNumber
        { get; set; }

        public string RestaurantName
        { get; set; }

        public string EmployeeName
        { get; set; }

        public string Amount
        { get; set; }

        public string CreateDate
        { get; set; }
    }
}