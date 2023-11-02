using Microsoft.EntityFrameworkCore;
using PostBook.Domain.Entities;
using PostBook.Domain.Interfaces.Repository;
using PostBook.Infraestructure.Data;

namespace PostBook.Repositories;

public class UserRepository : IUserRepository
{
    private readonly PostBookContext _context;

    public UserRepository(PostBookContext context)
    {
        _context = context;
    }

    public async Task CreateUser(User user)
    {
        await _context.AddAsync(user);
    }

    public async Task<bool> ValidateEmail(string email)
    {
        IQueryable<User> result = _context.Users.AsNoTracking().Where(u => u.Email == email);
        return await result.AnyAsync();
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        IQueryable<User> result = _context.Users.AsNoTracking();

        return await result.ToListAsync();
    }

    public Task<User> GetUserById(int id)
    {
        IQueryable<User> result = _context.Users.AsNoTracking().Where(u => u.Id == id);

        return result.FirstOrDefaultAsync();
    }

    public async Task<bool> SaveChanges()
    {
        return (await _context.SaveChangesAsync() > 0);
    }

    public Task<User> GetUserByEmail(string email)
    {
        IQueryable<User> result = _context.Users.AsNoTracking().Where(u => u.Email == email);
        return result.FirstOrDefaultAsync();
    }
}
