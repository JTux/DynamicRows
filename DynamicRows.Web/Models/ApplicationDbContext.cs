using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DynamicRows.Web.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection") { }

        public DbSet<Default> Defaults { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}