using System;
using midLibrary.Models;
using System.Collections.Generic;

namespace midLibrary
{
    public class Stores
    {
        static readonly string infoPath = "App_Data/info.csv";  
        static readonly string orderPath = "App_Data/order.csv"; 
        static readonly string categoryPath = "App_Data/category.csv"; 
        static void Main(string[] args)
        {
           
        }
         public List<Info> returnInfo (){

            var infoStore  = new InfoStore() { Path =  infoPath };
            var infoList   = infoStore.GetCollection();
            return  infoList ;
            
         }
        public List<Order> returnOrder (){

            var orderStore  = new OrderStore() { Path =  orderPath };
            var orderList   = orderStore.GetCollection();
            return  orderList;
        }
        public List<Category> returnCategory (){

            var categoryStore  = new CategoryStore() { Path =  orderPath };
            var categoryList   = categoryStore.GetCollection();
            return  categoryList;
        }
    }
}
