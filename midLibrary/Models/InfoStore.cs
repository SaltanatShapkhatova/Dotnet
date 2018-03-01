using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace midLibrary.Models{
    public class InfoStore : IStore<Info>
    {
        private List<Info> _cachedCollection;

        public string Path { get; set; }

        public List<Info> GetCollection()
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

        public Info ConvertItem(string item)
        {
            var itemList = item.Split(';');

            return new Info()
            {
                Info_id = Convert.ToInt32(itemList[0]),
                Route = itemList[1],
                Price = Convert.ToInt32(itemList[2]),
                Duration = Convert.ToInt32(itemList[3])
            };
        }
    }
}