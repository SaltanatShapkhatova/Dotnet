using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using midLibrary.Models;

namespace midterm.Models{
    public class OrderManager
    {

        public List<Details> ReturnDetails(List<Book> bookList,List<Record> recordList,
               List<Employees> employeeList,List<Reader> readerList){
             var newDetails = readerList
                                          .Join(recordList,
                                            reader =>reader.Id,
                                            record =>record.Reader_id,(reader,record)=>
                                                new {
                                                    reader, record
                                                }
                                            )
                                          .Join(employeeList,
                                            records =>records.record.Employee_id,
                                            employeer =>employeer.Id,(records,employeer)=>
                                                new {
                                                    records,employeer
                                                }
                                            )
                                          .Join(bookList,
                                            employees =>employees.records.record.Book_id,
                                            books => books.Id, (employees,books)=>
                                                new {
                                                    employees,books
                                                }
                                          
                                          ).ToList();
                      
                         
                         var joined = newDetails 
                                       .Select( x => 
                                        new 
                                        {
                                            name         = x.employees.records.reader.Name,
                                            employeeName = x.employees.employeer.Name,
                                            borrowDate   = x.employees.records.record.Borrowed_date,
                                            returnDate   = x.employees.records.record.Returned_date,
                                            bookName     =x.books.Name         
                                     }).Select(item => new Details(){
                                        Name         = item.name,
                                        EmployeeName = item.employeeName,
                                        BorrowDate   = item.borrowDate,
                                        ReturnDate   = item.returnDate,
                                        BookName     =item.bookName 
                            }).ToList();
            return joined;
        }
        public  List<Input> orderTransport(List<Info> infoList, List<Order> orderList, 
                                        List<Category> categoryList, string route,
                                            string categoryType, string date1){
             
                                var itemList = infoList
                                                .Where(x => x.Route == route)
                                                .Select(x=>x.Info_id)
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
                                newRecord.Category_id = categoryType;
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