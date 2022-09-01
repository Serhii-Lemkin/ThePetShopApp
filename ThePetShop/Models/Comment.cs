using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThePetShopApp.Models
{
    public class Comment
    {
        public Comment() => Created = DateTime.Now;
        public string UserId { get; set; }
        public IdentityUser? user { get; set; }
        public DateTime Created { get; set; }
        public int CommentId { get; set; }
        public int AnimalId { get; set; }
        public Animal? Animal { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? CommentTxt { get; set; }
    }
}
