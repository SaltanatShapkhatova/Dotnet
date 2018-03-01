using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using midterm.Models;
using midLibrary;

namespace midterm.Controllers
{
    public class OrderController : Controller
    {
        private Stores store;

        public OrderController(){
            store = new Stores ();
        }
        
        public IActionResult OrderView(){
            var orderList = store.returnOrder();
            return View(orderList);
        }

        [HttpGet]
         public IActionResult OrderTransport()
        {
            var res = new List<Input>();
            var val = new Input ()
            {
                BookId = 1,
                BookName = "War and Peace",
                PersonId = 0,
                BorrowDate = DateTime.Now
            };
            res.Add(val);
            return View(res);
        }

        [HttpPost]
        public IActionResult OrderTransport(Input model)
        {
            OrderManager om = new OrderManager(); 
            var infoList     = store.returnInfo();   
            var orderList   = store.returnOrder(); 
            var categoryList = store.returnCategory();
            var itms = om.orderTransport(infoList,
                                            orderList,
                                            categoryList,
                                            model.Route,
                                            model.CategoryType,
                                            model.Date1
                                        );
            
            return View(itms);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
