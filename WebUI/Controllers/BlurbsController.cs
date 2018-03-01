using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class BlurbsController : Controller
    {
        private IBlurbRepository repository;
        public BlurbsController(IBlurbRepository rep)
        {
            repository = rep;
        }
        public ViewResult List()
        {
            return View(repository.Blurbs.ToList());
        }
    }
}