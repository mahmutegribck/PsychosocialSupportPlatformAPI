using AutoMapper;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs.DoctorDTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs.PatientDTOs;
using PsychosocialSupportPlatformAPI.Business.Messages.DTOs;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs;
using PsychosocialSupportPlatformAPI.Entity.Entities;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.Business.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ApplicationUser, LoginDto>().ReverseMap();
            CreateMap<ApplicationUser, ResetPasswordDto>().ReverseMap();

            CreateMap<Doctor, LoginDto>().ReverseMap();
            CreateMap<Doctor, RegisterDoctorDto>().ReverseMap();
            CreateMap<Doctor, ResetPasswordDto>().ReverseMap();

            CreateMap<Patient, LoginDto>().ReverseMap();
            CreateMap<Patient, RegisterPatientDto>().ReverseMap();
            CreateMap<Patient, ResetPasswordDto>().ReverseMap();

            CreateMap<ApplicationUser, GetApplicationUserDto>().ReverseMap();
            CreateMap<Doctor, GetDoctorDto>().ReverseMap();
            CreateMap<Patient, GetPatientDto>().ReverseMap();

            CreateMap<Message, SendMessageDto>().ReverseMap();
            //CreateMap<ApplicationUser, GetApplicationUserDto>().ReverseMap();
            //CreateMap<ApplicationUser, UpdateApplicationUserDto>().ReverseMap();

            CreateMap<DateTime, DateOnly>().ConvertUsing(dt => DateOnly.FromDateTime(dt));
        }
    }
}
