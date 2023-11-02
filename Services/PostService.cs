using AutoMapper;
using PostBook.Domain.Dtos;
using PostBook.Domain.Entities;
using PostBook.Domain.Interfaces.Repository;
using PostBook.Domain.Interfaces.Service;

namespace PostBook.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    public PostService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
       
    }
    public async Task<bool> CreatePost(CreatePostDTO post, int idUser)
    {
        try
        {
            Post newPost = new()
            {
                Content = post.Content,
                UserId = idUser,
            };

            await _postRepository.CreatePost(newPost);
            var result = await _postRepository.SaveChanges();
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> DeletePost(int idPost, int idUser)
    {
        try
        {
            Post post = await _postRepository.GetPostById(idPost) ?? throw new Exception("Post not found");

            if(idUser != post.UserId) throw new Exception("You can't delete this post");

            await _postRepository.DeletePost(post);
            return await _postRepository.SaveChanges();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<IEnumerable<PostsDTO>> GetAllPosts(int idUser)
    {
        try
        {
            IEnumerable<Post> result = await _postRepository.GetAllPosts(idUser);

            List<PostsDTO> posts = new();

            foreach (Post post in result)
            {
                List<LikeDTO> listLikeDTO = new();

                foreach (var item in post.Likes)
                {
                    LikeDTO likeDto = new()
                    {
                        UserName = item.User.Name,
                    };

                    listLikeDTO.Add(likeDto);
                }

                PostsDTO postsDTO = new()
                {
                    Id = post.Id,
                    Content = post.Content,
                    Likes = listLikeDTO,
                    UserId = post.UserId,
                    CreatedAt = DateTime.Now,
                    User = new UserDTO()
                    {
                        Name = post.User.Name,
                        Email = post.User.Email,
                    },
                    Liked = post.Likes.Any(like => like.UserId == idUser),
                    UpdatedAt = post.UpdatedAt,

                };


                listLikeDTO = new();

                posts.Add(postsDTO);
            }

            return posts;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<IEnumerable<PostsDTO>> GetPostsByUserId(int userId)
    {
        try
        {
            IEnumerable<Post> result = await _postRepository.GetPostsByUserId(userId);

            List<PostsDTO> posts = new();

            foreach (Post post in result)
            {
                List<LikeDTO> listLikeDTO = new();

                foreach (var item in post.Likes)
                {
                    LikeDTO likeDto = new()
                    {
                        UserName = item.User.Name,
                    };

                    listLikeDTO.Add(likeDto);
                }

                PostsDTO postsDTO = new()
                {
                    Id = post.Id,
                    Content = post.Content,
                    Likes = listLikeDTO,
                    UserId = post.UserId,
                    CreatedAt = DateTime.Now,
                    User = new UserDTO()
                    {
                        Name = post.User.Name,
                        Email = post.User.Email,
                    },
                    Liked = post.Likes.Any(like => like.UserId == userId),
                    UpdatedAt = post.UpdatedAt,

                };


                listLikeDTO = new();

                posts.Add(postsDTO);
            }

            return posts;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> LikeUnlikePost(int idPost, int idUser)
    {
        try
        {
            Like like = await _postRepository.GetLikePost(idPost, idUser);

            if(like == null)
            {
                like = new()
                {
                    PostId = idPost,
                    UserId = idUser
                };

                await _postRepository.CreateLikePost(like);
                await _postRepository.SaveChanges();

                return true;
            }
            else 
            {
                await _postRepository.DeleteLikePost(like);
                await _postRepository.SaveChanges();

                return false;
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }


    public async Task<bool> UpdatePost(CreatePostDTO post, int idPost, int idUser)
    {
        try
        {
            Post postUpdate = await _postRepository.GetPostById(idPost) ?? throw new Exception("Post not found");

            if (idUser != postUpdate.UserId) throw new Exception("You can't update this post");

            postUpdate.Content = post.Content;
            postUpdate.UpdatedAt = DateTime.Now;

            await _postRepository.UpdatePost(postUpdate);
            return await _postRepository.SaveChanges();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
