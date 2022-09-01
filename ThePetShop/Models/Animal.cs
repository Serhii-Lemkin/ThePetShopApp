using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThePetShopApp.Models
{
    public class Animal
    {
        public Animal() { }
        
        public int AnimalId { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? Description { get; set; }
        public int Age { get; set; }
        [DisplayName("Image Name")]
        public string? PictureName { get; set; }
        [DisplayName("Category")]
        public int CategoryId { get; set; }

        public Category? Categories { get; set; }
        [NotMapped]
        [DisplayName("Photo")]
        public IFormFile? PictureFile { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
