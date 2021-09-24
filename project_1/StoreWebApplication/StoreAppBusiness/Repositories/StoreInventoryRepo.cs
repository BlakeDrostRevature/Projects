using System;
using Microsoft.EntityFrameworkCore;
using StoreAppBusiness.Interfaces;
using StoreAppDBContext.Models;
using StoreAppModels.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace StoreAppBusiness.Repositories {
    public class StoreInventoryRepo : IModelMapper<StoreInventory, ViewStoreInventory>, IStoreInvRepo {
        private readonly StoreDBContext _context;
        private readonly ILogger<StoreInventoryRepo> _logger;
        public StoreInventoryRepo(StoreDBContext context, ILogger<StoreInventoryRepo> logger) {
            _context = context;
            _logger = logger;
        }
        public StoreInventory ViewToEF(ViewStoreInventory view) {
            Store s = _context.Stores.FromSqlRaw<Store>("SELECT * FROM Stores WHERE StoreLocation = {0}", view.Store.Location).FirstOrDefault();
            Product p = _context.Products.FromSqlRaw<Product>("SELECT * FROM Products WHERE ProductName = {0}", view.Product.Name).FirstOrDefault();
            StoreInventory si = _context.StoreInventorys.FromSqlRaw<StoreInventory>("SELECT * FROM StoreInventorys WHERE StoreID = {0} AND ProductID = {1}", s.StoreId, p.ProductId).FirstOrDefault();
            return si;
        }
        public ViewStoreInventory EFToView(StoreInventory ef) {
            ViewStore s = new ViewStore(ef.Store.StoreLocation);
            Product p1 = _context.Products.FromSqlRaw<Product>("SELECT * FROM Products WHERE ProductID = {0}", ef.ProductId).FirstOrDefault();
            ViewProduct p = new ViewProduct(p1.ProductName, p1.ProductPrice);
            ViewStoreInventory si = new ViewStoreInventory(s, p, ef.Quantity);
            return si;
        }

        public async Task<List<ViewStoreInventory>> StoreInventoryListAsync(ViewStore s) {
            Store s1 = await _context.Stores.FromSqlRaw<Store>("SELECT * FROM Stores WHERE StoreLocation = {0}", s.Location).FirstOrDefaultAsync();
            List<StoreInventory> storeInv = await _context.StoreInventorys.FromSqlRaw<StoreInventory>("SELECT * FROM StoreInventorys WHERE StoreID = {0}", s1.StoreId).ToListAsync();
            List<ViewStoreInventory> vsi = new List<ViewStoreInventory>();
            foreach(StoreInventory si in storeInv) {
                vsi.Add(EFToView(si));
            }
            return vsi;
        }
    }
}
