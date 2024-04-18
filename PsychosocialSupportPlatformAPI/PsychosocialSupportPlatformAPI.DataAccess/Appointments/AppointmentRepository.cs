using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.Entity.Entities;

namespace PsychosocialSupportPlatformAPI.DataAccess.Appointments
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly PsychosocialSupportPlatformDBContext _context;
        public AppointmentRepository(PsychosocialSupportPlatformDBContext context)
        {

            _context = context;

        }
        public async Task CreatePatientAppointment(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePatientAppointment(Appointment appointment)
        {
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAllDoctorAppointments(string doctorID)
        {
            return await _context.Appointments.Where(a => a.DoctorId == doctorID).ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAllPatientAppointments(string patientID)
        {
            return await _context.Appointments.Where(a => a.PatientId == patientID).ToListAsync();
        }

        public async Task<Appointment> GetPatientAppointmentById(int appointmentID, string patientID)
        {
            return await _context.Appointments.Where(a => a.Id == appointmentID && a.PatientId == patientID).FirstOrDefaultAsync();
        }

        public async Task<Appointment> GetDoctorAppointmentById(int appointmentID, string doctorID)
        {
            return await _context.Appointments.Where(a => a.Id == appointmentID && a.DoctorId == doctorID).FirstOrDefaultAsync();

        }
    }
}
