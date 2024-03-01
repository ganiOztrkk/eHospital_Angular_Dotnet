using AutoMapper;
using E_HospitalServer.Entities.DTOs;
using E_HospitalServer.Entities.Models;

namespace E_HospitalServer.DataAccess.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserDto, User>();
    }
}