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

            for (int i = 0; i < 14; i++)
            {
                //switch ((DayOfWeek)(((int)startDay + i) % 7))
                //{

                //    case DayOfWeek.Sunday:
                //        await GetAppointmentSchedules(doctorSchedule, DayOfWeek.Sunday, i);
                //        break;


                //    case DayOfWeek.Monday:
                //        await GetAppointmentSchedules(doctorSchedule, DayOfWeek.Monday, i);
                //        break;


                //    case DayOfWeek.Tuesday:
                //        await GetAppointmentSchedules(doctorSchedule, DayOfWeek.Tuesday, i);
                //        break;


                //    case DayOfWeek.Wednesday:
                //        await GetAppointmentSchedules(doctorSchedule, DayOfWeek.Wednesday, i);
                //        break;


                //    case DayOfWeek.Thursday:
                //        await GetAppointmentSchedules(doctorSchedule, DayOfWeek.Thursday, i);
                //        break;


                //    case DayOfWeek.Friday:
                //        await GetAppointmentSchedules(doctorSchedule, DayOfWeek.Friday, i);
                //        break;


                //    case DayOfWeek.Saturday:
                //        await GetAppointmentSchedules(doctorSchedule, DayOfWeek.Saturday, i);
                //        break;

                //    default:
                //        return;

                //}
            }
        }

        public Task DeleteAppointmentSchedule(int appointmentScheduleId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<object>> GetAllAppointmentSchedules(DateTime day)
        {
            return await _appointmentScheduleRepository.GetAllAppointmentSchedules(day);
        }

        private async Task GetAppointmentSchedules(IEnumerable<DoctorSchedule> doctorSchedules, DateTime day, int index)
        {
            List<AppointmentSchedule> appointmentList = new List<AppointmentSchedule>();

            var thursdayDoctorSchedules = doctorSchedules.Where(s => s.Day == day).ToList();

            foreach (var item in thursdayDoctorSchedules)
            {
                if (item.EightToNine == true)
                {
                    AppointmentSchedule appointmentEightToNine = new()
                    {
                        TimeRange = TimeRange.EightToNine,
                        Day = DateTime.Now.AddDays(index).Date,
                        DoctorId = item.DoctorId
                    };

                    appointmentList.Add(appointmentEightToNine);
                }
                if (item.NineToTen == true)
                {
                    AppointmentSchedule appointmentNineToTen = new()
                    {
                        TimeRange = TimeRange.NineToTen,
                        Day = DateTime.Now.AddDays(index).Date,
                        DoctorId = item.DoctorId
                    };

                    appointmentList.Add(appointmentNineToTen);
                }
                if (item.TenToEleven == true)
                {
                    AppointmentSchedule appointmentTenToEleven = new()
                    {
                        TimeRange = TimeRange.TenToEleven,
                        Day = DateTime.Now.AddDays(index).Date,
                        DoctorId = item.DoctorId
                    };

                    appointmentList.Add(appointmentTenToEleven);
                }
                if (item.ElevenToTwelve == true)
                {
                    AppointmentSchedule appointmentElevenToTwelve = new()
                    {
                        TimeRange = TimeRange.ElevenToTwelve,
                        Day = DateTime.Now.AddDays(index).Date,
                        DoctorId = item.DoctorId
                    };

                    appointmentList.Add(appointmentElevenToTwelve);

                }
                if (item.TwelveToThirteen == true)
                {
                    AppointmentSchedule appointmentTwelveToThirteen = new()
                    {
                        TimeRange = TimeRange.TwelveToThirteen,
                        Day = DateTime.Now.AddDays(index).Date,
                        DoctorId = item.DoctorId
                    };

                    appointmentList.Add(appointmentTwelveToThirteen);
                }
                if (item.ThirteenToFourteen == true)
                {
                    AppointmentSchedule appointmentThirteenToFourteen = new()
                    {
                        TimeRange = TimeRange.ThirteenToFourteen,
                        Day = DateTime.Now.AddDays(index).Date,
                        DoctorId = item.DoctorId
                    };

                    appointmentList.Add(appointmentThirteenToFourteen);
                }
                if (item.FourteenToFifteen == true)
                {
                    AppointmentSchedule appointmentFourteenToFifteen = new()
                    {
                        TimeRange = TimeRange.ThirteenToFourteen,
                        Day = DateTime.Now.AddDays(index).Date,
                        DoctorId = item.DoctorId
                    };

                    appointmentList.Add(appointmentFourteenToFifteen);
                }
                if (item.FifteenToSixteen == true)
                {
                    AppointmentSchedule appointmentFifteenToSixteen = new()
                    {
                        TimeRange = TimeRange.FifteenToSixteen,
                        Day = DateTime.Now.AddDays(index).Date,
                        DoctorId = item.DoctorId
                    };

                    appointmentList.Add(appointmentFifteenToSixteen);
                }
                if (item.SixteenToSeventeen == true)
                {

                    AppointmentSchedule appointmentSixteenToSeventeen = new()
                    {
                        TimeRange = TimeRange.SixteenToSeventeen,
                        Day = DateTime.Now.AddDays(index).Date,
                        DoctorId = item.DoctorId
                    };

                    appointmentList.Add(appointmentSixteenToSeventeen);
                }
            }
            await _appointmentScheduleRepository.AddAppointmentScheduleList(appointmentList);
        }
    }
}
