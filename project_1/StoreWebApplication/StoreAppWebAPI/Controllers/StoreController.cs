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
    public class StoreController : Controller {
        private readonly IStoreRepo _storeRepo;
        private readonly ILogger<StoreController> _logger;
        public StoreController(IStoreRepo sr, ILogger<StoreController> logger) {
            _storeRepo = sr;
            _logger = logger;
        }

        
        // GET: StoreController
        public ActionResult Index() {
            return View();
        }

        // GET: StoreController/Details/5
        [HttpGet("storelist")]
        public async Task<List<ViewStore>> Details() {
            Task<List<ViewStore>> stores = _storeRepo.StoreListAsync();
            List<ViewStore> stores1 = await stores;
            return stores1;
        }

        // GET: StoreController/Create
        public ActionResult Create() {
            return View();
        }

        // POST: StoreController/Create
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

        // GET: StoreController/Edit/5
        public ActionResult Edit(int id) {
            return View();
        }

        // POST: StoreController/Edit/5
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

        // GET: StoreController/Delete/5
        public ActionResult Delete(int id) {
            return View();
        }

        // POST: StoreController/Delete/5
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
    }
}
