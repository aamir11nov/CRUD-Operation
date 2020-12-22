using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace CRUDDemo.Controllers
{
    public class CRUDController : Controller
    {

        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(Product Productname)
        {
            using (var context = new demoCRUDEntities())
            {
                context.Product.Add(Productname);
                context.SaveChanges();
            }
            string message = "Created the record successfully";
            ViewBag.Message = message;
            return View();
        }

        [HttpGet]
        public ActionResult Read()
        {
            using (var context = new demoCRUDEntities())
            {
                var data = context.Product.ToList();
                return View(data);
            }

        }


        public ActionResult Update(int Productid)
        {
            using (var context = new demoCRUDEntities())
            {
                var data = context.Product.Where(x => x.ProductNo == Productid).SingleOrDefault();
                return View(data);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int Productid, Product Productname)
        {
            using (var context = new demoCRUDEntities())
            {
                var data = context.Product.FirstOrDefault(x => x.ProductNo == Productid);
                if (data != null)
                {
                    data.Name = Productname.Name;
                    data.Id = Productname.Id;
                    data.Productname = Productname.Productname;
                    data.Categoryname = Productname.Categoryname;
                    context.SaveChanges();
                    return RedirectToAction("Read");
                }
                else
                    return View();
            }
        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int Productid)
        {
            using (var context = new demoCRUDEntities())
            {
                var data = context.Product.FirstOrDefault(x => x.ProductNo == Productid);
                if (data != null)
                {
                    context.Product.Remove(data);
                    context.SaveChanges();
                    return RedirectToAction("Read");
                }
                else
                    return View();
            }
        }
    }
}
