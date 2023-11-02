using PostBook.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostBook.Domain.Dtos
{
    public class PostsDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Content { get; set; }
        public IEnumerable<LikeDTO> Likes { get; set; }
        public bool Liked { get; set; }
        public int UserId { get; set; }
        public UserDTO User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set;}
    }
}
