using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostBook.Domain.Dtos;
using PostBook.Domain.Entities;
using PostBook.Domain.Interfaces.Service;

namespace PostBook.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet]
    [Route("GetAllPosts")]
    public async Task<IActionResult> GetAllPosts()
    {
        try
        {
            int idUser = int.Parse(User.FindFirst("Id").Value);

            IEnumerable<PostsDTO> posts = await _postService.GetAllPosts(idUser);
            return Ok(posts);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet]
    [Route("GetMyPosts")]
    public async Task<IActionResult> GetMyPosts()
    {
        try
        {
            int idUser = int.Parse(User.FindFirst("Id").Value);

            IEnumerable<PostsDTO> posts = await _postService.GetPostsByUserId(idUser);
            return Ok(posts);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost]
    [Route("CreatePost")]
    public async Task<IActionResult> CreatePost(CreatePostDTO post)
    {
        try
        {
            int idUser = int.Parse(User.FindFirst("Id").Value);

            bool result = await _postService.CreatePost(post, idUser);
            return Created("Post created successfully", "Post created successfully");
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPatch]
    [Route("UpdatePost/{idPost}")]
    public async Task<IActionResult> UpdatePost(CreatePostDTO post, int idPost)
    {
        try
        {
            int idUser = int.Parse(User.FindFirst("Id").Value);

            bool result = await _postService.UpdatePost(post, idPost, idUser);
            return Ok("Post Updated successfully");
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpDelete]
    [Route("DeletePost/{idPost}")]
    public async Task<IActionResult> DeletePost(int idPost)
    {
        try
        {
            int idUser = int.Parse(User.FindFirst("id").Value);

            bool result = await _postService.DeletePost(idPost, idUser);
        
            return Ok("Post deleted successfully");
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost]
    [Route("LikeUnlikePost/{idPost}")]
    public async Task<IActionResult> LikeUnlikePost(int idPost)
    {
        try
        {
            int idUser = int.Parse(User.FindFirst("id").Value);

            bool result = await _postService.LikeUnlikePost(idPost, idUser);

           
            return result ? Ok("Post liked successfully") : Ok("Post unliked successfully");
           
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

}
