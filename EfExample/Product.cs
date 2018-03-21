using System;
using System.Collections.Generic;
using System.Text;

namespace DataServiceLibrary
{
    public class Product
    {
        public OrderDetails orderDetail { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public double UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int QuantityPerUnit { get; set; }
        public int CategoryId { get; set; }
    }
}
