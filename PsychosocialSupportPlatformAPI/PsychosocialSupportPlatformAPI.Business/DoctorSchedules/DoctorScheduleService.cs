using AutoMapper;
using PsychosocialSupportPlatformAPI.Business.DoctorSchedules.DTOs;
using PsychosocialSupportPlatformAPI.Business.Videos.DTOs;
using PsychosocialSupportPlatformAPI.DataAccess.DoctorSchedules;
using PsychosocialSupportPlatformAPI.Entity.Entities;

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

            foreach (var timeRange in createDoctorScheduleDTO.TimeRanges)
            {

            }
            
            doctorSchedule.DoctorId = currentUserID;

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
