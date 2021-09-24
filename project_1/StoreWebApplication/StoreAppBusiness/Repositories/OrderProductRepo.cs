using System;
using Microsoft.EntityFrameworkCore;
using StoreAppBusiness.Interfaces;
using StoreAppDBContext.Models;
using StoreAppModels.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAppBusiness.Repositories {
    public class OrderProductRepo : IModelMapper<OrderProduct, ViewOrderProduct> {
        private readonly StoreDBContext _context;
        public OrderProduct ViewToEF(ViewOrderProduct view) {
            int orderid = view.Order.OrderID;
            Product p = _context.Products.FromSqlRaw<Product>("SELECT * FROM Products WHERE ProductName = {0}", view.Products.Name).FirstOrDefault();
            OrderProduct op1 = _context.OrderProducts.FromSqlRaw<OrderProduct>("SELECT * FROM OrderProducts WHERE OrderID = {0} AND ProductID = {1}", orderid, p.ProductId).FirstOrDefault();
            return op1;
        }
        public ViewOrderProduct EFToView(OrderProduct ef) {
            ViewProduct p = new ViewProduct(ef.Product.ProductName, ef.Product.ProductPrice);
            ViewCustomer c = new ViewCustomer(ef.Order.Customer.CustomerFirstName, ef.Order.Customer.CustomerLastName, ef.Order.Customer.CustomerUsername, ef.Order.Customer.CustomerPassword);
            ViewStore s = new ViewStore(ef.Order.Store.StoreLocation);
            ViewOrder o = new ViewOrder(ef.Order.OrderId, ef.Order.OrderDate, c, s, ef.Order.TotalPrice);
            ViewOrderProduct c1 = new ViewOrderProduct(ef.Quantity, p, o);
            return c1;
        }
    }
}
