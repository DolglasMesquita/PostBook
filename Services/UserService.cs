using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PostBook.Domain.Dtos;
using PostBook.Domain.Entities;
using PostBook.Domain.Interfaces.Repository;
using PostBook.Domain.Interfaces.Service;
using PostBook.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace PostBook.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public UserService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<bool> CreateUser(CreateUserDTO user)
    {
        try
        {
            bool emailExists = await _userRepository.ValidateEmail(user.Email);

            User newUser = new()
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
            };

            await _userRepository.CreateUser(newUser);

            return await _userRepository.SaveChanges();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> ValidateEmail(string email)
    {
        try
        {
            bool result = await _userRepository.ValidateEmail(email);
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        try
        {
            IEnumerable<User> result = await _userRepository.GetAllUsers();

            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<User> GetUserById(int id)
    {
        try
        {
            User result = await _userRepository.GetUserById(id);

            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<LoggedUserDTO> AuthenticateUser(LoginDTO login)
    {
        try
        {
            User user = await _userRepository.GetUserByEmail(login.Email);

            if (user.Password != login.Password)
            {
                return null;
            }

            LoggedUserDTO loggedUser = new()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Token = GenerateToken(user)
            };
            return loggedUser;
            
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private string GenerateToken(User user)
    {
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("Nome", user.Name, ClaimValueTypes.String),
                new Claim("Email", user.Email, ClaimValueTypes.String),
                new Claim("Id", user.Id.ToString(), ClaimValueTypes.Integer),
            }),
            Expires = DateTime.UtcNow.AddHours(24),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration.GetSection("JWT")["Key"])), SecurityAlgorithms.HmacSha256Signature)
        };

        JwtSecurityTokenHandler tokenHandler = new();

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

}
