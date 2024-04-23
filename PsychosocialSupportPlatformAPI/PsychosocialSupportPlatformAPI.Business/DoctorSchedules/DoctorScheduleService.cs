using AutoMapper;
using PsychosocialSupportPlatformAPI.Business.DoctorSchedules.DTOs;
using PsychosocialSupportPlatformAPI.DataAccess.DoctorSchedules;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
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

        public async Task CreateDoctorSchedule(CreateDoctorScheduleDTO[] createDoctorScheduleDTOs, string currentUserID)
        {
            foreach (var createDoctorScheduleDTO in createDoctorScheduleDTOs)
            {
                DoctorSchedule doctorSchedule = _mapper.Map<DoctorSchedule>(createDoctorScheduleDTO);
                doctorSchedule.DoctorId = currentUserID;

                DoctorSchedule existingSchedule = await _doctorScheduleRepository.GetDoctorScheduleByDay(currentUserID, doctorSchedule.Day);
                if (existingSchedule != null) throw new Exception("Girilen Gune Ait Takvim Kaydi Mevcut.");

                if (createDoctorScheduleDTO.Day < DateTime.Now.Date || createDoctorScheduleDTO.Day > DateTime.Now.Date.AddDays(14))
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
        }

        public async Task UpdateDoctorSchedule(UpdateDoctorScheduleDTO updateDoctorScheduleDTO, string currentUserID)
        {
            DoctorSchedule doctorSchedule = _mapper.Map<DoctorSchedule>(updateDoctorScheduleDTO);
            doctorSchedule.DoctorId = currentUserID;

            DoctorSchedule existingSchedule = await _doctorScheduleRepository.GetDoctorScheduleByDay(currentUserID, doctorSchedule.Day);
            if (existingSchedule == null) throw new Exception("Girilen Gune Ait Takvim Kaydi Mevcut Degil.");

            if (updateDoctorScheduleDTO.Day < DateTime.Now.Date || updateDoctorScheduleDTO.Day > DateTime.Now.Date.AddDays(14))
                throw new Exception();

            await _doctorScheduleRepository.UpdateDoctorSchedule(doctorSchedule);
        }

        public async Task DeleteDoctorSchedule(string doctorId, int scheduleId)
        {
            DoctorSchedule doctorSchedule = await _doctorScheduleRepository.GetDoctorScheduleById(doctorId, scheduleId) ?? throw new ArgumentNullException();
            await _doctorScheduleRepository.DeleteDoctorSchedule(doctorSchedule);
        }

        public async Task<GetDoctorScheduleDTO> GetDoctorScheduleByDay(string doctorId, DateTime day)
        {
            return _mapper.Map<GetDoctorScheduleDTO>(await _doctorScheduleRepository.GetDoctorScheduleByDay(doctorId, day));
        }

        public async Task<GetDoctorScheduleDTO> GetDoctorScheduleById(string doctorId, int scheduleId)
        {
            return _mapper.Map<GetDoctorScheduleDTO>(await _doctorScheduleRepository.GetDoctorScheduleById(doctorId, scheduleId));
        }

        public async Task<IEnumerable<GetDoctorScheduleDTO>> GetAllDoctorScheduleById(string doctorId)
        {
            return _mapper.Map<List<GetDoctorScheduleDTO>>(await _doctorScheduleRepository.GetAllDoctorScheduleById(doctorId));
        }

        public async Task<IEnumerable<GetDoctorScheduleDTO>> GetAllDoctorSchedule()
        {
            return _mapper.Map<List<GetDoctorScheduleDTO>>(await _doctorScheduleRepository.GetAllDoctorSchedule());
        }
    }
}
