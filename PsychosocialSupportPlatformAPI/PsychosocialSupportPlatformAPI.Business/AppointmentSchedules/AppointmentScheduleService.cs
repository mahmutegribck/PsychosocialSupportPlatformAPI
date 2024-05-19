using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PsychosocialSupportPlatformAPI.Business.Appointments.DTOs.Doctor;
using PsychosocialSupportPlatformAPI.Business.Mails;
using PsychosocialSupportPlatformAPI.Business.Mails.DTOs;
using PsychosocialSupportPlatformAPI.DataAccess.AppointmentSchedules;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.Business.AppointmentSchedules
{
    public class AppointmentScheduleService : IAppointmentScheduleService
    {
        private readonly IAppointmentScheduleRepository _appointmentScheduleRepository;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;
        private readonly UserManager<Doctor> _doctorManager;


        public AppointmentScheduleService(
            IAppointmentScheduleRepository appointmentScheduleRepository,
            IMapper mapper,
            IMailService mailService,
             UserManager<Doctor> doctorManager)
        {
            _appointmentScheduleRepository = appointmentScheduleRepository;
            _mapper = mapper;
            _mailService = mailService;
            _doctorManager = doctorManager;
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
                    TimeRange = TimeRange.FourteenToFifteen,
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
            if (doctorSchedule == null) throw new Exception("Kayıtlı Doktor Randevu Takvimi Bulunamadı.");

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
                            if (doctorSchedule.EightToNine && !appointmentSchedules.Any(a => a.TimeRange == timeRange) && !createAppointmentList.Any(a => a.TimeRange == timeRange))
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
                            if (doctorSchedule.NineToTen && !appointmentSchedules.Any(a => a.TimeRange == timeRange) && !createAppointmentList.Any(a => a.TimeRange == timeRange))
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
                            if (doctorSchedule.TenToEleven && !appointmentSchedules.Any(a => a.TimeRange == timeRange) && !createAppointmentList.Any(a => a.TimeRange == timeRange))
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
                            if (doctorSchedule.ElevenToTwelve && !appointmentSchedules.Any(a => a.TimeRange == timeRange) && !createAppointmentList.Any(a => a.TimeRange == timeRange))
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
                            if (doctorSchedule.TwelveToThirteen && !appointmentSchedules.Any(a => a.TimeRange == timeRange) && !createAppointmentList.Any(a => a.TimeRange == timeRange))
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
                            if (doctorSchedule.ThirteenToFourteen && !appointmentSchedules.Any(a => a.TimeRange == timeRange) && !createAppointmentList.Any(a => a.TimeRange == timeRange))
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
                            if (doctorSchedule.FourteenToFifteen && !appointmentSchedules.Any(a => a.TimeRange == timeRange) && !createAppointmentList.Any(a => a.TimeRange == timeRange))
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
                            if (doctorSchedule.FifteenToSixteen && !appointmentSchedules.Any(a => a.TimeRange == timeRange) && !createAppointmentList.Any(a => a.TimeRange == timeRange))
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
                            if (doctorSchedule.SixteenToSeventeen && !appointmentSchedules.Any(a => a.TimeRange == timeRange) && !createAppointmentList.Any(a => a.TimeRange == timeRange))
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
                foreach (AppointmentSchedule deleteAppointment in deleteAppointmentList)
                {
                    if (deleteAppointment.PatientId != null)
                    {
                        await _mailService.CancelAppointmentSendEmailToPatient(deleteAppointment);
                    }
                }
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

        public async Task<IEnumerable<object>> AllDoctorAppointments(string doctorId)
        {
            IEnumerable<AppointmentSchedule> doctorAppointments = await _appointmentScheduleRepository.AllDoctorAppointments(doctorId);

            if (!doctorAppointments.Any()) throw new Exception("Doktorun Hasta Randevusu Bulunamadı.");

            var groupedDoctorAppointments = doctorAppointments.GroupBy(d => d.Day)
                .Select(group => new
                {
                    AppointmentDay = group.Key.ToShortDateString(),
                    Patients = group.Select(p => new
                    {
                        AppointmentId = p.Id,
                        PatientId = p.PatientId,
                        PatientName = p.Patient!.Name,
                        PatientSurname = p.Patient.Surname,
                        AppointmentTimeRange = p.TimeRange,
                        AppointmentUrl = p.URL
                    })
                });

            return groupedDoctorAppointments;
        }

        public async Task<IEnumerable<object>> GetAllDoctorAppointmentsByPatientId(string patientId, string doctorId)
        {
            IEnumerable<AppointmentSchedule> doctorAppointments = await _appointmentScheduleRepository.GetAllDoctorAppointmentsByPatientId(patientId, doctorId);

            if (!doctorAppointments.Any()) throw new Exception("Doktorun Hasta ile Randevusu Bulunamadı.");



            var groupedDoctorAppointments = doctorAppointments.GroupBy(d => d.Day)
                .Select(group => new
                {
                    AppointmentDay = group.Key.ToShortDateString(),
                    Patients = group.Select(p => new
                    {
                        AppointmentId = p.Id,
                        PatientId = p.PatientId,
                        PatientName = p.Patient!.Name,
                        PatientSurname = p.Patient.Surname,
                        AppointmentTimeRange = p.TimeRange,
                        AppointmentUrl = p.URL
                    })
                });

            return groupedDoctorAppointments;
        }

        public async Task<IEnumerable<object>> GetAllPastDoctorAppointmentsByPatientSlug(string patientSlug, string doctorId)
        {
            IEnumerable<AppointmentSchedule> doctorAppointments = await _appointmentScheduleRepository.GetAllPastDoctorAppointmentsByPatientSlug(patientSlug, doctorId);

            if (!doctorAppointments.Any()) throw new Exception("Doktorun Hasta ile Randevusu Bulunamadı.");

            var groupedDoctorAppointments = doctorAppointments.GroupBy(d => d.PatientId).Select(group => new
            {
                PatientId = group.Key,
                PatientName = group.First().Patient.Name,
                PatientSurname = group.First().Patient.Surname,
                PatientProfileImageUrl = group.First().Patient.ProfileImageUrl,
                Appointments = group.SelectMany(d => d.Patient.AppointmentSchedules.GroupBy(a => a.Day)).Select(innerGroup => new
                {
                    AppointmentDay = innerGroup.Key.ToShortDateString(),
                    AppointmentDayTimeRange = innerGroup.Select(t => new
                    {
                        AppointmentId = t.Id,
                        AppointmentTimeRange = t.TimeRange

                    }).ToList(),
                }).ToList()
            });

            return groupedDoctorAppointments;
        }


        public async Task<IEnumerable<GetDoctorAppointmentDTO>> GetAllDoctorAppointmentsByDate(DateTime date, string doctorId)
        {
            IEnumerable<AppointmentSchedule> doctorAppointments = await _appointmentScheduleRepository.GetAllDoctorAppointmentsByDate(date, doctorId);

            if (!doctorAppointments.Any()) throw new Exception("Doktorun Bu Tarihte Randevusu Bulunamadı.");

            return _mapper.Map<IEnumerable<GetDoctorAppointmentDTO>>(doctorAppointments);

        }


    }
}
