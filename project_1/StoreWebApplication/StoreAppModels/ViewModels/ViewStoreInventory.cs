using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAppModels.ViewModels {
    public class ViewStoreInventory {
        private ViewStore store;
        private ViewProduct product;
        private int quantity;
        public ViewStore Store { get { return store; } set { store = value; } }
        public ViewProduct Product { get { return product; } set { product = value; } }
        public int Quantity { get { return quantity; } set { quantity = value; } }
        public ViewStoreInventory(ViewStore vs, ViewProduct vp, int q) {
            this.Store = vs;
            this.Product = vp;
            this.Quantity = q;
        }
    }
}
