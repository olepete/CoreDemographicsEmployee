using System;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Employees.Models.Public;

namespace WebAPI.Employees.Helpers
{
    public class LinkBuilder
    {
		public static PublicObject AddHateoas(IUrlHelper Url, PublicObject publicObject)
        {
			if (publicObject.Links == null)
				publicObject.Links = new System.Collections.Generic.List<Link>();


			publicObject.Links.Add(new Link
            {
                Rel = "self",
                Method = "GET",
                Uri = Url.Action("get", new { id = publicObject.Id })

            });

			publicObject.Links.Add(new Link
            {
                Rel = "update-action",
                Method = "PUT",
				Uri = Url.Action("put", new { id = publicObject.Id })
            });

			publicObject.Links.Add(new Link
            {
                Rel = "delete-action",
                Method = "DELETE",
				Uri = Url.Action("delete", new { id = publicObject.Id})
            });
            
            return publicObject;
        }

		public static PublicObject AddHateoasGet(IUrlHelper Url, PublicObject publicObject)
        {
            if (publicObject.Links == null)
                publicObject.Links = new System.Collections.Generic.List<Link>();


			publicObject.Links.Add(new Link
			{
				Rel = "self",
				Method = "GET",
				Uri = Url.Action("get", null)
            });

             
            return publicObject;
        }


    }
}
