using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.DataAccess.DataSeeding;
using PsychosocialSupportPlatformAPI.Entity.Entities;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
using PsychosocialSupportPlatformAPI.Entity.Entities.Messages;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using PsychosocialSupportPlatformAPI.Entity.Entities.Videos;

namespace PsychosocialSupportPlatformAPI.DataAccess
{
    public class PsychosocialSupportPlatformDBContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public PsychosocialSupportPlatformDBContext(DbContextOptions<PsychosocialSupportPlatformDBContext> options) : base(options) { }

        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageOutbox> MessageOutboxes { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<VideoStatistics> VideoStatistics { get; set; }
        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
        public DbSet<AppointmentSchedule> AppointmentSchedules { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedData.Seeding(builder);

            builder.Entity<Doctor>().ToTable("Doctors");
            builder.Entity<Patient>().ToTable("Patients");

            builder.Entity<Message>()
            .HasOne(m => m.Sender)
            .WithMany(u => u.SentMessages)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AppointmentSchedule>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.AppointmentSchedules)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AppointmentSchedule>()
               .HasOne(a => a.Patient)
               .WithMany(p => p.AppointmentSchedules)
               .HasForeignKey(a => a.PatientId)
               .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Doctor>()
               .HasMany(d => d.DoctorSchedules) 
               .WithOne(a => a.Doctor) 
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
