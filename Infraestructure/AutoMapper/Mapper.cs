using AutoMapper;
using PostBook.Domain.Dtos;
using PostBook.Domain.Entities;

namespace PostBook.Infraestructure.AutoMapper;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Post, PostsDTO>().ReverseMap();

        CreateMap<User, UserDTO>().ReverseMap();

        CreateMap<Like, LikeDTO>().ReverseMap();

    }

}
