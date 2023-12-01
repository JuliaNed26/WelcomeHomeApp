﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.Services.DTO
{
    public record EstablishmentTypeOutDTO
    {
        public int Id { get; init; }
        public string Name { get; init; } 
    }
}
