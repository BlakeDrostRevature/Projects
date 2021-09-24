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
    public class ProductRepo : IModelMapper<Product, ViewProduct> {
        private readonly StoreDBContext _context;
        public Product ViewToEF(ViewProduct view) {
            Product p = _context.Products.FromSqlRaw<Product>("SELECT * FROM Products WHERE ProductName = {0}", view.Name).FirstOrDefault();
            return p;
        }
        public ViewProduct EFToView(Product ef) {
            ViewProduct p = new ViewProduct(ef.ProductName, ef.ProductPrice);
            return p;
        }
    }
}
