using PsychosocialSupportPlatformAPI.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.DataAccess.Appointments
{
    public class AppointmentRepository : IAppointmentRepository
    {
        public Task CreateAppointment(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAppointment(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public Task<Appointment> GetAppointment(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public Task<List<Appointment>> GetDoctorAppointments()
        {
            throw new NotImplementedException();
        }

        public Task<List<Appointment>> GetPatientAppointments()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAppointment(Appointment appointment)
        {
            throw new NotImplementedException();
        }
    }
}
