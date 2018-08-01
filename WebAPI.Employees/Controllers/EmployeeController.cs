using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPI.Employees.Models.Public;
using WebAPI.Employees.Models.Serialization;

namespace WebAPI.Employees.Controllers
{
	[Route("api/[controller]")]
	public class EmployeeController : Controller
    {
		public EmployeeController( IEmployeeSerializer employeeSerializer )
		{
			this.serializer = employeeSerializer;
		}
        
		IEmployeeSerializer serializer;
		// GET api/Employee
        
		[HttpGet("list/{page=1}/{size=20}")]
        public IActionResult Get(int page, int size)
		{
			var totalItems = serializer.GetCount();
   
			if (page > Math.Ceiling( (totalItems / (decimal) size)))
				return NotFound();
			
			var objects = serializer.GetEmployees(page, size).Select(l => Helpers.LinkBuilder.AddHateoas(Url, Helpers.EmployeeHelper.ConvertToPublicObject(l))).ToList();
            
			var employeePage = new EmployeePage
			{
				Id = Convert.ToString(page),
				PageHeader = new PageHeader
				{
					Page = page,
					PageSize = size,
				},
				Employees = objects.Cast<Employee>().ToList()
		    };
			
			employeePage = (EmployeePage) Helpers.LinkBuilder.AddHateoasGet(Url, employeePage);
			Response.Headers.Add("X-Pagination", employeePage.PageHeader.ToJson() );
			return Ok(employeePage);
        }
        
		// GET api/Employee/5
		[HttpGet("{id}")]
		public IActionResult Get(string id)
        {
			if (String.IsNullOrEmpty(id))
				return BadRequest();

			var employee = serializer.GetEmployee(id);
			var externalEmployee = Helpers.LinkBuilder.AddHateoas(Url, Helpers.EmployeeHelper.ConvertToPublicObject(employee));
			return Ok(externalEmployee);
        }

		// POST api/Employee
        [HttpPost]
		public IActionResult Post([FromBody]WebAPI.Employees.Models.Public.Employee model)
        {
			if (model == null)
				return BadRequest();

			if (serializer.EmployeeExists(model.Id))
				return BadRequest("Employee exists");
			
			var employee = serializer.AddEmployee(Helpers.EmployeeHelper.ConvertToInternalObject(model));
			var externalEmployee = Helpers.LinkBuilder.AddHateoas(Url, Helpers.EmployeeHelper.ConvertToPublicObject(employee));
            
			return Created("api/Employee", externalEmployee);
        }

		// PUT api/Employee/5
        [HttpPut("{id}")]
		public IActionResult Put(string id, [FromBody]WebAPI.Employees.Models.Public.Employee employee)
        {
			if (String.IsNullOrEmpty(id))
				return BadRequest();
			if (employee != null)
				return BadRequest();
			if (id != employee.Id)
				return BadRequest();

			if (!serializer.EmployeeExists(id))
				return NotFound();

			var internalEmployee = Helpers.EmployeeHelper.ConvertToInternalObject(employee);
			//merge the persist lists
			var existingObject = serializer.GetEmployee(id);
			Helpers.EmployeeHelper.MergeExisting(existingObject, internalEmployee);

			serializer.UpdateEmployee(id, internalEmployee);
			return NoContent();
        }

		// DELETE api/Employee/5
        [HttpDelete("{id}")]
		public IActionResult Delete(string id)
        {
			if (String.IsNullOrEmpty(id))
                return BadRequest();

			if (!serializer.EmployeeExists(id))
				return NotFound();

			return NoContent();
        }
        
    }
}
