using AutoMapper;
using PsychosocialSupportPlatformAPI.Business.Appointments.DTOs;
using PsychosocialSupportPlatformAPI.Business.Appointments.DTOs.Doctor;
using PsychosocialSupportPlatformAPI.Business.AppointmentSchedules.DTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs.DoctorDTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs.PatientDTOs;
using PsychosocialSupportPlatformAPI.Business.DoctorSchedules.DTOs;
using PsychosocialSupportPlatformAPI.Business.Messages.DTOs;
using PsychosocialSupportPlatformAPI.Business.Statistics.Appointments.DTOs;
using PsychosocialSupportPlatformAPI.Business.Statistics.Videos.DTOs;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs.Admin;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs.DoctorDTOs;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs.DoctorTitle;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs.PatientDTOs;
using PsychosocialSupportPlatformAPI.Business.Videos.DTOs;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
using PsychosocialSupportPlatformAPI.Entity.Entities.Messages;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using PsychosocialSupportPlatformAPI.Entity.Entities.Videos;

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

            CreateMap<Doctor, UpdateDoctorDTO>().ReverseMap();
            CreateMap<Patient, UpdatePatientDTO>().ReverseMap();


            CreateMap<ApplicationUser, GetApplicationUserDto>().ReverseMap();
            CreateMap<ApplicationUser, GetAdminDto>().ReverseMap();
            CreateMap<Doctor, GetDoctorDto>().ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.DoctorTitle.Title)).ReverseMap();
            CreateMap<Doctor, GetPatientDoctorDto>().ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.DoctorTitle.Title)).ReverseMap();
            CreateMap<Patient, GetPatientDto>().ReverseMap();


            CreateMap<Message, SendMessageDto>().ReverseMap();
            CreateMap<Message, GetMessageDto>()
                .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.Sender.Name))
                .ForMember(dest => dest.SenderSurname, opt => opt.MapFrom(src => src.Sender.Surname))
                .ForMember(dest => dest.ReceiverName, opt => opt.MapFrom(src => src.Receiver.Name))
                .ForMember(dest => dest.ReceiverSurname, opt => opt.MapFrom(src => src.Receiver.Surname));


            CreateMap<Video, GetVideoDTO>().ReverseMap();
            CreateMap<Video, UpdateVideoDTO>().ReverseMap();

            CreateMap<VideoStatistics, AddVideoStatisticsDTO>().ReverseMap();
            CreateMap<VideoStatistics, CreateVideoStatisticsDTO>().ReverseMap();
            CreateMap<VideoStatistics, UpdateVideoStatisticsDTO>().ReverseMap();
            CreateMap<VideoStatistics, GetVideoStatisticsDTO>()
                .ForMember(dest => dest.VideoTitle, opt => opt.MapFrom(s => s.Video.Title))
                .ReverseMap();


            CreateMap<DoctorSchedule, CreateDoctorScheduleDTO>().ReverseMap();
            CreateMap<DoctorSchedule, UpdateDoctorScheduleDTO>().ReverseMap();
            CreateMap<DoctorSchedule, GetDoctorScheduleByAdminDTO>()
                .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Day.ToShortDateString()))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.Name))
                .ForMember(dest => dest.DoctorSurname, opt => opt.MapFrom(src => src.Doctor.Surname))
                .ForMember(dest => dest.DoctorProfileImageUrl, opt => opt.MapFrom(src => src.Doctor.ProfileImageUrl))
                .ForMember(dest => dest.DoctorTitle, opt => opt.MapFrom(src => src.Doctor.DoctorTitle.Title));

            CreateMap<DoctorSchedule, GetDoctorScheduleDTO>()
                .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Day.ToShortDateString()))
                .ReverseMap();

            CreateMap<AppointmentSchedule, GetAppointmentScheduleDTO>().ReverseMap();
            CreateMap<AppointmentSchedule, CancelPatientAppointmentDTO>().ReverseMap();
            CreateMap<AppointmentSchedule, CancelDoctorAppointmentDTO>().ReverseMap();
            CreateMap<AppointmentSchedule, GetPatientAppointmentDTO>()
                .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId))
                .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Day.ToShortDateString()))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.Name))
                .ForMember(dest => dest.DoctorSurname, opt => opt.MapFrom(src => src.Doctor.Surname))
                .ForMember(dest => dest.DoctorTitle, opt => opt.MapFrom(src => src.Doctor.DoctorTitle.Title));


            CreateMap<AppointmentStatistics, AddAppointmentStatisticsDTO>().ReverseMap();
            CreateMap<AppointmentStatistics, UpdateAppointmentStatisticsDTO>().ReverseMap();
            CreateMap<AppointmentStatistics, DeleteAppointmentStatisticsDTO>().ReverseMap();
            CreateMap<AppointmentStatistics, GetAppointmentStatisticsDTO>()
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.Name))
                .ForMember(dest => dest.DoctorSurname, opt => opt.MapFrom(src => src.Doctor.Surname))
                .ForMember(dest => dest.DoctorTitle, opt => opt.MapFrom(src => src.Doctor.DoctorTitle.Title))
                .ForMember(dest => dest.DoctorProfileImageUrl, opt => opt.MapFrom(src => src.Doctor.ProfileImageUrl))
                .ForMember(dest => dest.AppointmentDay, opt => opt.MapFrom(src => src.AppointmentSchedule.Day.ToShortDateString()));


            CreateMap<AppointmentSchedule, GetDoctorAppointmentDTO>()
                .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.PatientId))
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient!.Name))
                .ForMember(dest => dest.PatientSurname, opt => opt.MapFrom(src => src.Patient!.Surname))
                .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Day.ToShortDateString()));

            CreateMap<DoctorTitle, AddDoctorTitleDTO>().ReverseMap();
            CreateMap<DoctorTitle, GetDoctorTitleDTO>().ReverseMap();


            CreateMap<DateTime, DateOnly>().ConvertUsing(dt => DateOnly.FromDateTime(dt));
        }
    }
}
