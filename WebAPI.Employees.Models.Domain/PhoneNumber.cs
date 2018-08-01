using System;
namespace WebAPI.Employees.Models.Domain
{
    public class PhoneNumber
    {
		public PhoneNumberTypes PhoneNumberType { get; set; }
		public string Number { get; set; }
    }
}
