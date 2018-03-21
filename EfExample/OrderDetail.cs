using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataServiceLibrary
{
   public class OrderDetails
    {

        public double UnitPrice { get; set; }
        [Key]
        public int OrderId{ get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
        public Order Order { get; set; }
    }
}
