using PostBook.Domain.Dtos;

namespace PostBook.Domain.Entities;

public class Post
{
    public int Id { get; set; }
    public string Content { get; set; }
    public IEnumerable<Like> Likes { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
}
