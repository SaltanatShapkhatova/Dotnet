using Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Data
{
    public class DbInitializer
    {
        public static void Initialize(InventoryContext context)
        {
            context.Database.EnsureCreated();
            if (context.Departments.Any()) { return; }

            var Departments = new Department[]
            {
                new Department {Name="HR"},
                new Department {Name="IT"},
                new Department {Name="Marketing"},
                new Department {Name="Finance"},
            };
            foreach (Department r in Departments)
            {
                context.Departments.Add(r);
            }
            context.SaveChanges();

            var Products = new Product[]
            {
                new Product {Name="Comp1", DepartmentID=1},
                new Product {Name="Comp2", DepartmentID=1},

                new Product {Name="Laptop1", DepartmentID=2},
                new Product {Name="Laptop2", DepartmentID=2},

                new Product {Name="Monitor1", DepartmentID=3},
                new Product {Name="Monitor2", DepartmentID=3},

                new Product {Name="Mouse1", DepartmentID=4},
                new Product {Name="Mouse2", DepartmentID=4},
            };
            foreach (Product a in Products)
            {
                context.Products.Add(a);
            }
            context.SaveChanges();

            var customers = new Customer[]
            {
                new Customer {Username="user1", Password="user1"},
                new Customer {Username="user2", Password="user2"},
                new Customer {Username="user3", Password="user3"},
            };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();

            var invs = new Inventory[]
            {
                new Inventory {CustomerID=5, Completed=true},
            };
            foreach (Inventory r in invs)
            {
                context.Inventories.Add(r);
            }
            context.SaveChanges();

            var details = new Detail[]
            {
                new Detail {ID=1, ProductID=1},
                new Detail {ID=1, ProductID=2},
                new Detail {ID=1, ProductID=3},
            };
            foreach(Detail d in details)
            {
                context.Details.Add(d);
            }
            context.SaveChanges();
        }
    }
}
