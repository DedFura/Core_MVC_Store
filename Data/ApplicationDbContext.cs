using System;
using System.Collections.Generic;
using System.Text;
using Core_MVC_Store.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core_MVC_Store.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<ProductTypes> ProductTypeses { get; set; }
        public DbSet<SpecialTags> SpecialTagses { get; set; }
        public DbSet<Products> Productses { get; set; }
    }
}
