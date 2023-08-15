using AutoMapper;
using PRN231_G4_ProductManagement_BE.Models;

namespace PRN231_G4_ProductManagement_BE.AutoMapping
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, DTO.CategoryDTO>();
            CreateMap<DTO.CategoryDTO, Category>();
        }
    }
}
