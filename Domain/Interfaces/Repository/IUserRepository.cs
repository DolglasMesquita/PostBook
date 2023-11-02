using PostBook.Domain.Dtos;
using PostBook.Domain.Entities;

namespace PostBook.Domain.Interfaces.Repository;

public interface IUserRepository
{
    Task CreateUser(User user);
    Task<bool> ValidateEmail(string email);
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUserById(int id);
    Task<User> GetUserByEmail(string email);
    Task<bool> SaveChanges();

}
