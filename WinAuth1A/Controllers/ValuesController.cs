using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WinAuth1A.Controllers
{
    public class ValuesController : ApiController
    {

		public string Get(string id)
		{
			return "Test " + id;
		}

		[HttpPost]
		public string Post([FromBody] FourDetails details)
		{
			return details.Item1 + " " + details.Item2 + " " + details.Item3 + " " + details.Item4 + " ";
		}

	}


	public class FourDetails
	{
		public string Item1 { get; set; }
		public string Item2 { get; set; }
		public string Item3 { get; set; }
		public string Item4 { get; set; }

	}
}
