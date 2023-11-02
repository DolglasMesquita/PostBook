using PostBook.Domain.Dtos;
using PostBook.Domain.Entities;

namespace PostBook.Domain.Interfaces.Repository;

public interface IPostRepository
{
    Task CreatePost(Post post);
    Task<IEnumerable<Post>> GetAllPosts(int idUser);
    Task<Post> GetPostById(int idPost);
    Task<IEnumerable<Post>> GetPostsByUserId(int userId);
    Task UpdatePost(Post post);
    Task DeletePost(Post post);
    Task<Like> GetLikePost(int idPost, int idUser);
    Task CreateLikePost(Like like);
    Task DeleteLikePost(Like like);
    Task<bool> SaveChanges();
}
