using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WebAPI.Employees.Models.Public
{
    public class PublicObject
    {
		public String Id { get; set; }

		[JsonProperty(PropertyName = "_links")]
        public List<Link> Links { get; set; }

    }
}
