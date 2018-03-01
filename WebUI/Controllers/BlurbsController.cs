using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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
        public ViewResult List(int page=1)
        {
            return View(repository.Blurbs.OrderByDescending(b=>b.DateOfCreate).Skip((page-1)*Pagesize).Take(Pagesize).ToList());
        }
    }
}