using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace midLibrary.Models{
    public class OrderStore : IStore<Order>
    {
        private List<Order> _cachedCollection;

        public string Path { get; set; }

        public List<Order> GetCollection()
        {
            if(_cachedCollection == null)
            {
                var data = File.ReadAllLines(Path);
                _cachedCollection = data
                    .Skip(1)
                    .Select(x => ConvertItem(x))
                    .ToList();
            }
            
            return _cachedCollection;
        }

        public Order ConvertItem(string item)
        {
            var itemList = item.Split(';');

            return new Order()
            {
                Order_id = Convert.ToInt32(itemList[0]),
                Info_id = Convert.ToInt32(itemList[1]),
                Date_ordered = DateTime.ParseExact(itemList[2],"dd/mm/yyyy", null),
                Date_should_be_delivered = DateTime.ParseExact(itemList[3],"dd/mm/yyyy", null),
                Date_actually_delivered = DateTime.ParseExact(itemList[4],"dd/mm/yyyy", null),
                Category = Convert.ToInt32(itemList[5]),
                Status = itemList[6]
            };
        }
    }
}