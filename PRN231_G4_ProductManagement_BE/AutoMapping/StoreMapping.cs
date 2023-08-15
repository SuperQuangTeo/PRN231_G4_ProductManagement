using AutoMapper;
using PRN231_G4_ProductManagement_BE.Models;

namespace PRN231_G4_ProductManagement_BE.AutoMapping
{
    public class StoreMapping : Profile
    {
        public StoreMapping()
        {
            CreateMap<Store, DTO.StoreDTO>();
            CreateMap<DTO.StoreDTO, Store>();
        }
    }
}
