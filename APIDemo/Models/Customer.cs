using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDemo.Models
{
    public class Customer
    {
        public int customerId { get; set; }

        public string customerName { get; set; }
        public string customerAddress { get; set; }
        public string email { get; set; }
        public int phone { get; set; }
    }
}
