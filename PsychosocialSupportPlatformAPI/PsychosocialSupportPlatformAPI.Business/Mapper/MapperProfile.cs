using AutoMapper;
using PsychosocialSupportPlatformAPI.Business.Appointments.DTOs;
using PsychosocialSupportPlatformAPI.Business.AppointmentSchedules.DTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs.DoctorDTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs.PatientDTOs;
using PsychosocialSupportPlatformAPI.Business.DoctorSchedules.DTOs;
using PsychosocialSupportPlatformAPI.Business.Messages.DTOs;
using PsychosocialSupportPlatformAPI.Business.Statistics.DTOs;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs.Admin;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs.DoctorDTOs;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs.PatientDTOs;
using PsychosocialSupportPlatformAPI.Business.Videos.DTOs;
using PsychosocialSupportPlatformAPI.Entity.Entities;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
using PsychosocialSupportPlatformAPI.Entity.Entities.Messages;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;

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
            CreateMap<ApplicationUser, GetAdminDto>().ReverseMap();
            CreateMap<Doctor, GetDoctorDto>().ReverseMap();
            CreateMap<Patient, GetPatientDto>().ReverseMap();


            CreateMap<Message, SendMessageDto>().ReverseMap();
            CreateMap<Message, GetMessageDto>()
                .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.Sender.Name))
                .ForMember(dest => dest.SenderSurname, opt => opt.MapFrom(src => src.Sender.Surname))
                .ForMember(dest => dest.ReceiverName, opt => opt.MapFrom(src => src.Receiver.Name))
                .ForMember(dest => dest.ReceiverSurname, opt => opt.MapFrom(src => src.Receiver.Surname));


            CreateMap<MessageOutbox, GetOutboxMessageDto>().ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Message.Text));

            CreateMap<Video, GetVideoDTO>().ReverseMap();
            CreateMap<Video, UpdateVideoDTO>().ReverseMap();

            CreateMap<VideoStatistics, AddVideoStatisticsDTO>().ReverseMap();
            CreateMap<VideoStatistics, CreateVideoStatisticsDTO>().ReverseMap();
            CreateMap<VideoStatistics, UpdateVideoStatisticsDTO>().ReverseMap();
            CreateMap<VideoStatistics, GetVideoStatisticsDTO>().ReverseMap();

            CreateMap<Doctor, UpdateDoctorDTO>().ReverseMap();
            CreateMap<Patient, UpdatePatientDTO>().ReverseMap();

            CreateMap<Patient, RegisterPatientDto>().ReverseMap();
            CreateMap<Doctor, RegisterDoctorDto>().ReverseMap();


            CreateMap<DoctorSchedule, CreateDoctorScheduleDTO>().ReverseMap();
            CreateMap<DoctorSchedule, UpdateDoctorScheduleDTO>().ReverseMap();
            CreateMap<DoctorSchedule, GetDoctorScheduleDTO>().ReverseMap();

            CreateMap<Appointment, CreateAppointmentDTO>().ReverseMap();
            CreateMap<Appointment, UpdateAppointmentDTO>().ReverseMap();
            CreateMap<Appointment, GetAppointmentDTO>().ReverseMap();

            CreateMap<AppointmentSchedule, GetAppointmentScheduleDTO>().ReverseMap();



            //CreateMap<ApplicationUser, GetApplicationUserDto>().ReverseMap();
            //CreateMap<ApplicationUser, UpdateApplicationUserDto>().ReverseMap();

            CreateMap<DateTime, DateOnly>().ConvertUsing(dt => DateOnly.FromDateTime(dt));
        }
    }
}
