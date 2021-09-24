using Microsoft.EntityFrameworkCore;
using StoreAppBusiness.Interfaces;
using StoreAppDBContext.Models;
using StoreAppModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAppBusiness.Repositories {
    public class CustomerRepo : IModelMapper<Customer,ViewCustomer>, ICustomerRepo
    {
        private readonly StoreDBContext _context;
        public CustomerRepo(StoreDBContext context) {
            _context = context;
        }
        public Customer ViewToEF(ViewCustomer view) {
            Customer c1 = _context.Customers.FromSqlRaw<Customer>("SELECT * FROM Customers WHERE CustomerUsername = {0}", view.Username).FirstOrDefault();
            return c1;
        }
        public ViewCustomer EFToView(Customer ef) {
            ViewCustomer c1 = new ViewCustomer(ef.CustomerFirstName, ef.CustomerLastName, ef.CustomerUsername, ef.CustomerPassword);
            return c1;
        }
        public async Task<ViewCustomer> LoginCustomerAsync(ViewCustomer vc) {
            Customer c = await _context.Customers.FromSqlRaw<Customer>("SELECT * FROM Customers WHERE CustomerUsername = {0} AND CustomerPassword = {1}", vc.Username, vc.Password).FirstOrDefaultAsync();
            if (c == null) return null;
            ViewCustomer c1 = EFToView(c);
            return c1;
        }
        public async Task<ViewCustomer> RegisterCustomerAsync(ViewCustomer vc) {
            //Customer c = ViewToEF(vc);
            int response = await _context.Database.ExecuteSqlRawAsync("INSERT INTO Customers(CustomerFirstName, CustomerLastName, CustomerUsername, CustomerPassword) VALUES ({0},{1},{2},{3})", vc.Fname, vc.Lname, vc.Username, vc.Password);
            if (response != 1) return null;
            return await LoginCustomerAsync(vc);
        }
        public async Task<List<ViewCustomer>> CustomerListAsync() {
            List<Customer> customers = await _context.Customers.FromSqlRaw<Customer>("SELECT * FROM Customers").ToListAsync();
            List<ViewCustomer> vc = new List<ViewCustomer>();
            foreach(Customer c in customers) {
                vc.Add(EFToView(c));
            }
            return vc;
        }
    }
}
