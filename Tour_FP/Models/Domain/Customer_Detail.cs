using System;
using System.ComponentModel.DataAnnotations;

namespace Tour_FP.Models.Domain
{
	public class CustomerDetail
	{
		[Key]
		public int CustomerId { get; set; }

		[Required(ErrorMessage = "Please enter your full name.")]
		public string? FullName { get; set; }

		[Required(ErrorMessage = "Please enter your email address.")]
		[EmailAddress(ErrorMessage = "Please enter a valid email address.")]
		public string? Email { get; set; }

		[Required(ErrorMessage = "Please enter your phone number.")]
		[Phone(ErrorMessage = "Please enter a valid phone number.")]
		public string? PhoneNumber { get; set; }

		[Required(ErrorMessage = "Please enter your address.")]
		public string? Address { get; set; }

		[Required(ErrorMessage = "Please enter the number of adults.")]
		[Range(1, int.MaxValue, ErrorMessage = "Please select at least one adult.")]
		public int NumberOfAdults { get; set; }

		[Range(0, int.MaxValue, ErrorMessage = "Please select a valid number of children.")]
		public int NumberOfChildren { get; set; }

		public DateTime PreferredStartDate { get; set; }

		public DateTime PreferredEndDate { get; set; }

		public string? SpecialRequests { get; set; }

		// Additional fields can be added as needed, such as emergency contact details.
	}
}
