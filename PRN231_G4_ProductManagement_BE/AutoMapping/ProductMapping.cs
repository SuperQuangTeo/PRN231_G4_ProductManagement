using AutoMapper;
using PRN231_G4_ProductManagement_BE.DTO;
using PRN231_G4_ProductManagement_BE.Models;

namespace PRN231_G4_ProductManagement_BE.AutoMapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(x => x.Supplier, y => y.MapFrom(src => src.Supplier))
                .ForMember(x => x.Category, y => y.MapFrom(src => src.Category))
                .ForMember(x => x.Unit, y => y.MapFrom(src => src.Unit));
            CreateMap<Product, ProductDTO>()
                .ForMember(x => x.Supplier, y => y.MapFrom(src => src.Supplier))
                .ForMember(x => x.Category, y => y.MapFrom(src => src.Category))
                .ForMember(x => x.Unit, y => y.MapFrom(src => src.Unit));
            CreateMap<Supplier, SupplierDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Unit, UnitDTO>();
        }
    }
}
