using PostBook.Domain.Dtos;
using PostBook.Domain.Entities;

namespace PostBook.Domain.Interfaces.Service;

public interface IPostService
{
    Task<bool> CreatePost(CreatePostDTO post, int idUser);
    Task<IEnumerable<PostsDTO>> GetAllPosts(int idUser);
    Task<IEnumerable<PostsDTO>> GetPostsByUserId(int userId);
    Task<bool> UpdatePost(CreatePostDTO post, int idPost, int idUser);
    Task<bool> DeletePost(int idPost, int idUser);
    Task<bool> LikeUnlikePost(int idPost, int idUser);
}
