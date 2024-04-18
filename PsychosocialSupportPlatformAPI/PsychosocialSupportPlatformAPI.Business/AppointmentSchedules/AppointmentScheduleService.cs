using AutoMapper;
using PsychosocialSupportPlatformAPI.Business.AppointmentSchedules.DTOs;
using PsychosocialSupportPlatformAPI.DataAccess.AppointmentSchedules;
using PsychosocialSupportPlatformAPI.DataAccess.DoctorSchedules;
using PsychosocialSupportPlatformAPI.Entity.Entities;
using PsychosocialSupportPlatformAPI.Entity.Enums;

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

            for (int i = 0; i < 14; i++)
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
                                //Day = DateTime.Now.AddDays(i).Date,
                                DoctorId = item.DoctorId
                            };

                            if (item.EightToNine == true)
                            {
                                appointment.TimeRange = TimeRange.EightToNine;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.NineToTen == true)
                            {
                                appointment.TimeRange = TimeRange.NineToTen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.TenToEleven == true)
                            {
                                appointment.TimeRange = TimeRange.TenToEleven;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.ElevenToTwelve == true)
                            {
                                appointment.TimeRange = TimeRange.ElevenToTwelve;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);

                            }
                            if (item.TwelveToThirteen == true)
                            {
                                appointment.TimeRange = TimeRange.TwelveToThirteen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.ThirteenToFourteen == true)
                            {
                                appointment.TimeRange = TimeRange.ThirteenToFourteen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.FourteenToFifteen == true)
                            {
                                appointment.TimeRange = TimeRange.FourteenToFifteen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.FifteenToSixteen == true)
                            {
                                appointment.TimeRange = TimeRange.FifteenToSixteen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.SixteenToSeventeen == true)
                            {
                                appointment.TimeRange = TimeRange.SixteenToSeventeen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                // appointmentList.Add(appointment);
                            }
                        };
                        break;


                    case DayOfWeek.Monday:

                        var mondayDoctorSchedules = doctorSchedule.Where(s => s.Day == DayOfWeek.Monday).ToList();

                        foreach (var item in mondayDoctorSchedules)
                        {
                            AppointmentSchedule appointment = new()
                            {
                                //Day = DateTime.Now.AddDays(i).Date,
                                DoctorId = item.DoctorId
                            };

                            if (item.EightToNine == true)
                            {
                                appointment.TimeRange = TimeRange.EightToNine;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);

                            }
                            if (item.NineToTen == true)
                            {
                                appointment.TimeRange = TimeRange.NineToTen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                // appointmentList.Add(appointment);
                            }
                            if (item.TenToEleven == true)
                            {
                                appointment.TimeRange = TimeRange.TenToEleven;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.ElevenToTwelve == true)
                            {
                                appointment.TimeRange = TimeRange.ElevenToTwelve;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);

                            }
                            if (item.TwelveToThirteen == true)
                            {
                                appointment.TimeRange = TimeRange.TwelveToThirteen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.ThirteenToFourteen == true)
                            {
                                appointment.TimeRange = TimeRange.ThirteenToFourteen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.FourteenToFifteen == true)
                            {
                                appointment.TimeRange = TimeRange.FourteenToFifteen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.FifteenToSixteen == true)
                            {
                                appointment.TimeRange = TimeRange.FifteenToSixteen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.SixteenToSeventeen == true)
                            {
                                appointment.TimeRange = TimeRange.SixteenToSeventeen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                        }
                        break;


                    case DayOfWeek.Tuesday:

                        var tuesdayDoctorSchedules = doctorSchedule.Where(s => s.Day == DayOfWeek.Tuesday).ToList();

                        foreach (var item in tuesdayDoctorSchedules)
                        {
                            AppointmentSchedule appointment = new()
                            {
                                //Day = DateTime.Now.AddDays(i).Date,

                                DoctorId = item.DoctorId
                            };

                            if (item.EightToNine == true)
                            {
                                appointment.TimeRange = TimeRange.EightToNine;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.NineToTen == true)
                            {
                                appointment.TimeRange = TimeRange.NineToTen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.TenToEleven == true)
                            {
                                appointment.TimeRange = TimeRange.TenToEleven;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.ElevenToTwelve == true)
                            {
                                appointment.TimeRange = TimeRange.ElevenToTwelve;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);

                            }
                            if (item.TwelveToThirteen == true)
                            {
                                appointment.TimeRange = TimeRange.TwelveToThirteen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.ThirteenToFourteen == true)
                            {
                                appointment.TimeRange = TimeRange.ThirteenToFourteen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.FourteenToFifteen == true)
                            {
                                appointment.TimeRange = TimeRange.FourteenToFifteen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.FifteenToSixteen == true)
                            {
                                appointment.TimeRange = TimeRange.FifteenToSixteen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.SixteenToSeventeen == true)
                            {
                                appointment.TimeRange = TimeRange.SixteenToSeventeen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                        }
                        break;


                    case DayOfWeek.Wednesday:
                        var wednesdayDoctorSchedules = doctorSchedule.Where(s => s.Day == DayOfWeek.Wednesday).ToList();

                        foreach (var item in wednesdayDoctorSchedules)
                        {
                            AppointmentSchedule appointment = new()
                            {
                                //Day = DateTime.Now.AddDays(i).Date,
                                DoctorId = item.DoctorId
                            };

                            if (item.EightToNine == true)
                            {
                                appointment.TimeRange = TimeRange.EightToNine;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.NineToTen == true)
                            {
                                appointment.TimeRange = TimeRange.NineToTen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.TenToEleven == true)
                            {
                                appointment.TimeRange = TimeRange.TenToEleven;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.ElevenToTwelve == true)
                            {
                                appointment.TimeRange = TimeRange.ElevenToTwelve;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);

                            }
                            if (item.TwelveToThirteen == true)
                            {
                                appointment.TimeRange = TimeRange.TwelveToThirteen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.ThirteenToFourteen == true)
                            {
                                appointment.TimeRange = TimeRange.ThirteenToFourteen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.FourteenToFifteen == true)
                            {
                                appointment.TimeRange = TimeRange.FourteenToFifteen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.FifteenToSixteen == true)
                            {
                                appointment.TimeRange = TimeRange.FifteenToSixteen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.SixteenToSeventeen == true)
                            {
                                appointment.TimeRange = TimeRange.SixteenToSeventeen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                        }
                        break;


                    case DayOfWeek.Thursday:
                        var thursdayDoctorSchedules = doctorSchedule.Where(s => s.Day == DayOfWeek.Thursday).ToList();

                        foreach (var item in thursdayDoctorSchedules)
                        {
                            AppointmentSchedule appointment = new()
                            {
                                //Day = DateTime.Now.AddDays(i).Date,
                                DoctorId = item.DoctorId
                            };

                            if (item.EightToNine == true)
                            {
                                AppointmentSchedule appointmentEightToNine = new()
                                {
                                    TimeRange = TimeRange.EightToNine,
                                    Day = DateTime.Now.AddDays(i).Date,
                                    DoctorId = item.DoctorId
                                };
                                //appointment.TimeRange = TimeRange.EightToNine;
                                //appointment.Day = DateTime.Now.AddDays(i).Date;
                                //await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                appointmentList.Add(appointmentEightToNine);
                            }
                            if (item.NineToTen == true)
                            {
                                AppointmentSchedule appointmentNineToTen = new()
                                {
                                    TimeRange = TimeRange.NineToTen,
                                    Day = DateTime.Now.AddDays(i).Date,
                                    DoctorId = item.DoctorId
                                };
                                //appointment.TimeRange = TimeRange.NineToTen;
                                //appointment.Day = DateTime.Now.AddDays(i).Date;
                                //await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                appointmentList.Add(appointmentNineToTen);
                            }
                            if (item.TenToEleven == true)
                            {
                                AppointmentSchedule appointmentTenToEleven = new()
                                {
                                    TimeRange = TimeRange.TenToEleven,
                                    Day = DateTime.Now.AddDays(i).Date,
                                    DoctorId = item.DoctorId
                                };
                                //appointment.TimeRange = TimeRange.TenToEleven;
                                //appointment.Day = DateTime.Now.AddDays(i).Date;
                                //await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                appointmentList.Add(appointmentTenToEleven);
                            }
                            if (item.ElevenToTwelve == true)
                            {
                                AppointmentSchedule appointmentElevenToTwelve = new()
                                {
                                    TimeRange = TimeRange.ElevenToTwelve,
                                    Day = DateTime.Now.AddDays(i).Date,
                                    DoctorId = item.DoctorId
                                };
                                //appointment.TimeRange = TimeRange.ElevenToTwelve;
                                //appointment.Day = DateTime.Now.AddDays(i).Date;
                                //await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                appointmentList.Add(appointmentElevenToTwelve);

                            }
                            if (item.TwelveToThirteen == true)
                            {
                                AppointmentSchedule appointmentTwelveToThirteen = new()
                                {
                                    TimeRange = TimeRange.TwelveToThirteen,
                                    Day = DateTime.Now.AddDays(i).Date,
                                    DoctorId = item.DoctorId
                                };

                                //appointment.TimeRange = TimeRange.TwelveToThirteen;
                                //appointment.Day = DateTime.Now.AddDays(i).Date;
                                //await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                appointmentList.Add(appointmentTwelveToThirteen);
                            }
                            if (item.ThirteenToFourteen == true)
                            {
                                AppointmentSchedule appointmentThirteenToFourteen = new()
                                {
                                    TimeRange = TimeRange.ThirteenToFourteen,
                                    Day = DateTime.Now.AddDays(i).Date,
                                    DoctorId = item.DoctorId
                                };

                                //appointment.TimeRange = TimeRange.ThirteenToFourteen;
                                //appointment.Day = DateTime.Now.AddDays(i).Date;
                                //await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                appointmentList.Add(appointmentThirteenToFourteen);
                            }
                            if (item.FourteenToFifteen == true)
                            {
                                AppointmentSchedule appointmentFourteenToFifteen = new()
                                {
                                    TimeRange = TimeRange.ThirteenToFourteen,
                                    Day = DateTime.Now.AddDays(i).Date,
                                    DoctorId = item.DoctorId
                                };

                                //appointment.TimeRange = TimeRange.FourteenToFifteen;
                                //appointment.Day = DateTime.Now.AddDays(i).Date;
                                //await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                appointmentList.Add(appointmentFourteenToFifteen);
                            }
                            if (item.FifteenToSixteen == true)
                            {

                                AppointmentSchedule appointmentFifteenToSixteen = new()
                                {
                                    TimeRange = TimeRange.FifteenToSixteen,
                                    Day = DateTime.Now.AddDays(i).Date,
                                    DoctorId = item.DoctorId
                                };
                                //appointment.TimeRange = TimeRange.FifteenToSixteen;
                                //appointment.Day = DateTime.Now.AddDays(i).Date;
                                //await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                appointmentList.Add(appointmentFifteenToSixteen);
                            }
                            if (item.SixteenToSeventeen == true)
                            {

                                AppointmentSchedule appointmentSixteenToSeventeen = new()
                                {
                                    TimeRange = TimeRange.SixteenToSeventeen,
                                    Day = DateTime.Now.AddDays(i).Date,
                                    DoctorId = item.DoctorId
                                };

                                //appointment.TimeRange = TimeRange.SixteenToSeventeen;
                                //appointment.Day = DateTime.Now.AddDays(i).Date;
                                //await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                appointmentList.Add(appointmentSixteenToSeventeen);
                            }
                        }
                        break;


                    case DayOfWeek.Friday:
                        var fridayDoctorSchedules = doctorSchedule.Where(s => s.Day == DayOfWeek.Friday).ToList();

                        foreach (var item in fridayDoctorSchedules)
                        {
                            AppointmentSchedule appointment = new()
                            {
                                //Day = DateTime.Now.AddDays(i).Date,
                                DoctorId = item.DoctorId
                            };

                            if (item.EightToNine == true)
                            {
                                appointment.TimeRange = TimeRange.EightToNine;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.NineToTen == true)
                            {
                                appointment.TimeRange = TimeRange.NineToTen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.TenToEleven == true)
                            {
                                appointment.TimeRange = TimeRange.TenToEleven;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                // appointmentList.Add(appointment);
                            }
                            if (item.ElevenToTwelve == true)
                            {
                                appointment.TimeRange = TimeRange.ElevenToTwelve;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);

                            }
                            if (item.TwelveToThirteen == true)
                            {
                                appointment.TimeRange = TimeRange.TwelveToThirteen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.ThirteenToFourteen == true)
                            {
                                appointment.TimeRange = TimeRange.ThirteenToFourteen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.FourteenToFifteen == true)
                            {
                                appointment.TimeRange = TimeRange.FourteenToFifteen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.FifteenToSixteen == true)
                            {
                                appointment.TimeRange = TimeRange.FifteenToSixteen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.SixteenToSeventeen == true)
                            {
                                appointment.TimeRange = TimeRange.SixteenToSeventeen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                        }
                        break;


                    case DayOfWeek.Saturday:
                        var saturdayDoctorSchedules = doctorSchedule.Where(s => s.Day == DayOfWeek.Saturday).ToList();

                        foreach (var item in saturdayDoctorSchedules)
                        {
                            AppointmentSchedule appointment = new()
                            {
                                //Day = DateTime.Now.AddDays(i).Date,
                                DoctorId = item.DoctorId
                            };

                            if (item.EightToNine == true)
                            {
                                appointment.TimeRange = TimeRange.EightToNine;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.NineToTen == true)
                            {
                                appointment.TimeRange = TimeRange.NineToTen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.TenToEleven == true)
                            {
                                appointment.TimeRange = TimeRange.TenToEleven;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.ElevenToTwelve == true)
                            {
                                appointment.TimeRange = TimeRange.ElevenToTwelve;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);

                            }
                            if (item.TwelveToThirteen == true)
                            {
                                appointment.TimeRange = TimeRange.TwelveToThirteen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;
                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.ThirteenToFourteen == true)
                            {
                                appointment.TimeRange = TimeRange.ThirteenToFourteen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;

                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);
                                //appointmentList.Add(appointment);
                            }
                            if (item.FourteenToFifteen == true)
                            {
                                appointment.TimeRange = TimeRange.FourteenToFifteen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;

                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.FifteenToSixteen == true)
                            {
                                appointment.TimeRange = TimeRange.FifteenToSixteen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;

                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);

                                //appointmentList.Add(appointment);
                            }
                            if (item.SixteenToSeventeen == true)
                            {
                                appointment.TimeRange = TimeRange.SixteenToSeventeen;
                                appointment.Day = DateTime.Now.AddDays(i).Date;

                                await _appointmentScheduleRepository.AddAppointmentSchedule(appointment);
                                // appointmentList.Add(appointment);
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

        public async Task<IEnumerable<object>> GetAllAppointmentSchedules()
        {
            return await _appointmentScheduleRepository.GetAllAppointmentSchedules();
        }
    }
}
