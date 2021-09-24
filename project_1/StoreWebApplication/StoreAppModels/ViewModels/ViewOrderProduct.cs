using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAppModels.ViewModels {
    public class ViewOrderProduct {
        private int quantity;
        private ViewProduct product;
        private ViewOrder order;
        public int Quantity { get { return quantity; } set { quantity = value; } }
        public ViewProduct Products { get { return product; } set { product = value; } }
        public ViewOrder Order 
            {
                get { return order; }
                set { order = value; }
        }
        public ViewOrderProduct(int q, ViewProduct vp, ViewOrder vo) {
            this.Quantity = q;
            this.Products = vp;
            this.Order = vo;
        }
    }
}
