using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace PhotoGallery.Models
{
    public class GalleryItem
    {      
        public int Id { get; set; }      
        public string ImagePath { get; set; }     
    }
}
