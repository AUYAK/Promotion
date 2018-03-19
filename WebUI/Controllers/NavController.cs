using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class NavController : Controller
    {
        private IBlurbRepository repository;
        // GET: Nav
        public NavController(IBlurbRepository repo)
        {
            repository = repo;
        }
        public PartialViewResult Menu()
        {
            IEnumerable<string> categories = (IEnumerable<string>)repository.Blurbs.Select(x => x.Category.Name).Distinct();
            return PartialView(categories);
        }
    }
}