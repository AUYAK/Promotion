using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int Pagesize = 3;
        public ProductController(IProductRepository rep)
        {
            repository = rep;
        }
        public ViewResult List(string category, int page = 1)
        {
            ProductListViewModel PrLVM = new ProductListViewModel()
            {
                Products = repository.Products
                .Where(b => category == null ||category==""|| b.Category.Name == category)
                .OrderByDescending(b => b.DateOfCreate).Skip((page - 1) * Pagesize)
                .Take(Pagesize)
                .ToList(),
                CurrentCategory = category,
                Categories = repository.Products.Select(x => x.Category.Name).Distinct(),
                pagingInfo = new PagingInfo { CurrentPage = page, ItemsPerPage = Pagesize, TotalItems = repository.Products.Where(b => category == null || b.Category.Name == category).Count() }

            };
            ViewBag.CurrentCategory = category;
            return View(PrLVM);

        }
    }
}