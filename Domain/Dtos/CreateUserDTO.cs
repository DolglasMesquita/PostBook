using System.ComponentModel.DataAnnotations;

namespace PostBook.Domain.Dtos;

public class CreateUserDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Compare("Password")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}
