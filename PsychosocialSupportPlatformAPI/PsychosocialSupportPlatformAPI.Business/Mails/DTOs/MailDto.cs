﻿namespace PsychosocialSupportPlatformAPI.Business.Mails.DTOs
{
    public class MailDto
    {
        public required string PatientEmail { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
    }
}
