
using Machinetest.Context;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Machine.Controllers
{
    public class ProductController : Controller
    {
        // GET: Category
        product_listEntities dbObj = new product_listEntities();

        public ActionResult Product(ProductMaster obj)
        {

            return View(obj);

        }
        [HttpPost]
        public ActionResult AddProduct(ProductMaster model)
        {
            ProductMaster obj = new ProductMaster();
            if (ModelState.IsValid)
            {
                obj.ProductId = model.ProductId;
                obj.ProductId = model.ProductId;
                obj.ProductName = model.ProductName;

                if (model.ProductId == 0)
                {
                    dbObj.ProductMasters.Add(obj);
                    dbObj.SaveChanges();
                }
                else
                {
                    dbObj.Entry(obj).State = EntityState.Modified;
                    dbObj.SaveChanges();
                }
                ModelState.Clear();

            }
            return RedirectToAction("ProductList");
        }



        public ActionResult ProductList(int PageNo = 1)
        {
            var res = dbObj.ProductMasters.ToList();
            int NoofRecordsPerPage = 10;
            int NoofPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(res.Count) / Convert.ToDouble(NoofRecordsPerPage)));
            int NoofRecordsToSkip = (PageNo - 1) * NoofRecordsPerPage;
            ViewBag.PageNo = PageNo;
            ViewBag.NoofPages = NoofPages;
            res = res.Skip(NoofRecordsToSkip).Take(NoofRecordsPerPage).ToList();

            return View(res);
        }

        public ActionResult Delete(int id)
        {
            var res = dbObj.ProductMasters.Where(x => x.ProductId == id).First();
            dbObj.ProductMasters.Remove(res);
            dbObj.SaveChanges();

            var list = dbObj.ProductMasters.ToList();
            return RedirectToAction("ProductList");
        }
    }
}