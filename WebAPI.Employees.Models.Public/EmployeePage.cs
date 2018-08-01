using System;
using System.Collections.Generic;

namespace WebAPI.Employees.Models.Public
{
	public class EmployeePage : PublicObject
	{
		public PageHeader PageHeader {get;set;}
		public List<Employee> Employees { get; set; }
	}
}
