using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.DataAccess.DataSeeding;
using PsychosocialSupportPlatformAPI.Entity.Entities;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
using PsychosocialSupportPlatformAPI.Entity.Entities.Messages;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using PsychosocialSupportPlatformAPI.Entity.Entities.Videos;
using System.Reflection.Emit;

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
        public DbSet<AppointmentStatistics> AppointmentStatistics { get; set; }
        public DbSet<DoctorTitle> DoctorTitles { get; set; }



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

            builder.Entity<Doctor>()
                .Navigation(d => d.DoctorTitle)
                .AutoInclude();

            
            //builder.Entity<AppointmentStatistics>()
            //.HasOne(a => a.AppointmentSchedule)
            //.WithMany(s => s.AppointmentStatistics)
            //.HasForeignKey(a => a.AppointmentScheduleId)
            //.OnDelete(DeleteBehavior.Cascade); // On delete cascade for AppointmentStatistics

            //builder.Entity<AppointmentStatistics>()
            //    .HasOne(a => a.Patient)
            //    .WithMany(p => p.AppointmentStatistics)
            //    .HasForeignKey(a => a.PatientId)
            //    .OnDelete(DeleteBehavior.Cascade); // On delete cascade for AppointmentStatistics

            //builder.Entity<AppointmentStatistics>()
            //    .HasOne(a => a.Doctor)
            //    .WithMany(d => d.AppointmentStatistics)
            //    .HasForeignKey(a => a.DoctorId)
            //    .OnDelete(DeleteBehavior.Cascade);


            //builder.Entity<Doctor>()
            //  .HasMany(d => d.AppointmentStatistics)
            //  .WithOne(a => a.Doctor)
            //  .HasForeignKey(a => a.DoctorId)
            //  .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Patient>()
              .HasMany(p => p.AppointmentStatistics)
              .WithOne(p => p.Patient)
              .HasForeignKey(p => p.PatientId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AppointmentSchedule>()
               .HasMany(a => a.AppointmentStatistics)
               .WithOne(a => a.AppointmentSchedule)
               .HasForeignKey(a => a.AppointmentScheduleId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AppointmentStatistics>(entity =>
            {
                entity.HasOne(s => s.Doctor)
                .WithMany(s => s.AppointmentStatistics)
                .HasForeignKey(s => s.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
            });


        }
    }
}
