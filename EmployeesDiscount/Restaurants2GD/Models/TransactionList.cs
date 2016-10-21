using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurants2GD.Models
{
    public class TransactionList
    {
        public List<Transaction> Trans
        { get; set; }

        public int ReturnCount
        { get; set; }
    }
}