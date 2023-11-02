using Microsoft.EntityFrameworkCore;
using PostBook.Domain.Dtos;
using PostBook.Domain.Entities;
using PostBook.Domain.Interfaces.Repository;
using PostBook.Infraestructure.Data;

namespace PostBook.Repositories;

public class PostRepository : IPostRepository
{
    private readonly PostBookContext _context;

    public PostRepository(PostBookContext context)
    {
        _context = context;
    }

    public async Task CreatePost(Post post)
    {
        await _context.Posts.AddAsync(post);
    }

    public Task DeletePost(Post post)
    {
        _context.Posts.Remove(post);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<Post>> GetAllPosts(int idUser)
    {
        IQueryable<Post> result = _context.Posts.AsNoTracking()
                .Include(u => u.User).Include(p => p.Likes).ThenInclude(u => u.User);

        return await result.OrderByDescending(o => o.CreatedAt).ToListAsync();
    }


    public async Task<Post> GetPostById(int idPost)
    {
        Post result = await _context.Posts.AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == idPost);

        return result;
    }

    public async Task<IEnumerable<Post>> GetPostsByUserId(int userId)
    {
        IQueryable<Post> result = _context.Posts.AsNoTracking().Where(p => p.UserId == userId)
                .Include(u => u.User).Include(p => p.Likes).ThenInclude(u => u.User);

        return await result.OrderByDescending(o => o.CreatedAt).ToListAsync();
    }

    public async Task CreateLikePost(Like like)
    {
        await _context.Likes.AddAsync(like);
    }

    public Task DeleteLikePost(Like like)
    {
        _context.Likes.Remove(like);
        return Task.CompletedTask;
    }

    public Task UpdatePost(Post post)
    {
        _context.Posts.Update(post);

        return Task.CompletedTask;
    }

    public async Task<bool> SaveChanges()
    {
        return (await _context.SaveChangesAsync() > 0);
    }

    public async Task<Like> GetLikePost(int idPost, int idUser)
    {
        Like result = await _context.Likes.AsNoTracking()
            .SingleOrDefaultAsync(l => l.PostId == idPost && l.UserId == idUser);

        return result;
    }
}
