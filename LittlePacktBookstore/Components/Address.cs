using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittlePacktBookstore.Components
{
	public class Address: ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var addresses = new Models.Address();
			//{
			//	Countries = new List<SelectListItem>
			//	{
			//		new SelectListItem{Value="1", Text="Country 1"},
			//		new SelectListItem{Value="2", Text="Country 2"},
			//		new SelectListItem{Value="3", Text="Country 3"},
			//		new SelectListItem{Value="4", Text="Country 4"},
			//		new SelectListItem{Value="5", Text="Country 5"}
			//	}
			//};
			return View(addresses);
		}
	}
}
