using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFBlurbRepository:IBlurbRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Blurb> Blurbs{get{ return context.Blurbs; } }
    }
}
