using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class BlurbsController : Controller
    {
        private IBlurbRepository repository;
        public int Pagesize = 3;
        public BlurbsController(IBlurbRepository rep)
        {
            repository = rep;
        }
        public ViewResult List(string category, int page = 1)
        {
            BlurbsListViewModel PrLVM = new BlurbsListViewModel()
            {
                Blurbs = repository.Blurbs
                .Where(b => category == null || b.Category.Name == category)
                .OrderByDescending(b => b.DateOfCreate).Skip((page - 1) * Pagesize)
                .Take(Pagesize)
                .ToList(),
                CurrentCategory = category,
                Categories= repository.Blurbs.Select(x => x.Category.Name).Distinct(),
                pagingInfo = new PagingInfo { CurrentPage = page, ItemsPerPage = Pagesize, TotalItems = repository.Blurbs.Count() }
            };
            ViewBag.CurrentCategory = category;
            return View(PrLVM);
            
        }
    }
}