using System.ComponentModel.DataAnnotations;

namespace PostBook.Domain.Dtos;

public class CreatePostDTO
{
    [Required]
    [MaxLength(255)]
    public string Content { get; set; }
}
