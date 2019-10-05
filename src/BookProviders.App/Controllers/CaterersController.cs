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
        private readonly IMapper _mapper;

        public CaterersController(ICatererRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var model = _mapper.Map<IEnumerable<CatererViewModel>>(await _repo.GetAll());
            return View(model);
        }

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
            await _repo.Add(caterer);

            return RedirectToAction("Index");
        }

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
            await _repo.Update(caterer);
            
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

            await _repo.Remove(id);
            
            return RedirectToAction("Index");
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
