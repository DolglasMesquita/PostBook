using PostBook.Domain.Dtos;
using PostBook.Domain.Entities;

namespace PostBook.Domain.Interfaces.Service
{
    public interface IUserService
    {
        Task<bool> CreateUser(CreateUserDTO user);
        Task<bool> ValidateEmail(string email);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<LoggedUserDTO> AuthenticateUser(LoginDTO login);
    }
}
