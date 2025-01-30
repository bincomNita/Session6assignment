using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.Data;
using PhotoGallery.Models;
using static System.Net.Mime.MediaTypeNames;

namespace PhotoGallery.Controllers
{
    public class GalleryController : Controller
    {

        private readonly AppDbContext _context;

        public GalleryController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult UploadImage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile image, [Bind("Id,Title,Description,ImagePath")] GalleryItem galleryItem)
        {
            if (image == null || string.IsNullOrEmpty(image.FileName) || image.Length <= 0)
            {
                ModelState.AddModelError("Image", "Please upload a valid image file.");
                return View();
            }

            string imagedir = Path.Combine("wwwroot", "Images");
            if (!Path.Exists(imagedir))
            {
                Directory.CreateDirectory(imagedir);
            }
            string ImagePath = Path.Combine(imagedir,image.FileName);

            using (var fileStream = new FileStream(Path.Combine(imagedir, image.FileName), FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }


            galleryItem.ImagePath = ImagePath;

            //saving to the database
            _context.Add(galleryItem);
            await _context.SaveChangesAsync();          
            return RedirectToAction("ListImage");
        }

        public IActionResult ListImage()
        {
            //display all the list of gallary items
            var galleryItems = _context.GalleryItems.ToList();         
            return View(galleryItems);
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ContactUs(ContactUs contactUs)
        {
            if (ModelState.IsValid) 
            {
                //saving to the database
                _context.Add(contactUs);
                await _context.SaveChangesAsync();                
            }
            return RedirectToAction("Success", "Gallery");
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
