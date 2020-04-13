using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LittlePacktBookstore.Models
{
	public class Address
	{
		//[Required]
		[Display(Name = "Address Line 1")]
		public string Address1 { get; set; }
		[DisplayName("Address Line 2")]
		public string Address2 { get; set; }
		//[Required]
		[MaxLength(3)]
		public string City { get; set; }
		//[Required]
		public string Zip { get; set; }
		public string State { get; set; }
		//[Required]
		public string Country { get; set; }
		public List<SelectListItem> Countries { get; set; }
	}
}
