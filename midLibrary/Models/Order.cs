using System;

namespace midLibrary.Models
{
    public class Order
    {
        public int Info_id { get; set; }
        public int Order_id{ get; set; }
        public DateTime Date_ordered{ get; set; }
        public DateTime Date_should_be_delivered{ get; set; }
        public DateTime Date_actually_delivered{ get; set; }
        public string Category{ get; set; }
        public string Status{get; set;}
    }
}