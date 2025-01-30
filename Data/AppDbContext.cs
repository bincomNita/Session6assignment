using Microsoft.EntityFrameworkCore;
using PhotoGallery.Models;
namespace PhotoGallery.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) :DbContext(options)
    {
        public DbSet<GalleryItem> GalleryItems { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
    }
}
