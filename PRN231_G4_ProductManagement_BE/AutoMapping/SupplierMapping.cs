using AutoMapper;
using PRN231_G4_ProductManagement_BE.Models;

namespace PRN231_G4_ProductManagement_BE.AutoMapping
{
    public class SupplierMapping : Profile
    {
        public SupplierMapping()
        {
            CreateMap<Supplier, DTO.SupplierDTO>();
            CreateMap<DTO.SupplierDTO, Supplier>();
        }
    }
}
