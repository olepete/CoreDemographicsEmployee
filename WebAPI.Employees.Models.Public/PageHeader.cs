using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WebAPI.Employees.Models.Public
{
	public class PageHeader
	{
		public int Page { get; set; }
		public int PageSize { get; set; }

		public string ToJson() => JsonConvert.SerializeObject(this,
								    new JsonSerializerSettings
									{
										ContractResolver = new CamelCasePropertyNamesContractResolver()
									});
	}
}
