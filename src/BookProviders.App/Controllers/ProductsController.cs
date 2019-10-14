﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookProviders.App.ViewModels;
using BookProviders.Business.Interfaces;
using AutoMapper;
using BookProviders.Business.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

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

        [Route("Details/{id:guid}")]
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

            var profixImg = Guid.NewGuid() + "_";
            if (!await UploadImage(model.ImageUpload, profixImg))
                return View(model);

            model.Image = profixImg + model.ImageUpload.FileName;
            await _repo.Add(_mapper.Map<Product>(model));

            return RedirectToAction("Index");
        }

        [Route("Edit/{id:guid}")]
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

            var product = await GetProduct(id);
            model.Caterer = product.Caterer;
            model.Image = product.Image;

            if (!ModelState.IsValid)
                return NotFound();

            if (model.ImageUpload != null)
            {
                var profixImg = Guid.NewGuid() + "_";
                if (!await UploadImage(model.ImageUpload, profixImg))
                    return View(model);

                product.Image = profixImg + model.ImageUpload.FileName;
            }

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Active = model.Active;

            await _repo.Update(_mapper.Map<Product>(product));

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

        private async Task<bool> UploadImage(IFormFile file, string prefixImg)
        {
            if (file.Length <= 0)
                return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", prefixImg + file.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "File already exists!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create)){
                await file.CopyToAsync(stream);
            }

            return true;
        }
    }
}
