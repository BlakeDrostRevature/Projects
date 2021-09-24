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
    public class OrderRepo : IModelMapper<Order, ViewOrder>, IOrderRepo {
        private readonly StoreDBContext _context;
        public OrderRepo(StoreDBContext context) {
            _context = context;
        }
        public Order ViewToEF(ViewOrder view) {
            Order o = _context.Orders.FromSqlRaw<Order>("SELECT * FROM Orders WHERE OrderId = {0}", view.OrderID).FirstOrDefault();
            return o;
        }
        public ViewOrder EFToView(Order ef) {
            Customer c1 = _context.Customers.FromSqlRaw<Customer>("SELECT * FROM Customers WHERE CustomerID = {0}", ef.CustomerId).FirstOrDefault();
            ViewCustomer c = new ViewCustomer(c1.CustomerFirstName, c1.CustomerLastName, c1.CustomerUsername, c1.CustomerPassword);
            Store s1 = _context.Stores.FromSqlRaw<Store>("SELECT * FROM Stores WHERE StoreID = {0}", ef.StoreId).FirstOrDefault();
            ViewStore s = new ViewStore(s1.StoreLocation);
            ViewOrder o = new ViewOrder(ef.OrderId, ef.OrderDate, c, s, ef.TotalPrice);
            return o;
        }

        public async Task<List<ViewOrder>> OrderListAsync() {
            List<Order> orders = await _context.Orders.FromSqlRaw<Order>("SELECT * FROM Orders").ToListAsync();
            List<ViewOrder> vo = new List<ViewOrder>();
            foreach(Order o in orders) {
                vo.Add(EFToView(o));
            }
            return vo;
        }
        public async Task<ViewOrder> PlaceOrder(string u, string p, string l, decimal t) {
            Store s = await _context.Stores.FromSqlRaw<Store>("SELECT * FROM Stores WHERE StoreLocation = {0}", l).FirstOrDefaultAsync();
            Customer c = await _context.Customers.FromSqlRaw<Customer>("SELECT * FROM Customers WHERE CustomerUsername = {0} AND CustomerPassword = {1}", u, p).FirstOrDefaultAsync();
            DateTime oDate = DateTime.Today;
            int response = await _context.Database.ExecuteSqlRawAsync("INSERT INTO Orders(CustomerID,StoreID,OrderDate,TotalPrice) VALUES ({0},{1},{2},{3})",c.CustomerId, s.StoreId, oDate, t);
            if (response != 1) return null;
            Order o = await _context.Orders.FromSqlRaw<Order>("SELECT * FROM Orders WHERE OrderDate = {0} AND StoreID = {1} AND CustomerID = {2}", oDate, s.StoreId, c.CustomerId).FirstOrDefaultAsync();
            ViewCustomer vc = new ViewCustomer(c.CustomerFirstName,c.CustomerLastName,c.CustomerUsername,c.CustomerPassword);
            ViewStore vs = new ViewStore(s.StoreLocation);
            ViewOrder vo = new ViewOrder(o.OrderId, oDate,vc,vs,t);
            return vo;
        }
    }
}
