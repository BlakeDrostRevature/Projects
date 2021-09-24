using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAppModels.ViewModels {
    public class ViewCustomer {
        private string fname;
        private string lname;
        private string username;
        private string password;
        public string Fname {
            get {
                return this.fname;
            }
            set {
                if(value.Length > 50 || value.Length == 0) {
                    this.fname = "invalid First Name Input";
                }
                else {
                    this.fname = value;
                }
            }
        }
        public string Lname {
            get {
                return this.lname;
            }
            set {
                if (value.Length > 50 || value.Length == 0) {
                    this.lname = "invalid Last Name Input";
                }
                else {
                    this.lname = value;
                }
            }
        }
        public string Username {
            get {
                return this.username;
            }
            set {
                if (value.Length > 50 || value.Length == 0) {
                    this.username = "invalid username Input";
                }
                else {
                    this.username = value;
                }
            }
        }
        public string Password {
            get { return password; }
            set { password = value; }
        }
        public ViewCustomer() { }
        public ViewCustomer(string fname, string lname, string username, string password) {
            this.Fname = fname;
            this.Lname = lname;
            this.Username = username;
            this.Password = password;
        }
    }
}
