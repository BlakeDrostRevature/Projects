using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAppModels.ViewModels {
    public class ViewProduct {
        private string name;
        private decimal price;
        public string Name {
            get {
                return this.name;
            }
            set {
                if(value.Length > 50 || value.Length == 0) {
                    this.name = "invalid product name input";
                }
                else {
                    this.name = value;
                }
            }
        }
        public decimal Price { 
            get {
                return this.price;
            }
            set {
                if(value > 1000 || value == 0) {
                    this.price = (decimal)20.00;
                }
                else {
                    this.price = value;
                }
            }
        }
        public ViewProduct(string name, decimal price) {
            this.Name = name;
            this.Price = price;
        }
    }
}
