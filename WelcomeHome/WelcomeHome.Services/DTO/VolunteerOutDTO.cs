﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelcomeHome.Services.DTO
{
    public record VolunteerOutDTO
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; } 

        public string Email { get; set; }

        public string SocialUrl { get; set; }

        public Guid? EstablishmentId { get; set; }

    }
}
