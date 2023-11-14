using EF_CodeFirst.Filters;
using EF_CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EF_CodeFirst.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class ProductsController : Controller
    {
        // GET: Admin/Products
        CompanyDBContext db = new CompanyDBContext();
        public ActionResult Index(string search = "", string SortColumn = "ProductID", string IconClass = "fa-sort-asc", int page = 1)
        {
            //List<Product> pr = db.Products.ToList();
            // Search
            List<Product> pr = db.Products.Where(x => x.ProductName.Contains(search)).ToList();
            ViewBag.TB = search;

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
            Product pr = db.Products.Where(x => x.ProductID == id).FirstOrDefault();
            return View(pr);
        }
        public ActionResult Create()
        {
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brands = db.Brands.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product p)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Categories = db.Categories.ToList();
                ViewBag.Brands = db.Brands.ToList();
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            Product pr = db.Products.Where(r => r.ProductID == id).FirstOrDefault();
            return View(pr);
        }
        [HttpPost]
        public ActionResult Edit(Product pro)
        {
            Product pr = db.Products.Where(r => r.ProductID == pro.ProductID).FirstOrDefault();

            pr.ProductName = pro.ProductName;
            pr.Price = pro.Price;
            pr.DateOfPurchase = pro.DateOfPurchase;
            pr.AvailabilityStatus = pro.AvailabilityStatus;
            pr.CategoryID = pro.CategoryID;
            pr.BrandID = pro.BrandID;
            pr.Active = pro.Active;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            Product pr = db.Products.Where(r => r.ProductID == id).FirstOrDefault();
            return View(pr);
        }
        [HttpPost]
        public ActionResult Delete(int id, Product p)
        {
            Product pr = db.Products.Where(r => r.ProductID == id).FirstOrDefault();
            db.Products.Remove(pr);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}