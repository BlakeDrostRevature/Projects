using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAppModels.ViewModels {
    public class ViewStore {
        private string location;
        public string Location { get { return location; } set { location = value; } }
        public ViewStore(string loc) {
            this.Location = loc;
        }
    }
}
