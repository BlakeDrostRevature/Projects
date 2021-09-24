using StoreAppModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAppBusiness.Interfaces {
    public interface ICustomerRepo {
        Task<ViewCustomer> LoginCustomerAsync(ViewCustomer vc);
        Task<ViewCustomer> RegisterCustomerAsync(ViewCustomer vc);
        Task<List<ViewCustomer>> CustomerListAsync();
    }
}
