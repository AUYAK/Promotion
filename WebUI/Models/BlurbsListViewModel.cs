using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class BlurbsListViewModel
    {
        public IEnumerable<Blurb> Blurbs { get; set; }
        public PagingInfo pagingInfo { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public String CurrentCategory { get; set; }
    }
}