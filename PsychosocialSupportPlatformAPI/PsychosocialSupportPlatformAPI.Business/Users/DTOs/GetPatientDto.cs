﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.Business.Users.DTOs
{
    public class GetPatientDto
    {

        public required string Id { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string? ProfileImageUrl { get; set; }
        public required string PhoneNumber { get; set; }

    }
}
