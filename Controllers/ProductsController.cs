using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EF_CodeFirst.Models;
using EF_CodeFirst.Filters;

namespace EF_CodeFirst.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        [MyAuthenFilter]
        public ActionResult Index( string SortColumn = "ProductID", string IconClass = "fa-sort-asc", int page = 1)
        {
            //throw new Exception("Error");
            CompanyDBContext db = new CompanyDBContext();
            //List<Product> pr = db.Products.ToList();
            // Search
            List<Product> pr = db.Products.ToList();

            //Sort
            ViewBag.SortColumn = SortColumn;
            ViewBag.IconClass = IconClass;
            if (SortColumn == "ProductId")
            {
                if (IconClass == "fa-sort-asc")
                {
                    pr = pr.OrderBy(r => r.ProductID).ToList();
                }
                else
                {
                    pr = pr.OrderByDescending(r => r.ProductID).ToList();
                }
            }
            else if (SortColumn == "ProductName")
            {
                if (IconClass == "fa-sort-asc")
                {
                    pr = pr.OrderBy(r => r.ProductName).ToList();
                }
                else
                {
                    pr = pr.OrderByDescending(r => r.ProductName).ToList();
                }
            }

            // Paging 
            int NoOfRecordPerPage = 5;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(pr.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;
            pr = pr.Skip(NoOfRecordSkip).Take(NoOfRecordPerPage).ToList();

            return View(pr);
        }
        public ActionResult Detail(int id)
        {

            CompanyDBContext db = new CompanyDBContext();
            Product pr = db.Products.Where(x => x.ProductID == id).FirstOrDefault();
            return View(pr);
        }
        public ActionResult  DisPlaySingleProduct(Product p)
        {
            return PartialView("MyProduct", p);
        }
    }
}