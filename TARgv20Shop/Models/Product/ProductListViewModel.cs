using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Targv20Shop.Models.Product
{
    public class ProductListViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
