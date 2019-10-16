using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookProviders.App.ViewModels;
using BookProviders.Business.Interfaces;
using AutoMapper;
using BookProviders.Business.Models;

namespace BookProviders.App.Controllers
{
    public class CaterersController : BaseController
    {
        private readonly ICatererRepository _repo;
        private readonly ICatererService _service;
        private readonly IMapper _mapper;

        public CaterersController(ICatererRepository repo, ICatererService service, 
                                 IMapper mapper, INotifier notifier) : base(notifier)
        {
            _repo = repo;
            _mapper = mapper;
           _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var model = _mapper.Map<IEnumerable<CatererViewModel>>(await _repo.GetAll());
            return View(model);
        }

        [Route("Details/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var model = await GetCatererAndAddress(id);

            if (model == null)
                return NotFound();

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CatererViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var caterer = _mapper.Map<Caterer>(model);
            await _service.Add(caterer);

            if (!ValidOperation())
                return View(model);

            return RedirectToAction("Index");
        }

        [Route("Edit/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await GetCatererAddressAndProducs(id);

            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CatererViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            var caterer = _mapper.Map<Caterer>(model);
            await _service.Update(caterer);

            if (!ValidOperation())
                return View(model);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var model = await GetCatererAndAddress(id);
            
            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var model = await GetCatererAndAddress(id);

            if (model == null)
                return NotFound();

            await _service.Remove(id);

            if (!ValidOperation())
                return View(model);

            TempData["Success"] = "Caterer deleted!";

            return RedirectToAction("Index");
        }

        [Route("GetAddress/{id:guid}")]
        public async Task<IActionResult> GetAddress(Guid id)
        {
            var caterer = await GetCatererAndAddress(id);

            if (caterer == null)
                return NotFound();

            return PartialView("_DetailsAddress", caterer);
        }

        [Route("UpdateAddress/{id:guid}")]
        public async Task<IActionResult> UpdateAddress (Guid id)
        {
            var caterer = await GetCatererAndAddress(id);

            if (caterer == null)
                return NotFound();

            return PartialView("_UpdateAddress", 
                new CatererViewModel { Address = caterer.Address });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAddress(CatererViewModel model)
        {
            ModelState.Remove("Name");
            ModelState.Remove("Document");

            if(!ModelState.IsValid)
                return PartialView("_UpdateAddress", model);

            await _service.UpdateAddress(_mapper.Map<Address>(model.Address));

            if (!ValidOperation())
                return PartialView("_UpdateAddress", model);

            var url = Url.Action("GetAddress", "Caterers", new { id = model.Id });
            return Json(new { success = true, url });
        }

        private async Task<CatererViewModel> GetCatererAndAddress(Guid id)
        {
            return _mapper.Map<CatererViewModel>(await _repo.GetCatererAndAddress(id));
        }

        private async Task<CatererViewModel> GetCatererAddressAndProducs(Guid id)
        {
            return _mapper.Map<CatererViewModel>(await _repo.GetCatererAddressAndProducs(id));
        }
    }
}
