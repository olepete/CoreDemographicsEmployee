using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Employees.Models.Public
{
    public class PagedRequest
    {
		[Required()]
		public int PageNumber { get; set; }
        [Required()]
		public int PageCount { get; set; }
    }
}
