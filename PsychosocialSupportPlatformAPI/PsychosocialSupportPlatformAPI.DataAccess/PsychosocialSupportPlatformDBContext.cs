using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.Entity.Entities;
using PsychosocialSupportPlatformAPI.Entity.Entities.Messages;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.DataAccess
{
    public class PsychosocialSupportPlatformDBContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {

        public PsychosocialSupportPlatformDBContext(DbContextOptions<PsychosocialSupportPlatformDBContext> options) : base(options) { }

        public DbSet<Message> Messages { get; set; }

        public DbSet<MessageOutbox> MessageOutboxes { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

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
        }
    }
}
