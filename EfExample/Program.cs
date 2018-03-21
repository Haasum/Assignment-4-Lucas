using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DataServiceLibrary
{
    public class program
    {

        static void Main(string[] args)
        {
            using (var db = new NorthwindContext())
            {

                // PrintCategory(1);
                // DeleteCatagory(15);
                //GetProductId(3);
                //  GetProductString("Chai");
                //   GetProductByCatagory(2);
                //GetOrderDetails(10248);
                // GetProductDetails(1);
                // GetOrder(10260);
                // GetOrderByShippingName("Vins et alcools Chevalier");
                // ListOrders();

                foreach (var product in db.Products.Include(x => x.Category))
                {
                    // Different ways to do the same - syntatic sugar
                    //Console.WriteLine(product.Name + " " + product.Category.Name);
                    //Console.WriteLine("{0} {1}", product.Name, product.Category.Name);
                    // Console.WriteLine($"{product.Name} {product.Category.Name}");

                }
            }
            //UpdateCategoryName(5, "Updated");
            //CreateCategory(new Category { Name = "Test", Description = "Test" });
            // SelectCategories();
        }


        public Category UpdateCategory(int id, string name, string description)
        {
            using (var db = new NorthwindContext())
            {
                var category = db.Categories.FirstOrDefault(x => x.Id == id);
                if (category == null)
                    category.Name = name;
                category.Description = description;
                db.SaveChanges();
                return category;
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
                if (category == null) return null;
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

        public List<OrderDetails> GetOrderDetailsByOrderId(int id)
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


        private void GetProductDetails(int id)
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


       public Order GetOrderByShippingName(string name)
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