using System;
using System.Collections.Generic;

namespace WebAPI.Employees.Models.Domain
{
    public class InternalEmployee
    {
		public Object Id { get; set; }
        public string PublicId { get; set; }

		public Name EmployeeName { get; set; }
		public List<JobDetail> JobDetails { get; set; }
		public List<Address> Addresses { get; set; }
		public List<PhoneNumber> PhoneNumbers { get; set; }
		public string EmailAddress { get; set; }
		public bool IsActive { get; set; }
		public List<ActionLog> ActionLogs { get; set; }
    }
}
