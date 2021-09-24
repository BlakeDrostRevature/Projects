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
    public class StoreRepo : IModelMapper<Store, ViewStore>, IStoreRepo {
        private readonly StoreDBContext _context;
        public StoreRepo(StoreDBContext context) {
            _context = context;
        }
        public Store ViewToEF(ViewStore view) {
            Store s = _context.Stores.FromSqlRaw<Store>("SELECT * FROM Stores WHERE StoreLocation = {0}", view.Location).FirstOrDefault();
            return s;
        }
        public ViewStore EFToView(Store ef) {
            ViewStore s = new ViewStore(ef.StoreLocation);
            return s;
        }

        public async Task<List<ViewStore>> StoreListAsync() {
            List<Store> stores = await _context.Stores.FromSqlRaw<Store>("SELECT * FROM Stores").ToListAsync();
            List<ViewStore> vs = new List<ViewStore>();
            foreach(Store s in stores) {
                vs.Add(EFToView(s));
            }
            return vs;
        }
    }
}
