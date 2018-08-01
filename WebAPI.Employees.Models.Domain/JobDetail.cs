using System;
namespace WebAPI.Employees.Models.Domain
{
    public class JobDetail
    {
		public string Office { get; set; }
		public string Department { get; set; }
		public string SupervisorId { get; set; }
		public DateTime StartDate { get; set; }
		public string JobTitle { get; set; }
		public string Description { get; set; }

    }
}
