using Api.Dtos.Request;
using Api.Dtos.Response;
using Api.Entities;
using AutoMapper;

namespace Api.Mappings
{
    public class SaleMapperProfile : Profile
    {
        public SaleMapperProfile()
        {


            CreateMap<Sale, SaleResponseDto>();

        }
    
    }
}
