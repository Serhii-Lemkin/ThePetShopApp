using System.ComponentModel.DataAnnotations.Schema;

namespace ThePetShopApp.Models
{
    public class Category
    {

        public int CategoryId { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string? Name { get; set; }

        public ICollection<Animal>? Animals { get; set; }
    }
}
