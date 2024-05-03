using AutoMapper;
using PsychosocialSupportPlatformAPI.Business.AppointmentSchedules;
using PsychosocialSupportPlatformAPI.Business.DoctorSchedules.DTOs;
using PsychosocialSupportPlatformAPI.DataAccess.DoctorSchedules;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
using PsychosocialSupportPlatformAPI.Entity.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PsychosocialSupportPlatformAPI.Business.DoctorSchedules
{
    public class DoctorScheduleService : IDoctorScheduleService
    {
        private readonly IDoctorScheduleRepository _doctorScheduleRepository;
        private readonly IAppointmentScheduleService _appointmentScheduleService;
        private readonly IMapper _mapper;

        public DoctorScheduleService(IDoctorScheduleRepository doctorScheduleRepository, IAppointmentScheduleService appointmentScheduleService, IMapper mapper)
        {
            _doctorScheduleRepository = doctorScheduleRepository;
            _appointmentScheduleService = appointmentScheduleService;
            _mapper = mapper;
        }



        public async Task CreateDoctorSchedule(List<CreateDoctorScheduleDTO> createDoctorScheduleDTOs, string currentUserID)
        {
            if (!createDoctorScheduleDTOs.Any() || currentUserID == null) throw new Exception();

            foreach (var createDoctorScheduleDTO in createDoctorScheduleDTOs)
            {
                if (!createDoctorScheduleDTO.TimeRanges.Any()) throw new Exception();

                DateTime day = DateTime.Parse(createDoctorScheduleDTO.Day);
                if (day < DateTime.Now.Date || day > DateTime.Now.Date.AddDays(14))
                    throw new Exception();

                DoctorSchedule doctorSchedule = _mapper.Map<DoctorSchedule>(createDoctorScheduleDTO);
                doctorSchedule.DoctorId = currentUserID;

                DoctorSchedule? existingSchedule = await _doctorScheduleRepository.GetDoctorScheduleByDay(currentUserID, doctorSchedule.Day);
                if (existingSchedule != null) throw new Exception("Girilen Gune Ait Takvim Kaydi Mevcut.");

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
                await _doctorScheduleRepository.CreateDoctorSchedule(doctorSchedule);
                await _appointmentScheduleService.AddAppointmentSchedule(doctorSchedule);
            }
        }


        public async Task UpdateDoctorSchedule(UpdateDoctorScheduleDTO updateDoctorScheduleDTO, string currentUserID)
        {
            if (updateDoctorScheduleDTO == null || currentUserID == null) throw new ArgumentNullException();

            DateTime day = DateTime.Parse(updateDoctorScheduleDTO.Day);
            if (day < DateTime.Now.Date || day > DateTime.Now.Date.AddDays(14)) throw new Exception();

            DoctorSchedule? existingSchedule = await _doctorScheduleRepository.GetDoctorScheduleByDay(currentUserID, day);
            if (existingSchedule == null) throw new Exception("Girilen Gune Ait Takvim Kaydi Mevcut Degil.");

            DoctorSchedule doctorSchedule = _mapper.Map<DoctorSchedule>(updateDoctorScheduleDTO);
            doctorSchedule.DoctorId = currentUserID;

            await _doctorScheduleRepository.UpdateDoctorSchedule(doctorSchedule);
            await _appointmentScheduleService.UpdateAppointmentSchedule(doctorSchedule);
        }


        public async Task DeleteDoctorSchedule(string doctorId, int scheduleId)
        {
            if (doctorId == null || scheduleId < 0) throw new ArgumentNullException();

            DoctorSchedule? doctorSchedule = await _doctorScheduleRepository.GetDoctorScheduleById(doctorId, scheduleId);

            if (doctorSchedule == null) throw new Exception();

            await _doctorScheduleRepository.DeleteDoctorSchedule(doctorSchedule);
            await _appointmentScheduleService.DeleteAppointmentSchedule(doctorId, doctorSchedule.Day);
        }


        public async Task<GetDoctorScheduleDTO?> GetDoctorScheduleByDay(string doctorId, DateTime day)
        {
            if (doctorId == null) throw new ArgumentNullException();

            return _mapper.Map<GetDoctorScheduleDTO?>(await _doctorScheduleRepository.GetDoctorScheduleByDay(doctorId, day));
        }


        public async Task<GetDoctorScheduleDTO?> GetDoctorScheduleById(string doctorId, int scheduleId)
        {
            if (doctorId == null || scheduleId < 0) throw new ArgumentNullException();

            return _mapper.Map<GetDoctorScheduleDTO?>(await _doctorScheduleRepository.GetDoctorScheduleById(doctorId, scheduleId));
        }


        public async Task<IEnumerable<GetDoctorScheduleDTO?>> GetAllDoctorScheduleById(string doctorId)
        {
            if (doctorId == null) throw new ArgumentNullException();

            return _mapper.Map<IEnumerable<GetDoctorScheduleDTO?>>(await _doctorScheduleRepository.GetAllDoctorScheduleById(doctorId));
        }


        public async Task<IEnumerable<object>> GetAllDoctorSchedules()
        {
            IEnumerable<GetDoctorScheduleByAdminDTO?> allDoctorSchedules = _mapper.Map<IEnumerable<GetDoctorScheduleByAdminDTO?>>(await _doctorScheduleRepository.GetAllDoctorSchedules());
            if (!allDoctorSchedules.Any()) throw new Exception();

            IEnumerable<object> groupedSchedules = allDoctorSchedules.GroupBy(dto => DateTime.Parse(dto!.Day)).OrderBy(group => group.Key)
                .Select(group =>
                {
                    return new
                    {
                        Day = group.Key.ToShortDateString(),
                        DoctorSchedules = group.Select(dto =>
                            new GetDoctorScheduleByAdminDTO
                            {
                                Id = dto.Id,
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
