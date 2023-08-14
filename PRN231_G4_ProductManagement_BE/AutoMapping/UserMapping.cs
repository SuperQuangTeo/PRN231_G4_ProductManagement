using AutoMapper;
using PRN231_G4_ProductManagement_BE.Models;

namespace PRN231_G4_ProductManagement_BE.AutoMapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, DTO.UserDTO>();
            CreateMap<DTO.UserDTO, User>(); 
            CreateMap<User, DTO.User2DTO>();
            CreateMap<DTO.User2DTO, User>();
        }
    }
}
