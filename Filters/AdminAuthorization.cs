using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EF_CodeFirst.Filters
{
    public class AdminAuthorization : FilterAttribute, IAuthorizationFilter
    {
        void IAuthorizationFilter.OnAuthorization(AuthorizationContext filterContext)
        {
            if(filterContext.HttpContext.User.IsInRole("Admin")==false)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}