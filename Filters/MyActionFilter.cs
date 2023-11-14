using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace EF_CodeFirst.Filters
{
    public class MyActionFilter : FilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewBag.Number = 15;
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            Debug.WriteLine("Action Executing");
            filterContext.Controller.ViewBag.Number = 5;
        }
    }
}