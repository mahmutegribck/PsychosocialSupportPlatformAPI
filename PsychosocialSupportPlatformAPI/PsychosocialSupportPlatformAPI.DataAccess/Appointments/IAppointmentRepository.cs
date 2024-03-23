using PsychosocialSupportPlatformAPI.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.DataAccess.Appointments
{
    public interface IAppointmentRepository
    {
        Task CreateAppointment(Appointment appointment);
        Task DeleteAppointment(Appointment appointment);
        Task UpdateAppointment(Appointment appointment);
        Task<Appointment> GetAppointment(Appointment appointment);
        Task<List<Appointment>> GetPatientAppointments();
        Task<List<Appointment>> GetDoctorAppointments();
    }
}
