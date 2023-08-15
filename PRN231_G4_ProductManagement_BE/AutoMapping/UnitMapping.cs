using AutoMapper;
using PRN231_G4_ProductManagement_BE.Models;

namespace PRN231_G4_ProductManagement_BE.AutoMapping
{
    public class UnitMapping : Profile
    {
        public UnitMapping()
        {
            CreateMap<Unit, DTO.UnitDTO>();
            CreateMap<DTO.UnitDTO, Unit>();
        }
    }
}
