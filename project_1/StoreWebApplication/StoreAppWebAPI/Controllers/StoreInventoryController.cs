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
    public class StoreInventoryController : Controller {
        private readonly IStoreInvRepo _storeInventoryRepo;
        private readonly ILogger<StoreInventoryController> _logger;

        public StoreInventoryController(IStoreInvRepo sr, ILogger<StoreInventoryController> logger) {
            _storeInventoryRepo = sr;
            _logger = logger;
        }
        
        // GET: StoreInventoryController
        public ActionResult Index() {
            return View();
        }

        // GET: StoreInventoryController/Details/5
        public ActionResult Details(int id) {
            return View();
        }

        // GET: StoreInventoryController/Create
        public ActionResult Create() {
            return View();
        }

        // POST: StoreInventoryController/Create
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

        // GET: StoreInventoryController/Edit/5
        public ActionResult Edit(int id) {
            return View();
        }

        // POST: StoreInventoryController/Edit/5
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

        // GET: StoreInventoryController/Delete/5
        public ActionResult Delete(int id) {
            return View();
        }

        // POST: StoreInventoryController/Delete/5
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

        [HttpGet("product/{sumlocation}")]
        public async Task<ActionResult<ViewStoreInventory>> StoreInvStuff(string sumlocation) {
            if (!ModelState.IsValid) return BadRequest();
            ViewStore s = new ViewStore(sumlocation);
            List<ViewStoreInventory> storeInv = await _storeInventoryRepo.StoreInventoryListAsync(s);
            if(storeInv == null) {
                return NotFound();
            }
            return Ok(storeInv);
        }
    }
}
