using System;

namespace midterm.Models
{
    public class Report
    {
        public int Order_id{ get; set; }
        public int Order_amount  { get; set; }
        public int Late_amount {get; set;}
        public int Canceled_amount {get; set;}
        public float Success_rate{get; set;}
    }
}