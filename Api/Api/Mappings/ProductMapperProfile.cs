using Api.Dtos.Request;
using Api.Dtos.Response;
using Api.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;

namespace Api.Mappings
{
    public class ProductMapperProfile : Profile
    {
        public ProductMapperProfile() {

            CreateMap<ProductRequestDto, Product>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<Product, ProductResponseDto>();

        }
    }
}
