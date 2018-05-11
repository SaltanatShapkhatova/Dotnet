using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Models
{
    public class Inventory
    {
        public int ID { get; set; }

        public int CustomerID { get; set; }

        public bool Completed { get; set; }

        public List<Detail> Details { get; set; }
    }
}
