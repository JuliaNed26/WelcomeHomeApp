﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelcomeHome.Services.DTO
{
    public record UserInDTO
    {
        [Required]
        public string FullName { get; set; } = null!;
        [Required, Phone]
        public string PhoneNumber { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
