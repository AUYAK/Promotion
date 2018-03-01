using Domain.Abstract;
using Domain.Entities;
using System.Linq;

namespace Domain.Concrete
{
    public class EFBlurbRepository:IBlurbRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Blurb> Blurbs{get{ return context.Blurbs; } }
    }
}
