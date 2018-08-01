using System;
using System.Collections.Generic;

namespace WebAPI.Employees.Create_Database
{
    class Program
    {
		static void Main(string[] args)
		{
			Console.WriteLine("Creating employee records");
			generatedIds = new List<int>();

			var serializer = new
				WebAPI.Employees.Models.Serialization.EmployeeSerializer("<YOUR MONGO URL>"
                                                                             , "demographics", "employees");
			
			for (int x = 0; x < 20000; x++ )
			{
				var employee = new WebAPI.Employees.Models.Domain.InternalEmployee()
				{
					Id = MongoDB.Bson.ObjectId.GenerateNewId(),
					PublicId = GetRandomId(),
                    ActionLogs = new List<Models.Domain.ActionLog>()
					{
						new Models.Domain.ActionLog()
						{
							ActionDate = DateTime.Now,
                            ActionDescription = "Created"
						}
					},
                    IsActive = true,
                    EmployeeName = new Models.Domain.Name()
					{
						FirstName = Faker.Name.First(),
                        MiddleName = Faker.Name.First(),
                        LastName = Faker.Name.Last(),
                        Title = Faker.Name.Prefix()
					},
                    EmailAddress = Faker.Internet.Email(),
                    PhoneNumbers = new List<Models.Domain.PhoneNumber>()
					{
						new Models.Domain.PhoneNumber()
						{
							Number = Faker.Phone.Number(),
                            PhoneNumberType = Models.Domain.PhoneNumberTypes.Home
						},
                        new Models.Domain.PhoneNumber()
						{
							Number=Faker.Phone.Number(),
                            PhoneNumberType = Models.Domain.PhoneNumberTypes.Work
						},
                        new Models.Domain.PhoneNumber()
						{
							Number = Faker.Phone.Number(),
                            PhoneNumberType = Models.Domain.PhoneNumberTypes.SMS
						}
					},
                    JobDetails = new List<Models.Domain.JobDetail>()
					{
						new Models.Domain.JobDetail()
						{
							Department = Faker.Lorem.GetFirstWord(),
                            Description = Faker.Lorem.Sentence(),
							JobTitle = Faker.Lorem.GetFirstWord(),
							Office = Convert.ToString(Faker.RandomNumber.Next(1000, 1005)),
                            StartDate = GetRandomDate(),
                            SupervisorId = GetRandomEmployee()
						}
					},
                    Addresses = new List<Models.Domain.Address>()
					{
						new Models.Domain.Address()
						{
							AddressLine1 = Faker.Address.StreetAddress(),
                            AddressLine2 = Faker.Address.SecondaryAddress(),
                            AddressType = Models.Domain.AddressTypes.Work,
                            City = Faker.Address.City(),
                            CountryCode = "US",
                            PostalCode = Faker.Address.ZipCode(),
                            StateProvince = Faker.Address.UsStateAbbr()
						},
						new Models.Domain.Address()
                        {
                            AddressLine1 = Faker.Address.StreetAddress(),
                            AddressLine2 = Faker.Address.SecondaryAddress(),
                            AddressType = Models.Domain.AddressTypes.Home,
                            City = Faker.Address.City(),
                            CountryCode = "US",
                            PostalCode = Faker.Address.ZipCode(),
                            StateProvince = Faker.Address.UsStateAbbr()
                        }
					}

				};
				var newEmployee = serializer.AddEmployee(employee);
				Console.WriteLine($"Created #{x} with id of {newEmployee.Id}");
			}
			generatedIds = null;
			Console.WriteLine("Finished creating employees");
		}

        private static DateTime GetRandomDate()
		{
			DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
		}

        private static string GetRandomEmployee()
		{
			if (generatedIds.Count < 5)
				return String.Empty;//add some high level employees
			
			var offset = gen.Next(1, generatedIds.Count);
			return Convert.ToString(generatedIds[offset]);
		}

        private static string GetRandomId()
		{
			var id = Faker.RandomNumber.Next(100000, 999999);
            while( generatedIds.Contains(id) ) 
			{
				id = Faker.RandomNumber.Next(100000, 999999);
			}

			generatedIds.Add(id);
			return Convert.ToString(id);
		}

		private static Random gen = new Random();
		private static List<int> generatedIds;
    }
}
