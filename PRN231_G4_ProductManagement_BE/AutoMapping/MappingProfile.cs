using AutoMapper;
using PRN231_G4_ProductManagement_BE.Models;

namespace PRN231_G4_ProductManagement_BE.AutoMapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, DTO.CategoryDTO>();
            CreateMap<DTO.CategoryDTO, Category>();

        }
    }
}
