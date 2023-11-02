﻿using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.DAL.Models
{
	public class Document
	{
		[Key]
		public Guid Id { get; set; }

		public string Name { get; set; }
	}
}
