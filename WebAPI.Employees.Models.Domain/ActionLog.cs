using System;
namespace WebAPI.Employees.Models.Domain
{
    public class ActionLog
    {
		public DateTime ActionDate { get; set; }
		public string ActionDescription { get; set; }
    }
}
