using AutoMapper;
using Desafio.Backend.Application;
using Desafio.Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Backend.Infra.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeDto, EmployeeEntity>()
               .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.first_name, opt => opt.MapFrom(src => src.FirstName))
               .ForMember(dest => dest.last_name, opt => opt.MapFrom(src => src.LastName))
               .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
               .ForMember(dest => dest.document, opt => opt.MapFrom(src => src.Document))
               .ForMember(dest => dest.phones, opt => opt.MapFrom(src => src.Phones))
               .ForMember(dest => dest.manager_id, opt => opt.MapFrom(src => src.ManagerId))
               .ForMember(dest => dest.password, opt => opt.MapFrom(src => src.Password))
               .ForMember(dest => dest.birthday, opt => opt.MapFrom(src => src.Birthday.ToUniversalTime()))
               .ForMember(dest => dest.role_id, opt => opt.MapFrom(src => src.RoleId));

            CreateMap<EmployeeEntity, EmployeeDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.first_name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.last_name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.email))
                .ForMember(dest => dest.Document, opt => opt.MapFrom(src => src.document))
                .ForMember(dest => dest.Phones, opt => opt.MapFrom(src => src.phones))
                .ForMember(dest => dest.ManagerId, opt => opt.MapFrom(src => src.manager_id))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.password))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.birthday.ToUniversalTime()))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.role_id));

        }
    }

}
