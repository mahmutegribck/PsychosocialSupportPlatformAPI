﻿namespace PsychosocialSupportPlatformAPI.Business.Users.DTOs
{
    public class GetDoctorDto
    {
        public required string Id { get; set; }
        public required string Email { get; set; }
        public required string Title { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string? ProfileImageUrl { get; set; }
        public required string PhoneNumber { get; set; }
    }
}
