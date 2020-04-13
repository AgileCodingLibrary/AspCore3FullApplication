using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittlePacktBookstore.Models
{
	public class Country
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public static List<SelectListItem> GetCountrySelectList(IEnumerable<Country> Countries)
		{
			return Countries.Select(x => new SelectListItem()
			{
				Value = x.Id.ToString(),
				Text = x.Name
			}).ToList();
		}

	}
}
