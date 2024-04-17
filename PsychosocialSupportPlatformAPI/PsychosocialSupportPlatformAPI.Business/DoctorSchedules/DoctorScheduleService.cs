using AutoMapper;
using PsychosocialSupportPlatformAPI.Business.DoctorSchedules.DTOs;
using PsychosocialSupportPlatformAPI.Business.Videos.DTOs;
using PsychosocialSupportPlatformAPI.DataAccess.DoctorSchedules;
using PsychosocialSupportPlatformAPI.Entity.Entities;
using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.Business.DoctorSchedules
{
    public class DoctorScheduleService : IDoctorScheduleService
    {
        private readonly IDoctorScheduleRepository _doctorScheduleRepository;
        private readonly IMapper _mapper;
        public DoctorScheduleService(IDoctorScheduleRepository doctorScheduleRepository, IMapper mapper)
        {
            _doctorScheduleRepository = doctorScheduleRepository;
            _mapper = mapper;
        }

        public async Task CreateDoctorSchedule(CreateDoctorScheduleDTO createDoctorScheduleDTO, string currentUserID)
        {
            var doctorSchedule = _mapper.Map<DoctorSchedule>(createDoctorScheduleDTO);
            doctorSchedule.DoctorId = currentUserID;

            if (createDoctorScheduleDTO.Day < DayOfWeek.Sunday || createDoctorScheduleDTO.Day > DayOfWeek.Saturday)
                throw new Exception();


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
        }

        public async Task DeleteDoctorSchedule(int doctorScheduleId)
        {
            await _doctorScheduleRepository.DeleteDoctorSchedule(doctorScheduleId);
        }

        public async Task<IEnumerable<GetDoctorScheduleDTO>> GetAllDoctorScheduleById(string doctorId)
        {
            return _mapper.Map<List<GetDoctorScheduleDTO>>(await _doctorScheduleRepository.GetAllDoctorScheduleById(doctorId));
        }

        public async Task<GetDoctorScheduleDTO> GetDoctorScheduleById(string doctorId, int scheduleId)
        {
            return _mapper.Map<GetDoctorScheduleDTO>(await _doctorScheduleRepository.GetDoctorScheduleById(doctorId, scheduleId));
        }

        public async Task UpdateDoctorSchedule(UpdateDoctorScheduleDTO updateDoctorScheduleDTO, string currentUserID)
        {
            var doctorSchedule = _mapper.Map<DoctorSchedule>(updateDoctorScheduleDTO);
            doctorSchedule.DoctorId = currentUserID;
            await _doctorScheduleRepository.UpdateDoctorSchedule(doctorSchedule);
        }
    }
}
     