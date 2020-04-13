using LittlePacktBookstore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LittlePacktBookstore.ViewModels;

namespace LittlePacktBookstore.Data
{
    public class LittlePacktBookStoreDbContex:IdentityDbContext<SiteUser>
    {
		public LittlePacktBookStoreDbContex(DbContextOptions options):base(options)
		{
		}
		public DbSet<Book> Books { get; set; }
		public DbSet<Carousel> Carousels { get; set; }
		public DbSet<Order> Orders { get; set; }
	}
}
