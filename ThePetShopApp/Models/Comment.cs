using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThePetShopApp.Models
{
    public class Comment
    {        
        public int CommentId { get; set; }
        
        public int AnimalId { get; set; }
        public Animal? Animal { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? CommentTxt { get; set; }
    }
}
