using StoreAppModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAppBusiness.Interfaces {
    public interface IStoreRepo {
        Task<List<ViewStore>> StoreListAsync();
    }
}
