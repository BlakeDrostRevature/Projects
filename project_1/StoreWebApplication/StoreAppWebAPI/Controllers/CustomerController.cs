using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StoreAppBusiness.Interfaces;
using StoreAppBusiness.Repositories;
using StoreAppModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAppWebAPI.Controllers {
    public class CustomerController : Controller {
        private readonly ICustomerRepo _customerRepo;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(ICustomerRepo cr, ILogger<CustomerController> logger) {
            _customerRepo = cr;
            _logger = logger;
        }
        // GET: CustomerController
        public ActionResult Index() {
            return View();
        }

        // GET: CustomerController/Details/5
        [HttpGet("customerlist")]
        public async Task<List<ViewCustomer>> Details() {
            Task<List<ViewCustomer>> customers = _customerRepo.CustomerListAsync();
            List<ViewCustomer> customers1 = await customers;
            return customers1;
        }
        /*
        // GET: CustomerController/Create
        [HttpPost("register")]
        public async Task<ActionResult<ViewCustomer>> Create(ViewCustomer vc) {
            if (!ModelState.IsValid) return BadRequest();
            ViewCustomer c1 = await _customerRepo.RegisterCustomerAsync(vc);
            if(c1 == null) {
                return NotFound();
            }
            return Created($"~customer/{c1.Username}", c1);
        }
        */
        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id) {
            return View();
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id) {
            return View();
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
        [HttpGet("login/{username}/{password}")]
        public async Task<ActionResult<ViewCustomer>> Login(string username, string password) {
            if (!ModelState.IsValid) return BadRequest();
            ViewCustomer c = new ViewCustomer() { Username = username, Password = password };
            ViewCustomer c1 = await _customerRepo.LoginCustomerAsync(c);
            if(c1 == null) {
                return NotFound();
            }
            return Ok(c1);
        }
        [HttpPost("register/{fname}/{lname}/{username}/{password}")]
        public async Task<ActionResult<ViewCustomer>> Register(string fname, string lname, string username, string password) {
            if (!ModelState.IsValid) return BadRequest();
            ViewCustomer vc = new ViewCustomer(fname, lname, username, password);
            ViewCustomer c1 = await _customerRepo.RegisterCustomerAsync(vc);
            if(c1 == null) {
                return NotFound();
            }
            return c1;
        }
    }
}
