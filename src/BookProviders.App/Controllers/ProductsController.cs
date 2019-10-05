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
    public class ProductsController : BaseController
    {
        private readonly IProductRepository _repo;
        private readonly ICatererRepository _repoCaterer;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository repo, ICatererRepository repoCaterer, IMapper mapper)
        {
            _repo = repo;
            _repoCaterer = repoCaterer;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var model = _mapper.Map<IEnumerable<ProductViewModel>>(await _repo.GetProductsAndCaterers());

            return View(model);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var model = await GetProduct(id);

            if (model == null)
                return NotFound();

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = await GetCaterers(new ProductViewModel());

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            model = await GetCaterers(model);

            if (!ModelState.IsValid)
                return NotFound();

            await _repo.Add(_mapper.Map<Product>(model));

            return RedirectToAction("Indext");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await GetProduct(id);

            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return NotFound();

            await _repo.Update(_mapper.Map<Product>(model));

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var model = await GetProduct(id);

            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {

            var model = await GetProduct(id);

            if (model == null)
                return NotFound();

            await _repo.Remove(id);

            return RedirectToAction("Index");
        }

        private async Task<ProductViewModel> GetProduct(Guid id)
        {
            var product = _mapper.Map<ProductViewModel>(await _repo.GetProductAndCaterer(id));
            product.Caterers = _mapper.Map<IEnumerable<CatererViewModel>>(await _repoCaterer.GetAll());
            return product;
        }

        private async Task<ProductViewModel> GetCaterers(ProductViewModel model)
        {
            model.Caterers = _mapper.Map<IEnumerable<CatererViewModel>>(await _repoCaterer.GetAll());
            return model;
        }
    }
}
