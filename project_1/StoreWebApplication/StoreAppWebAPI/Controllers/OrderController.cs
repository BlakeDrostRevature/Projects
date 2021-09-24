using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StoreAppBusiness.Interfaces;
using StoreAppModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAppWebAPI.Controllers {
    public class OrderController : Controller {
        private readonly IOrderRepo _orderRepo;
        private readonly ILogger<OrderController> _logger;
        public OrderController(IOrderRepo or, ILogger<OrderController> logger) {
            _orderRepo = or;
            _logger = logger;
        }

        // GET: OrderController
        public ActionResult Index() {
            return View();
        }

        // GET: OrderController/Details/5
        [HttpGet("orderlist")]
        public async Task<List<ViewOrder>> Details() {
            Task<List<ViewOrder>> orders = _orderRepo.OrderListAsync();
            List<ViewOrder> vo1 = await orders;
            return vo1;
        }

        // GET: OrderController/Create
        public ActionResult Create() {
            return View();
        }

        // POST: OrderController/Create
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

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id) {
            return View();
        }

        // POST: OrderController/Edit/5
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

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id) {
            return View();
        }

        // POST: OrderController/Delete/5
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
        [HttpPost("makeorder/{username}/{password}/{location}/{total}")]
        public async Task<ActionResult<ViewOrder>> PlaceOrder(string username, string password, string location, decimal total) {
            if (!ModelState.IsValid) return BadRequest();
            ViewOrder vo = await _orderRepo.PlaceOrder(username, password, location, total);
            if(vo == null) {
                return NotFound();
            }
            return vo;
        }
    }
}
