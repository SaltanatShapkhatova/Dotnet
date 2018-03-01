using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using midLibrary.Models;

namespace midterm.Models{
    public class OrderManager
    {
        
        public  List<Input> orderBook(List<Info> infoList, List<Order> orderList, 
                                        List<Category> categoryList, string route,
                                            string categoryType, string date1){
             
                                var itemList = infoList
                                                .Where(x => x.Route == route)
                                                .Select(x=>x.Id)
                                                .ToList();
                                var dayList = infoList
                                                .Where(x => x.Route == route)
                                                .Select(x=>x.Duration)
                                                .ToList();
                                int infoID=itemList.ElementAt(0);
                                Order newRecord = new Order();
                                newRecord.Date_ordered = DateTime.ParseExact(date1,"dd/mm/yyyy",null);
                                newRecord.Date_should_be_delivered = DateTime.ParseExact(date1,"dd/mm/yyyy",null)
                                                    .AddDays(dayList.ElementAt(0));
                                newRecord.Date_actually_delivered = null;
                                newRecord.Category = categoryType;
                                newRecord.Info_id = infoID;
                                orderList.Add(newRecord);
                             /*   Input newInput = new Input();
                                newInput.BookId = bookID;
                                newInput.PersonId = readerID;
                                newInput.BorrowDate =  DateTime.Today;
                                newInput.BookName = BookName;
                                List<Input> items = new List<Input>();
                                items.Add(newInput);*/
                return items;
        }
        
    }
}