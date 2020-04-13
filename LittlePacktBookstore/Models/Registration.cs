using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LittlePacktBookstore.Models
{
	public class Registration
	{
		public int Id { get; set; }
		//[Required]
		public string FirstName { get; set; }
		//[Required]
		public string LastName { get; set; }
		[Required]
		[EmailAddress(ErrorMessage ="Invalid email address")]
		[DataType(DataType.EmailAddress)]
		//[Remote("CheckEmail", "Home")]
		public string Email { get; set; }

		public Address MailingAddress { get; set; }
	}
}
