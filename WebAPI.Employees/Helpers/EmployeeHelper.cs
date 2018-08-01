using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Employees.Models.Domain;
using WebAPI.Employees.Models.Public;

namespace WebAPI.Employees.Helpers
{
    internal class EmployeeHelper
	{
		public static WebAPI.Employees.Models.Public.Employee ConvertToPublicObject(WebAPI.Employees.Models.Domain.InternalEmployee model)
		{
			var employee = new WebAPI.Employees.Models.Public.Employee()
			{
				EmailAddress = model.EmailAddress,
				FirstName = model.EmployeeName.FirstName,
				MiddleName = model.EmployeeName.MiddleName,
				LastName = model.EmployeeName.LastName,
				Title = model.EmployeeName.Title,
				Id = model.PublicId
			};

			var job = model.JobDetails.OrderByDescending(m => m.StartDate).Take(1).FirstOrDefault();
			employee.Department = job.Department;
			employee.JobDescription = job.Description;
			employee.JobTitle = job.JobTitle;
			employee.Office = job.Office;
			employee.StartDate = job.StartDate;
			employee.Office = job.Office;
			employee.SMSPhone = GetPhone(Models.Domain.PhoneNumberTypes.SMS, model);
			employee.WorkPhone = GetPhone(Models.Domain.PhoneNumberTypes.Work, model);
			employee.HomePhone = GetPhone(Models.Domain.PhoneNumberTypes.Home, model);
			employee.HomeAddress = GetAddress(Models.Domain.AddressTypes.Home, model);
			employee.WorkAddress = GetAddress(Models.Domain.AddressTypes.Work, model);

			return employee;
		}

		      
        public static WebAPI.Employees.Models.Domain.InternalEmployee ConvertToInternalObject(WebAPI.Employees.Models.Public.Employee model)
		{
			var employee = new WebAPI.Employees.Models.Domain.InternalEmployee()
			{
				EmailAddress = model.EmailAddress,
				PublicId = model.Id,
				EmployeeName = new Models.Domain.Name()
				{
					FirstName = model.FirstName,
					MiddleName = model.MiddleName,
					LastName = model.LastName,
					Title = model.Title
				},
                JobDetails = new System.Collections.Generic.List<Models.Domain.JobDetail>()
				{
					new Models.Domain.JobDetail()
					{
						Department = model.Department,
                        Description = model.JobDescription,
                        JobTitle = model.JobTitle,
                        Office = model.Office,
						StartDate = model.StartDate,
                        SupervisorId = model.SupervisorId
					}
				},
                PhoneNumbers = new System.Collections.Generic.List<Models.Domain.PhoneNumber>(),
                Addresses = new System.Collections.Generic.List<Models.Domain.Address>()
			};

			if (!String.IsNullOrEmpty(model.WorkPhone))
				employee.PhoneNumbers.Add(new Models.Domain.PhoneNumber() { PhoneNumberType = Models.Domain.PhoneNumberTypes.Work, Number = model.WorkPhone });
			if (!String.IsNullOrEmpty(model.HomePhone))
                employee.PhoneNumbers.Add(new Models.Domain.PhoneNumber() { PhoneNumberType = Models.Domain.PhoneNumberTypes.Home, Number = model.HomePhone });
			if (!String.IsNullOrEmpty(model.SMSPhone))
				employee.PhoneNumbers.Add(new Models.Domain.PhoneNumber() { PhoneNumberType = Models.Domain.PhoneNumberTypes.SMS, Number = model.SMSPhone });

            if( model.WorkAddress != null )
			{
				employee.Addresses.Add(new Models.Domain.Address()
				{
					AddressType = Models.Domain.AddressTypes.Work,
					AddressLine1 = model.WorkAddress.AddressLine1,
					AddressLine2 = model.WorkAddress.AddressLine2,
					AddressLine3 = model.WorkAddress.AddressLine3,
					City = model.WorkAddress.City,
					CountryCode = model.WorkAddress.CountryCode,
					PostalCode = model.WorkAddress.PostalCode,
					StateProvince = model.WorkAddress.StateProvince
				});
			}

            if( model.HomeAddress != null )
			{
				employee.Addresses.Add(new Models.Domain.Address()
                {
                    AddressType = Models.Domain.AddressTypes.Work,
					AddressLine1 = model.HomeAddress.AddressLine1,
					AddressLine2 = model.HomeAddress.AddressLine2,
					AddressLine3 = model.HomeAddress.AddressLine3,
					City = model.HomeAddress.City,
					CountryCode = model.HomeAddress.CountryCode,
					PostalCode = model.HomeAddress.PostalCode,
					StateProvince = model.HomeAddress.StateProvince
                });
			}

			return employee;
		}

        private static string GetPhone( Models.Domain.PhoneNumberTypes type, Models.Domain.InternalEmployee employee )
		{
			var phone = employee.PhoneNumbers.Where(p => p.PhoneNumberType == type).FirstOrDefault();
			return phone == null ? String.Empty : phone.Number;
		}

		private static Models.Public.Address GetAddress(Models.Domain.AddressTypes type, Models.Domain.InternalEmployee employee)
		{
			Models.Public.Address address = null;
			var sourceAddress = employee.Addresses.Where(a => a.AddressType == type).FirstOrDefault();
			if (sourceAddress != null)
			{
				address = new Models.Public.Address()
				{
					AddressLine1 = sourceAddress.AddressLine1,
					AddressLine2 = sourceAddress.AddressLine2,
					AddressLine3 = sourceAddress.AddressLine3,
					City = sourceAddress.City,
					CountryCode = sourceAddress.CountryCode,
					PostalCode = sourceAddress.PostalCode,
					StateProvince = sourceAddress.StateProvince
				};
			}
			return address;
		}

		public static Models.Domain.InternalEmployee MergeExisting( Models.Domain.InternalEmployee existingRecord, Models.Domain.InternalEmployee model )
		{
			//merge the historical lists

			//does the current job passed in exist, key off startdate
			if (model.JobDetails != null)
			{
				var currentJob = model.JobDetails.FirstOrDefault();
				if (currentJob != null)
				{
					model.JobDetails.AddRange(existingRecord.JobDetails.Where(j => j.StartDate != currentJob.StartDate));
				}
				else
				{
					model.JobDetails.AddRange(existingRecord.JobDetails);
				}
			}

            model.ActionLogs.AddRange(existingRecord.ActionLogs);
            model.ActionLogs.Add(new ActionLog() { ActionDate = DateTime.Now, ActionDescription = "Updated object" });

			return model;
		}
    }
}
