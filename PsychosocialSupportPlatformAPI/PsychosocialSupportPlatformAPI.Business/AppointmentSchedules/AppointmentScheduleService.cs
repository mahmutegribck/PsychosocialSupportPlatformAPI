using AutoMapper;
using PsychosocialSupportPlatformAPI.Business.AppointmentSchedules.DTOs;
using PsychosocialSupportPlatformAPI.DataAccess.AppointmentSchedules;
using PsychosocialSupportPlatformAPI.DataAccess.DoctorSchedules;
using PsychosocialSupportPlatformAPI.Entity.Entities;
using PsychosocialSupportPlatformAPI.Entity.Enums;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq.Expressions;

namespace PsychosocialSupportPlatformAPI.Business.AppointmentSchedules
{
    public class AppointmentScheduleService : IAppointmentScheduleService
    {
        private readonly IAppointmentScheduleRepository _appointmentScheduleRepository;
        private readonly IDoctorScheduleRepository _doctorScheduleRepository;
        private readonly IMapper _mapper;

        public AppointmentScheduleService(IAppointmentScheduleRepository appointmentScheduleRepository, IDoctorScheduleRepository doctorScheduleRepository, IMapper mapper)
        {
            _appointmentScheduleRepository = appointmentScheduleRepository;
            _doctorScheduleRepository = doctorScheduleRepository;
            _mapper = mapper;
        }


        public async Task AddAppointmentSchedule(AddAppointmentScheduleDTO addAppointmentScheduleDTO)
        {
            var doctorSchedule = await _doctorScheduleRepository.GetAllDoctorScheduleById(addAppointmentScheduleDTO.DoctorId);
            var startDay = DateTime.Today.DayOfWeek;

            List<AppointmentSchedule> appointmentList = new List<AppointmentSchedule>();

            for (int i = 0; i < 15; i++)
            {
                //int dayOfWeekValue = (int)startDay + i;
                //DayOfWeek currentDasy = (DayOfWeek)(dayOfWeekValue % 7);

                //var currentDay = (DayOfWeek)(((int)startDay + i) % 7);

                switch ((DayOfWeek)(((int)startDay + i) % 7))
                {

                    case DayOfWeek.Sunday:

                        var sundayDoctorSchedules = doctorSchedule.Where(s => s.Day == DayOfWeek.Sunday).ToList();

                        foreach (var item in sundayDoctorSchedules)
                        {
                            AppointmentSchedule appointment = new()
                            {
                                Day = DateTime.Now.AddDays(i),
                                DoctorId = item.DoctorId
                            };

                            if (item.EightToNine == true)
                            {
                                appointment.TimeRange = TimeRange.EightToNine;
                                appointmentList.Add(appointment);
                            }
                            if (item.NineToTen == true)
                            {
                                appointment.TimeRange = TimeRange.NineToTen;
                                appointmentList.Add(appointment);
                            }
                            if (item.TenToEleven == true)
                            {
                                appointment.TimeRange = TimeRange.TenToEleven;
                                appointmentList.Add(appointment);
                            }
                            if (item.ElevenToTwelve == true)
                            {
                                appointment.TimeRange = TimeRange.ElevenToTwelve;
                                appointmentList.Add(appointment);

                            }
                            if (item.TwelveToThirteen == true)
                            {
                                appointment.TimeRange = TimeRange.TwelveToThirteen;
                                appointmentList.Add(appointment);
                            }
                            if (item.ThirteenToFourteen == true)
                            {
                                appointment.TimeRange = TimeRange.ThirteenToFourteen;
                                appointmentList.Add(appointment);
                            }
                            if (item.FourteenToFifteen == true)
                            {
                                appointment.TimeRange = TimeRange.FourteenToFifteen;
                                appointmentList.Add(appointment);
                            }
                            if (item.FifteenToSixteen == true)
                            {
                                appointment.TimeRange = TimeRange.FifteenToSixteen;
                                appointmentList.Add(appointment);
                            }
                            if (item.SixteenToSeventeen == true)
                            {
                                appointment.TimeRange = TimeRange.SixteenToSeventeen;
                                appointmentList.Add(appointment);
                            }
                        };
                        break;


                    case DayOfWeek.Monday:

                        var mondayDoctorSchedules = doctorSchedule.Where(s => s.Day == DayOfWeek.Monday).ToList();

                        foreach (var item in mondayDoctorSchedules)
                        {
                            AppointmentSchedule appointment = new()
                            {
                                Day = DateTime.Now.AddDays(i),
                                DoctorId = item.DoctorId
                            };

                            if (item.EightToNine == true)
                            {
                                appointment.TimeRange = TimeRange.EightToNine;
                                appointmentList.Add(appointment);
                            }
                            if (item.NineToTen == true)
                            {
                                appointment.TimeRange = TimeRange.NineToTen;
                                appointmentList.Add(appointment);
                            }
                            if (item.TenToEleven == true)
                            {
                                appointment.TimeRange = TimeRange.TenToEleven;
                                appointmentList.Add(appointment);
                            }
                            if (item.ElevenToTwelve == true)
                            {
                                appointment.TimeRange = TimeRange.ElevenToTwelve;
                                appointmentList.Add(appointment);

                            }
                            if (item.TwelveToThirteen == true)
                            {
                                appointment.TimeRange = TimeRange.TwelveToThirteen;
                                appointmentList.Add(appointment);
                            }
                            if (item.ThirteenToFourteen == true)
                            {
                                appointment.TimeRange = TimeRange.ThirteenToFourteen;
                                appointmentList.Add(appointment);
                            }
                            if (item.FourteenToFifteen == true)
                            {
                                appointment.TimeRange = TimeRange.FourteenToFifteen;
                                appointmentList.Add(appointment);
                            }
                            if (item.FifteenToSixteen == true)
                            {
                                appointment.TimeRange = TimeRange.FifteenToSixteen;
                                appointmentList.Add(appointment);
                            }
                            if (item.SixteenToSeventeen == true)
                            {
                                appointment.TimeRange = TimeRange.SixteenToSeventeen;
                                appointmentList.Add(appointment);
                            }
                        }
                        break;


                    case DayOfWeek.Tuesday:

                        var tuesdayDoctorSchedules = doctorSchedule.Where(s => s.Day == DayOfWeek.Tuesday).ToList();

                        foreach (var item in tuesdayDoctorSchedules)
                        {
                            AppointmentSchedule appointment = new()
                            {
                                Day = DateTime.Now.AddDays(i),
                                DoctorId = item.DoctorId
                            };

                            if (item.EightToNine == true)
                            {
                                appointment.TimeRange = TimeRange.EightToNine;
                                appointmentList.Add(appointment);
                            }
                            if (item.NineToTen == true)
                            {
                                appointment.TimeRange = TimeRange.NineToTen;
                                appointmentList.Add(appointment);
                            }
                            if (item.TenToEleven == true)
                            {
                                appointment.TimeRange = TimeRange.TenToEleven;
                                appointmentList.Add(appointment);
                            }
                            if (item.ElevenToTwelve == true)
                            {
                                appointment.TimeRange = TimeRange.ElevenToTwelve;
                                appointmentList.Add(appointment);

                            }
                            if (item.TwelveToThirteen == true)
                            {
                                appointment.TimeRange = TimeRange.TwelveToThirteen;
                                appointmentList.Add(appointment);
                            }
                            if (item.ThirteenToFourteen == true)
                            {
                                appointment.TimeRange = TimeRange.ThirteenToFourteen;
                                appointmentList.Add(appointment);
                            }
                            if (item.FourteenToFifteen == true)
                            {
                                appointment.TimeRange = TimeRange.FourteenToFifteen;
                                appointmentList.Add(appointment);
                            }
                            if (item.FifteenToSixteen == true)
                            {
                                appointment.TimeRange = TimeRange.FifteenToSixteen;
                                appointmentList.Add(appointment);
                            }
                            if (item.SixteenToSeventeen == true)
                            {
                                appointment.TimeRange = TimeRange.SixteenToSeventeen;
                                appointmentList.Add(appointment);
                            }
                        }
                        break;


                    case DayOfWeek.Wednesday:
                        var wednesdayDoctorSchedules = doctorSchedule.Where(s => s.Day == DayOfWeek.Wednesday).ToList();

                        foreach (var item in wednesdayDoctorSchedules)
                        {
                            AppointmentSchedule appointment = new()
                            {
                                Day = DateTime.Now.AddDays(i),
                                DoctorId = item.DoctorId
                            };

                            if (item.EightToNine == true)
                            {
                                appointment.TimeRange = TimeRange.EightToNine;
                                appointmentList.Add(appointment);
                            }
                            if (item.NineToTen == true)
                            {
                                appointment.TimeRange = TimeRange.NineToTen;
                                appointmentList.Add(appointment);
                            }
                            if (item.TenToEleven == true)
                            {
                                appointment.TimeRange = TimeRange.TenToEleven;
                                appointmentList.Add(appointment);
                            }
                            if (item.ElevenToTwelve == true)
                            {
                                appointment.TimeRange = TimeRange.ElevenToTwelve;
                                appointmentList.Add(appointment);

                            }
                            if (item.TwelveToThirteen == true)
                            {
                                appointment.TimeRange = TimeRange.TwelveToThirteen;
                                appointmentList.Add(appointment);
                            }
                            if (item.ThirteenToFourteen == true)
                            {
                                appointment.TimeRange = TimeRange.ThirteenToFourteen;
                                appointmentList.Add(appointment);
                            }
                            if (item.FourteenToFifteen == true)
                            {
                                appointment.TimeRange = TimeRange.FourteenToFifteen;
                                appointmentList.Add(appointment);
                            }
                            if (item.FifteenToSixteen == true)
                            {
                                appointment.TimeRange = TimeRange.FifteenToSixteen;
                                appointmentList.Add(appointment);
                            }
                            if (item.SixteenToSeventeen == true)
                            {
                                appointment.TimeRange = TimeRange.SixteenToSeventeen;
                                appointmentList.Add(appointment);
                            }
                        }
                        break;


                    case DayOfWeek.Thursday:
                        var thursdayDoctorSchedules = doctorSchedule.Where(s => s.Day == DayOfWeek.Thursday).ToList();

                        foreach (var item in thursdayDoctorSchedules)
                        {
                            AppointmentSchedule appointment = new()
                            {
                                Day = DateTime.Now.AddDays(i),
                                DoctorId = item.DoctorId
                            };

                            if (item.EightToNine == true)
                            {
                                appointment.TimeRange = TimeRange.EightToNine;
                                appointmentList.Add(appointment);
                            }
                            if (item.NineToTen == true)
                            {
                                appointment.TimeRange = TimeRange.NineToTen;
                                appointmentList.Add(appointment);
                            }
                            if (item.TenToEleven == true)
                            {
                                appointment.TimeRange = TimeRange.TenToEleven;
                                appointmentList.Add(appointment);
                            }
                            if (item.ElevenToTwelve == true)
                            {
                                appointment.TimeRange = TimeRange.ElevenToTwelve;
                                appointmentList.Add(appointment);

                            }
                            if (item.TwelveToThirteen == true)
                            {
                                appointment.TimeRange = TimeRange.TwelveToThirteen;
                                appointmentList.Add(appointment);
                            }
                            if (item.ThirteenToFourteen == true)
                            {
                                appointment.TimeRange = TimeRange.ThirteenToFourteen;
                                appointmentList.Add(appointment);
                            }
                            if (item.FourteenToFifteen == true)
                            {
                                appointment.TimeRange = TimeRange.FourteenToFifteen;
                                appointmentList.Add(appointment);
                            }
                            if (item.FifteenToSixteen == true)
                            {
                                appointment.TimeRange = TimeRange.FifteenToSixteen;
                                appointmentList.Add(appointment);
                            }
                            if (item.SixteenToSeventeen == true)
                            {
                                appointment.TimeRange = TimeRange.SixteenToSeventeen;
                                appointmentList.Add(appointment);
                            }
                        }
                        break;


                    case DayOfWeek.Friday:
                        var fridayDoctorSchedules = doctorSchedule.Where(s => s.Day == DayOfWeek.Friday).ToList();

                        foreach (var item in fridayDoctorSchedules)
                        {
                            AppointmentSchedule appointment = new()
                            {
                                Day = DateTime.Now.AddDays(i),
                                DoctorId = item.DoctorId
                            };

                            if (item.EightToNine == true)
                            {
                                appointment.TimeRange = TimeRange.EightToNine;
                                appointmentList.Add(appointment);
                            }
                            if (item.NineToTen == true)
                            {
                                appointment.TimeRange = TimeRange.NineToTen;
                                appointmentList.Add(appointment);
                            }
                            if (item.TenToEleven == true)
                            {
                                appointment.TimeRange = TimeRange.TenToEleven;
                                appointmentList.Add(appointment);
                            }
                            if (item.ElevenToTwelve == true)
                            {
                                appointment.TimeRange = TimeRange.ElevenToTwelve;
                                appointmentList.Add(appointment);

                            }
                            if (item.TwelveToThirteen == true)
                            {
                                appointment.TimeRange = TimeRange.TwelveToThirteen;
                                appointmentList.Add(appointment);
                            }
                            if (item.ThirteenToFourteen == true)
                            {
                                appointment.TimeRange = TimeRange.ThirteenToFourteen;
                                appointmentList.Add(appointment);
                            }
                            if (item.FourteenToFifteen == true)
                            {
                                appointment.TimeRange = TimeRange.FourteenToFifteen;
                                appointmentList.Add(appointment);
                            }
                            if (item.FifteenToSixteen == true)
                            {
                                appointment.TimeRange = TimeRange.FifteenToSixteen;
                                appointmentList.Add(appointment);
                            }
                            if (item.SixteenToSeventeen == true)
                            {
                                appointment.TimeRange = TimeRange.SixteenToSeventeen;
                                appointmentList.Add(appointment);
                            }
                        }
                        break;


                    case DayOfWeek.Saturday:
                        var saturdayDoctorSchedules = doctorSchedule.Where(s => s.Day == DayOfWeek.Saturday).ToList();

                        foreach (var item in saturdayDoctorSchedules)
                        {
                            AppointmentSchedule appointment = new()
                            {
                                Day = DateTime.Now.AddDays(i),
                                DoctorId = item.DoctorId
                            };

                            if (item.EightToNine == true)
                            {
                                appointment.TimeRange = TimeRange.EightToNine;
                                appointmentList.Add(appointment);
                            }
                            if (item.NineToTen == true)
                            {
                                appointment.TimeRange = TimeRange.NineToTen;
                                appointmentList.Add(appointment);
                            }
                            if (item.TenToEleven == true)
                            {
                                appointment.TimeRange = TimeRange.TenToEleven;
                                appointmentList.Add(appointment);
                            }
                            if (item.ElevenToTwelve == true)
                            {
                                appointment.TimeRange = TimeRange.ElevenToTwelve;
                                appointmentList.Add(appointment);

                            }
                            if (item.TwelveToThirteen == true)
                            {
                                appointment.TimeRange = TimeRange.TwelveToThirteen;
                                appointmentList.Add(appointment);
                            }
                            if (item.ThirteenToFourteen == true)
                            {
                                appointment.TimeRange = TimeRange.ThirteenToFourteen;
                                appointmentList.Add(appointment);
                            }
                            if (item.FourteenToFifteen == true)
                            {
                                appointment.TimeRange = TimeRange.FourteenToFifteen;
                                appointmentList.Add(appointment);
                            }
                            if (item.FifteenToSixteen == true)
                            {
                                appointment.TimeRange = TimeRange.FifteenToSixteen;
                                appointmentList.Add(appointment);
                            }
                            if (item.SixteenToSeventeen == true)
                            {
                                appointment.TimeRange = TimeRange.SixteenToSeventeen;
                                appointmentList.Add(appointment);
                            }
                        }
                        break;


                    default:
                        return;

                }
            }
            await _appointmentScheduleRepository.AddAppointmentScheduleList(appointmentList);

        }

        public Task DeleteAppointmentSchedule(int appointmentScheduleId)
        {
            throw new NotImplementedException();
        }
    }
}
