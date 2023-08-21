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
            CreateMap<Supplier, SupplierDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Unit, UnitDTO>();

            CreateMap<Import, ImportDTO>()
                .ForMember(x => x.Product, y => y.MapFrom(src => src.Product))
                .ForMember(x => x.User, y => y.MapFrom(src => src.User));
            CreateMap<Export, ExportDTO>()
                .ForMember(x => x.Product, y => y.MapFrom(src => src.Product))
                .ForMember(x => x.Store, y => y.MapFrom(src => src.Store))
                .ForMember(x => x.User, y => y.MapFrom(src => src.User));

            CreateMap<Product, ProductDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<Store, StoreDTO>();
        }
    }
}
