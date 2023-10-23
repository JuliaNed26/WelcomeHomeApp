﻿namespace WelcomeHome.DAL.Models
{
    public class Country
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<City>? Cities { get; set; }
    }
}