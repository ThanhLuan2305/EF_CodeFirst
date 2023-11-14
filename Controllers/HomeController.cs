using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EF_CodeFirst.Filters;
using System.Diagnostics;

namespace EF_CodeFirst.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [MyActionFilter]
        [MyResultFilter]
        [OutputCache(Duration = 100)] // Lưu trữ 1 số thông tin trong khoảng tg quy định
        public ActionResult Index()
        {
            ViewBag.Number = 10;
            return View();
        }
        public ActionResult Error404()
        {
            return View();
        }
    }
}