using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAppModels.ViewModels {
    public class ViewOrder {
        private int orderID;
        private DateTime orderDate;
        private ViewCustomer customer;
        private ViewStore store;
        private decimal total;

        public int OrderID { get { return orderID; } set { orderID = value; } }
        public DateTime OrderDate { get { return orderDate; } set { orderDate = value; } }
        public ViewCustomer Customer { get { return customer; } set { customer = value; } }
        public ViewStore Store { get { return store; } set { store = value; } }
        public decimal Total { get { return total; } set { total = value; } }
        public ViewOrder(int orderID, DateTime orderDate, ViewCustomer customer, ViewStore store, decimal total) {
            this.OrderID = orderID;
            this.OrderDate = orderDate;
            this.Customer = customer;
            this.Store = store;
            this.Total = total;
        }
    }
}
