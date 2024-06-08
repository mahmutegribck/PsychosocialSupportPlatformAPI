using AutoMapper;
using PsychosocialSupportPlatformAPI.Business.AppointmentSchedules;
using PsychosocialSupportPlatformAPI.Business.DoctorSchedules.DTOs;
using PsychosocialSupportPlatformAPI.DataAccess.DoctorSchedules;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.Business.DoctorSchedules
{
    public class DoctorScheduleService : IDoctorScheduleService
    {
        private readonly IDoctorScheduleRepository _doctorScheduleRepository;
        private readonly IAppointmentScheduleService _appointmentScheduleService;
        private readonly IMapper _mapper;

        public DoctorScheduleService(
            IDoctorScheduleRepository doctorScheduleRepository,
            IAppointmentScheduleService appointmentScheduleService,
            IMapper mapper)
        {
            _doctorScheduleRepository = doctorScheduleRepository;
            _appointmentScheduleService = appointmentScheduleService;
            _mapper = mapper;
        }


        public async Task AddDoctorSchedule(List<CreateDoctorScheduleDTO> createDoctorScheduleDTOs, string currentUserId, CancellationToken cancellationToken)
        {
            if (!createDoctorScheduleDTOs.Any() || currentUserId == null) throw new Exception();

            foreach (var createDoctorScheduleDTO in createDoctorScheduleDTOs)
            {
                if (!createDoctorScheduleDTO.TimeRanges.Any()) throw new Exception();

                DateTime day = DateTime.Parse(createDoctorScheduleDTO.Day);
                if (day < DateTime.Now.Date || day > DateTime.Now.Date.AddDays(14))
                    throw new Exception();

                DoctorSchedule doctorSchedule = _mapper.Map<DoctorSchedule>(createDoctorScheduleDTO);
                doctorSchedule.DoctorId = currentUserId;

                DoctorSchedule? existingSchedule = await _doctorScheduleRepository.GetDoctorScheduleByDay(currentUserId, doctorSchedule.Day, cancellationToken);

                foreach (var timeRange in createDoctorScheduleDTO.TimeRanges)
                {
                    switch (timeRange)
                    {
                        case TimeRange.EightToNine:
                            doctorSchedule.EightToNine = true;
                            break;
                        case TimeRange.NineToTen:
                            doctorSchedule.NineToTen = true;
                            break;
                        case TimeRange.TenToEleven:
                            doctorSchedule.TenToEleven = true;
                            break;
                        case TimeRange.ElevenToTwelve:
                            doctorSchedule.ElevenToTwelve = true;
                            break;
                        case TimeRange.TwelveToThirteen:
                            doctorSchedule.TwelveToThirteen = true;
                            break;
                        case TimeRange.ThirteenToFourteen:
                            doctorSchedule.ThirteenToFourteen = true;
                            break;
                        case TimeRange.FourteenToFifteen:
                            doctorSchedule.FourteenToFifteen = true;
                            break;
                        case TimeRange.FifteenToSixteen:
                            doctorSchedule.FifteenToSixteen = true;
                            break;
                        case TimeRange.SixteenToSeventeen:
                            doctorSchedule.SixteenToSeventeen = true;
                            break;
                        default:
                            // Belirtilen saat aralığı tanımlı değilse, hata fırlatılabilir veya atlanabilir.
                            break;
                    }

                }
                if (existingSchedule != null)
                {
                    doctorSchedule.Id = existingSchedule.Id;
                    await _doctorScheduleRepository.UpdateDoctorSchedule(doctorSchedule, cancellationToken);
                    await _appointmentScheduleService.UpdateAppointmentSchedule(doctorSchedule, cancellationToken);
                }
                else
                {
                    await _doctorScheduleRepository.CreateDoctorSchedule(doctorSchedule, cancellationToken);
                    await _appointmentScheduleService.AddAppointmentSchedule(doctorSchedule, cancellationToken);
                }
            }
        }


        public async Task UpdateDoctorSchedule(UpdateDoctorScheduleDTO updateDoctorScheduleDTO, string currentUserId, CancellationToken cancellationToken)
        {
            if (updateDoctorScheduleDTO == null || currentUserId == null) throw new ArgumentNullException();

            DateTime day = DateTime.Parse(updateDoctorScheduleDTO.Day);
            if (day < DateTime.Now.Date || day > DateTime.Now.Date.AddDays(14)) throw new Exception();

            DoctorSchedule? existingSchedule = await _doctorScheduleRepository.GetDoctorScheduleByDay(currentUserId, day, cancellationToken);
            if (existingSchedule == null) throw new Exception("Girilen Gune Ait Takvim Kaydi Mevcut Degil.");

            DoctorSchedule doctorSchedule = _mapper.Map<DoctorSchedule>(updateDoctorScheduleDTO);
            doctorSchedule.DoctorId = currentUserId;

            await _doctorScheduleRepository.UpdateDoctorSchedule(doctorSchedule, cancellationToken);
            await _appointmentScheduleService.UpdateAppointmentSchedule(doctorSchedule, cancellationToken);
        }


        public async Task DeleteDoctorSchedule(string doctorId, int scheduleId, CancellationToken cancellationToken)
        {
            if (doctorId == null || scheduleId < 0) throw new ArgumentNullException();

            DoctorSchedule? doctorSchedule = await _doctorScheduleRepository.GetDoctorScheduleById(doctorId, scheduleId, cancellationToken);

            if (doctorSchedule == null) throw new Exception();

            await _doctorScheduleRepository.DeleteDoctorSchedule(doctorSchedule, cancellationToken);
            await _appointmentScheduleService.DeleteAppointmentSchedule(doctorId, doctorSchedule.Day, cancellationToken);
        }


        public async Task<GetDoctorScheduleDTO?> GetDoctorScheduleByDay(string doctorId, DateTime day, CancellationToken cancellationToken)
        {
            if (doctorId == null) throw new ArgumentNullException();

            return _mapper.Map<GetDoctorScheduleDTO?>(await _doctorScheduleRepository.GetDoctorScheduleByDay(doctorId, day, cancellationToken));
        }


        public async Task<GetDoctorScheduleDTO?> GetDoctorScheduleById(string doctorId, int scheduleId, CancellationToken cancellationToken) 
        {
            if (doctorId == null || scheduleId < 0) throw new ArgumentNullException();

            return _mapper.Map<GetDoctorScheduleDTO?>(await _doctorScheduleRepository.GetDoctorScheduleById(doctorId, scheduleId, cancellationToken));
        }


        public async Task<IEnumerable<GetDoctorScheduleDTO?>> GetAllDoctorScheduleById(string doctorId, CancellationToken cancellationToken)
        {
            if (doctorId == null) throw new ArgumentNullException();

            return _mapper.Map<IEnumerable<GetDoctorScheduleDTO?>>(await _doctorScheduleRepository.GetAllDoctorScheduleById(doctorId, cancellationToken));
        }


        public async Task<IEnumerable<object>> GetAllDoctorSchedulesByDate(DateTime day, CancellationToken cancellationToken)
        {
            IEnumerable<GetDoctorScheduleByAdminDTO?> allDoctorSchedules = _mapper.Map<IEnumerable<GetDoctorScheduleByAdminDTO?>>(await _doctorScheduleRepository.GetAllDoctorSchedulesByDate(day, cancellationToken));

            if (!allDoctorSchedules.Any()) throw new Exception();

            IEnumerable<object> groupedSchedules = allDoctorSchedules
                .GroupBy(dto => DateTime.Parse(dto!.Day))
                .OrderBy(group => group.Key)
                .Select(group =>
                {
                    return new
                    {
                        Day = group.Key.ToShortDateString(),
                        DoctorSchedules = group.Select(dto =>
                            new GetDoctorScheduleByAdminDTO
                            {
                                Id = dto!.Id,
                                Day = dto.Day,
                                EightToNine = dto.EightToNine,
                                NineToTen = dto.NineToTen,
                                TenToEleven = dto.TenToEleven,
                                ElevenToTwelve = dto.ElevenToTwelve,
                                TwelveToThirteen = dto.TwelveToThirteen,
                                ThirteenToFourteen = dto.ThirteenToFourteen,
                                FourteenToFifteen = dto.FourteenToFifteen,
                                FifteenToSixteen = dto.FifteenToSixteen,
                                SixteenToSeventeen = dto.SixteenToSeventeen,
                                DoctorId = dto.DoctorId,
                                DoctorName = dto.DoctorName,
                                DoctorSurname = dto.DoctorSurname,
                                DoctorTitle = dto.DoctorTitle,
                                DoctorProfileImageUrl = dto.DoctorProfileImageUrl
                            }).ToList()
                    };
                }).ToList();

            return groupedSchedules;
        }
    }
}
