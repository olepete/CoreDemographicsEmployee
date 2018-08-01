using System;
using System.Collections.Generic;

namespace WebAPI.Employees.Models.Public
{
	public class Link
	{
		public string Rel { get; set; }
		public string Method { get; set; }
		public string Uri { get; set; }
		//public string Href { get; set; }
	}
}
