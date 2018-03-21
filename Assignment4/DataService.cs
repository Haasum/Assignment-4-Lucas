using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

using DataServiceLibrary;

namespace Assignment4   
{
     public class DataService
    {
        public Boolean UpdateCategory(int id, string name, string description)
        {
            using (var db = new NorthwindContext())
            {
                var category = db.Categories.FirstOrDefault(x => x.Id == id);
                if (category == null) return false;
                category.Name = name;
                category.Description = description;
                db.SaveChanges();
                return true;
            }
        }

      public Category CreateCategory(string name, string description)
        {
            using (var db = new NorthwindContext())
            {
                Category category = new Category { Name = name, Description = description };
                db.Categories.Add(category);
                db.SaveChanges();
                return category;
            }
        }

        public List<Category> GetCategories()
        {
            using (var db = new NorthwindContext())
            {
                var category = db.Categories;
                return category.ToList();
                }
            }
        


       public Category GetCategory(int id)
        {

            using (var db = new NorthwindContext())
                {
                    var category = db.Categories.FirstOrDefault(x => x.Id == id);
                if (category == null)return null;
                return category;


            }

        }
        public bool DeleteCategory(int id)
        {   

            using (var db = new NorthwindContext())
            {
                var category = db.Categories.FirstOrDefault(x => x.Id == id);
                if (category == null) return false;
                else
                {
                    db.Remove(category);
                    db.SaveChanges();
                    return true;
                }
            }
        }
       public Product GetProduct(int id)
        {
            using (var db = new NorthwindContext())
            {   

                var product = db.Products.Include(Category => Category.Category).FirstOrDefault(x => x.Id == id);

                return product;


            }
        }


        public List<Product> GetProductByName(string name)
        {
            using (var db = new NorthwindContext())
            {
                var product = db.Products.Where(x => x.Name.Contains(name));

                return product.ToList();

            }
        }


        public List<Product> GetProductByCategory(int id)
        {
            using (var db = new NorthwindContext())
            {

                var product = db.Products.Where(x => x.CategoryId == id);

                return product.ToList();
            }

           
        }

        public List <OrderDetails> GetOrderDetailsByOrderId(int id)
        {
            using (var db = new NorthwindContext())
            {

                var orderdetail = db.OrderDetails.Include(Product => Product.Product).Where(x => x.OrderId == id);
                if (orderdetail == null) return null;
                else 
                {
                   return orderdetail.ToList();
                }
                
        }
        }


        private static void GetProductDetails(int id)
        {
            using (var db = new NorthwindContext())
            {
                foreach (var product in db.Products.Include(OrderDetail => OrderDetail.orderDetail).ThenInclude(Order => Order.Order))
                    if (product.Id == id)
                    {
                        Console.WriteLine(product.orderDetail.Order.Date + ", " + product.UnitPrice + ", " + product.orderDetail.Quantity);
                    }
            }
        }
        public Order GetOrder(int id)
        {
            using (var db = new NorthwindContext())
            {
                var order = db.Orders
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                    .ThenInclude(p => p.Category)
                    .FirstOrDefault(x => x.Id == id);

                return order;


            }

        }


        private static Order GetOrderByShippingName(string name)
        {

            using (var db = new NorthwindContext())
            {
                var order = db.Orders
                    .Include(o => o.OrderDetails)
                    .FirstOrDefault(x => x.ShipName == name);

                return order;
            }
        }

       public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();
            using (var db = new NorthwindContext())
            {

               return db.Orders.ToList();


                
            }
        }
    }
}