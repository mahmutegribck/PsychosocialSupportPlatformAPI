using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;

namespace PsychosocialSupportPlatformAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<Doctor> _userManagerDoctor;
        private readonly UserManager<Patient> _patientManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<Doctor> userManagerDoctor, IMapper mapper, UserManager<Patient> patientManager)
        {
            _userManagerDoctor = userManagerDoctor;
            _mapper = mapper;
            _patientManager = patientManager;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = _mapper.Map<List<GetDoctorDto>>(await _userManagerDoctor.Users.ToListAsync());
            if (doctors.Count != 0)
            {
                return Ok(doctors);

            }
            return NotFound("Kullanici Bulunamadi");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPAtients()
        {
            var patients = _mapper.Map<List<GetPatientDto>>(await _patientManager.Users.ToListAsync());
            if (patients.Count != 0)
            {
                return Ok(patients);

            }
            return NotFound("Kullanici Bulunamadi");
        }

    }
}
