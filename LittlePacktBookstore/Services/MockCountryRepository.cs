using LittlePacktBookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittlePacktBookstore.Services
{
	public class MockCountryRepository : IRepository<Country>
	{
		public bool Add(Country item)
		{
			throw new NotImplementedException();
		}

		public bool Delete(Country Item)
		{
			throw new NotImplementedException();
		}

		public bool Edit(Country item)
		{
			throw new NotImplementedException();
		}

		public Country Get(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Country> GetAll()
		{
			return new List<Country>
			{
				new Country{Id=1,Name="Country1" },
				new Country{Id=2,Name="Country2" },
				new Country{Id=3,Name="Country3" },
				new Country{Id=4,Name="Country4" },
				new Country{Id=5,Name="Country5" },
			};
		}
	}
}
