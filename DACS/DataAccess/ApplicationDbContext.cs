using DACS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using DACS.Models.EF;

namespace DACS.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Adv> Advs { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }

        public DbSet<ProductComment> productComments { get; set; }
        public DbSet<ApplicationUser> User { get; set; }
    }
}
