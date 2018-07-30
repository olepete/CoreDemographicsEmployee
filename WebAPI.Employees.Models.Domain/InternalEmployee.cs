using System;
using System.Collections.Generic;

namespace WebAPI.Employees.Models.Domain
{
    public class InternalEmployee
    {
		public string Id { get; set; }


		public bool IsActive { get; set; }
		public List<ActionLog> ActionLogs { get; set; }
    }
}
