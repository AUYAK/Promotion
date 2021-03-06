﻿using Domain.Entities;
using System.Data.Entity;

namespace Domain.Concrete
{
    class EFDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Blurb> Blurbs { get; set; }
        public DbSet<BlurbCategory> BlurbCategories { get; set; }
    }
}
