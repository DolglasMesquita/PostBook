using PostBook.Domain.Dtos;

namespace PostBook.Domain.Entities;

public class Like
{
    public int PostId { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

}
