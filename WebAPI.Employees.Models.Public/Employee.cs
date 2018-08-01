using System;
using System.Collections.Generic;
 

namespace WebAPI.Employees.Models.Public
{
	public class Employee : PublicObject
	{
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Title { get; set; }

		public Address HomeAddress { get; set; }
		public Address WorkAddress { get; set; }
		public Address ShippingAddress { get; set; }

		public string HomePhone { get; set; }
		public string WorkPhone { get; set; }
		public string SMSPhone { get; set; }

		public string EmailAddress { get; set; }
		public string Office { get; set; }
		public string Department { get; set; }
		public string JobTitle { get; set; }
		public string JobDescription { get; set; }
		public DateTime StartDate { get; set; }
		public string SupervisorId { get; set; }
	}
}
