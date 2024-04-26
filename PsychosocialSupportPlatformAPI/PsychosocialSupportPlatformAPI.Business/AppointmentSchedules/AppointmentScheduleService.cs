using AutoMapper;
using PsychosocialSupportPlatformAPI.Business.AppointmentSchedules.DTOs;
using PsychosocialSupportPlatformAPI.DataAccess.AppointmentSchedules;
using PsychosocialSupportPlatformAPI.DataAccess.DoctorSchedules;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.Business.AppointmentSchedules
{
    public class AppointmentScheduleService : IAppointmentScheduleService
    {
        private readonly IAppointmentScheduleRepository _appointmentScheduleRepository;

        public AppointmentScheduleService(IAppointmentScheduleRepository appointmentScheduleRepository, IDoctorScheduleRepository doctorScheduleRepository, IMapper mapper)
        {
            _appointmentScheduleRepository = appointmentScheduleRepository;
        }


        public async Task AddAppointmentSchedule(DoctorSchedule doctorSchedule)
        {
            if (doctorSchedule == null) throw new Exception();

            List<AppointmentSchedule> appointmentList = new List<AppointmentSchedule>();

            if (doctorSchedule.EightToNine == true)
            {
                AppointmentSchedule appointmentEightToNine = new()
                {
                    TimeRange = TimeRange.EightToNine,
                    Day = doctorSchedule.Day,
                    DoctorId = doctorSchedule.DoctorId
                };

                appointmentList.Add(appointmentEightToNine);
            }
            if (doctorSchedule.NineToTen == true)
            {
                AppointmentSchedule appointmentNineToTen = new()
                {
                    TimeRange = TimeRange.NineToTen,
                    Day = doctorSchedule.Day,
                    DoctorId = doctorSchedule.DoctorId
                };

                appointmentList.Add(appointmentNineToTen);
            }
            if (doctorSchedule.TenToEleven == true)
            {
                AppointmentSchedule appointmentTenToEleven = new()
                {
                    TimeRange = TimeRange.TenToEleven,
                    Day = doctorSchedule.Day,
                    DoctorId = doctorSchedule.DoctorId
                };

                appointmentList.Add(appointmentTenToEleven);
            }
            if (doctorSchedule.ElevenToTwelve == true)
            {
                AppointmentSchedule appointmentElevenToTwelve = new()
                {
                    TimeRange = TimeRange.ElevenToTwelve,
                    Day = doctorSchedule.Day,
                    DoctorId = doctorSchedule.DoctorId
                };

                appointmentList.Add(appointmentElevenToTwelve);

            }
            if (doctorSchedule.TwelveToThirteen == true)
            {
                AppointmentSchedule appointmentTwelveToThirteen = new()
                {
                    TimeRange = TimeRange.TwelveToThirteen,
                    Day = doctorSchedule.Day,
                    DoctorId = doctorSchedule.DoctorId
                };

                appointmentList.Add(appointmentTwelveToThirteen);
            }
            if (doctorSchedule.ThirteenToFourteen == true)
            {
                AppointmentSchedule appointmentThirteenToFourteen = new()
                {
                    TimeRange = TimeRange.ThirteenToFourteen,
                    Day = doctorSchedule.Day,
                    DoctorId = doctorSchedule.DoctorId
                };

                appointmentList.Add(appointmentThirteenToFourteen);
            }
            if (doctorSchedule.FourteenToFifteen == true)
            {
                AppointmentSchedule appointmentFourteenToFifteen = new()
                {
                    TimeRange = TimeRange.ThirteenToFourteen,
                    Day = doctorSchedule.Day,
                    DoctorId = doctorSchedule.DoctorId
                };

                appointmentList.Add(appointmentFourteenToFifteen);
            }
            if (doctorSchedule.FifteenToSixteen == true)
            {
                AppointmentSchedule appointmentFifteenToSixteen = new()
                {
                    TimeRange = TimeRange.FifteenToSixteen,
                    Day = doctorSchedule.Day,
                    DoctorId = doctorSchedule.DoctorId
                };

                appointmentList.Add(appointmentFifteenToSixteen);
            }
            if (doctorSchedule.SixteenToSeventeen == true)
            {

                AppointmentSchedule appointmentSixteenToSeventeen = new()
                {
                    TimeRange = TimeRange.SixteenToSeventeen,
                    Day = doctorSchedule.Day,
                    DoctorId = doctorSchedule.DoctorId
                };

                appointmentList.Add(appointmentSixteenToSeventeen);
            }

            await _appointmentScheduleRepository.AddAppointmentScheduleList(appointmentList);
        }


        public async Task UpdateAppointmentSchedule(DoctorSchedule doctorSchedule)
        {
            if (doctorSchedule == null) throw new Exception();

            List<AppointmentSchedule> createAppointmentList = new List<AppointmentSchedule>();
            List<AppointmentSchedule> deleteAppointmentList = new List<AppointmentSchedule>();

            IEnumerable<AppointmentSchedule> appointmentSchedules = await _appointmentScheduleRepository.GetAppointmentScheduleByDay(doctorSchedule.DoctorId, doctorSchedule.Day);
            if (!appointmentSchedules.Any())
            {
                await AddAppointmentSchedule(doctorSchedule);
            }
            foreach (var appointmentSchedule in appointmentSchedules)
            {
                for (TimeRange timeRange = TimeRange.EightToNine; timeRange < TimeRange.SixteenToSeventeen + 1; timeRange++)
                {
                    switch (timeRange)
                    {
                        case TimeRange.EightToNine:
                            if (doctorSchedule.EightToNine && appointmentSchedule.TimeRange != timeRange)
                            {
                                AppointmentSchedule appointmentEightToNine = new()
                                {
                                    TimeRange = TimeRange.EightToNine,
                                    Day = doctorSchedule.Day,
                                    DoctorId = doctorSchedule.DoctorId
                                };
                                createAppointmentList.Add(appointmentEightToNine);
                            }
                            else if (!doctorSchedule.EightToNine && appointmentSchedule.TimeRange == timeRange)
                            {
                                deleteAppointmentList.Add(appointmentSchedule);

                            }
                            break;

                        case TimeRange.NineToTen:
                            if (doctorSchedule.NineToTen && appointmentSchedule.TimeRange != timeRange)
                            {
                                AppointmentSchedule appointmentNineToTen = new()
                                {
                                    TimeRange = TimeRange.NineToTen,
                                    Day = doctorSchedule.Day,
                                    DoctorId = doctorSchedule.DoctorId
                                };
                                createAppointmentList.Add(appointmentNineToTen);
                            }
                            else if (!doctorSchedule.NineToTen && appointmentSchedule.TimeRange == timeRange)
                            {
                                deleteAppointmentList.Add(appointmentSchedule);

                            }
                            break;

                        case TimeRange.TenToEleven:
                            if (doctorSchedule.TenToEleven && appointmentSchedule.TimeRange != timeRange)
                            {
                                AppointmentSchedule appointmentTenToEleven = new()
                                {
                                    TimeRange = TimeRange.TenToEleven,
                                    Day = doctorSchedule.Day,
                                    DoctorId = doctorSchedule.DoctorId
                                };
                                createAppointmentList.Add(appointmentTenToEleven);
                            }
                            else if (!doctorSchedule.TenToEleven && appointmentSchedule.TimeRange == timeRange)
                            {
                                deleteAppointmentList.Add(appointmentSchedule);
                            }
                            break;

                        case TimeRange.ElevenToTwelve:
                            if (doctorSchedule.ElevenToTwelve && appointmentSchedule.TimeRange != timeRange)
                            {
                                AppointmentSchedule appointmentElevenToTwelve = new()
                                {
                                    TimeRange = TimeRange.ElevenToTwelve,
                                    Day = doctorSchedule.Day,
                                    DoctorId = doctorSchedule.DoctorId
                                };
                                createAppointmentList.Add(appointmentElevenToTwelve);
                            }
                            else if (!doctorSchedule.ElevenToTwelve && appointmentSchedule.TimeRange == timeRange)
                            {
                                deleteAppointmentList.Add(appointmentSchedule);
                            }
                            break;

                        case TimeRange.TwelveToThirteen:
                            if (doctorSchedule.TwelveToThirteen && appointmentSchedule.TimeRange != timeRange)
                            {
                                AppointmentSchedule appointmentTwelveToThirteen = new()
                                {
                                    TimeRange = TimeRange.TwelveToThirteen,
                                    Day = doctorSchedule.Day,
                                    DoctorId = doctorSchedule.DoctorId
                                };
                                createAppointmentList.Add(appointmentTwelveToThirteen);
                            }
                            else if (!doctorSchedule.TwelveToThirteen && appointmentSchedule.TimeRange == timeRange)
                            {
                                deleteAppointmentList.Add(appointmentSchedule);
                            }
                            break;

                        case TimeRange.ThirteenToFourteen:
                            if (doctorSchedule.ThirteenToFourteen && appointmentSchedule.TimeRange != timeRange)
                            {
                                AppointmentSchedule appointmentThirteenToFourteen = new()
                                {
                                    TimeRange = TimeRange.ThirteenToFourteen,
                                    Day = doctorSchedule.Day,
                                    DoctorId = doctorSchedule.DoctorId
                                };
                                createAppointmentList.Add(appointmentThirteenToFourteen);
                            }
                            else if (!doctorSchedule.ThirteenToFourteen && appointmentSchedule.TimeRange == timeRange)
                            {
                                deleteAppointmentList.Add(appointmentSchedule);
                            }
                            break;

                        case TimeRange.FourteenToFifteen:
                            if (doctorSchedule.FourteenToFifteen && appointmentSchedule.TimeRange != timeRange)
                            {
                                AppointmentSchedule appointmentFourteenToFifteen = new()
                                {
                                    TimeRange = TimeRange.FourteenToFifteen,
                                    Day = doctorSchedule.Day,
                                    DoctorId = doctorSchedule.DoctorId
                                };
                                createAppointmentList.Add(appointmentFourteenToFifteen);
                            }
                            else if (!doctorSchedule.FourteenToFifteen && appointmentSchedule.TimeRange == timeRange)
                            {
                                deleteAppointmentList.Add(appointmentSchedule);
                            }
                            break;

                        case TimeRange.FifteenToSixteen:
                            if (doctorSchedule.FifteenToSixteen && appointmentSchedule.TimeRange != timeRange)
                            {
                                AppointmentSchedule appointmentFifteenToSixteen = new()
                                {
                                    TimeRange = TimeRange.FifteenToSixteen,
                                    Day = doctorSchedule.Day,
                                    DoctorId = doctorSchedule.DoctorId
                                };
                                createAppointmentList.Add(appointmentFifteenToSixteen);
                            }
                            else if (!doctorSchedule.FifteenToSixteen && appointmentSchedule.TimeRange == timeRange)
                            {
                                deleteAppointmentList.Add(appointmentSchedule);
                            }
                            break;

                        case TimeRange.SixteenToSeventeen:
                            if (doctorSchedule.SixteenToSeventeen && appointmentSchedule.TimeRange != timeRange)
                            {
                                AppointmentSchedule appointmentSixteenToSeventeen = new()
                                {
                                    TimeRange = TimeRange.SixteenToSeventeen,
                                    Day = doctorSchedule.Day,
                                    DoctorId = doctorSchedule.DoctorId
                                };
                                createAppointmentList.Add(appointmentSixteenToSeventeen);
                            }
                            else if (!doctorSchedule.SixteenToSeventeen && appointmentSchedule.TimeRange == timeRange)
                            {
                                deleteAppointmentList.Add(appointmentSchedule);
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
            if (deleteAppointmentList.Any())
            {
                await _appointmentScheduleRepository.DeleteAppointmentScheduleList(deleteAppointmentList);

            }
            if (createAppointmentList.Any())
            {
                await _appointmentScheduleRepository.AddAppointmentScheduleList(createAppointmentList);
            }
        }


        public async Task DeleteAppointmentSchedule(string doctorId, DateTime day)
        {
            if (doctorId == null) throw new Exception(); 

            IEnumerable<AppointmentSchedule> appointmentSchedules = await _appointmentScheduleRepository.GetAppointmentScheduleByDay(doctorId, day);
            await _appointmentScheduleRepository.DeleteAppointmentScheduleList(appointmentSchedules);
        }


        public async Task<IEnumerable<object>> GetAllAppointmentSchedules(DateTime day)
        {
            return await _appointmentScheduleRepository.GetAllAppointmentSchedules(day);
        }
    }
}
