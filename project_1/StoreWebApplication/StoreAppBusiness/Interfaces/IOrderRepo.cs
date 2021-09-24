using StoreAppModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAppBusiness.Interfaces {
    public interface IOrderRepo {
        Task<List<ViewOrder>> OrderListAsync();
        Task<ViewOrder> PlaceOrder(string u, string p, string l, decimal t);
    }
}
