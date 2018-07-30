using System;
using System.Collections.Generic;
using WebAPI.Employees.Models.Domain;

namespace WebAPI.Employes.Models.Serialization
{
    public interface IEmployeeSerializer
    {
		InternalEmployee GetEmployee(string id);
		List<InternalEmployee> GetEmployees();
		InternalEmployee AddEmployee(InternalEmployee model);
		void UpdateEmployee(string id, InternalEmployee model);
		void DeleteEmployee(string id);
		bool EmployeeExists(string id);
    }
}
