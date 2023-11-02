﻿using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.DAL.Models
{
	public class Step
	{
		[Key]
		public Guid Id { get; set; }

		public string Description { get; set; }

		public Guid EstablishmentTypeId { get; set; }

		public EstablishmentType EstablishmentType { get; set; }

		public ICollection<StepDocument> StepDocuments { get; set; }
	}
}
