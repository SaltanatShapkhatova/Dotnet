using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Models
{
    public class Department
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<Product> Product { get; set; }
    }
}
