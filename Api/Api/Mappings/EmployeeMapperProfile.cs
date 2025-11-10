using Api.Dtos.Request;
using Api.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;

namespace Api.Mappings
{
    public class EmployeeMapperProfile : Profile
    {
        public EmployeeMapperProfile() {

            CreateMap<RegisterRequestDto, Employee>();
        }
    }
}
