
using Machinetest.Context;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Machine.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        product_listEntities dbObj = new product_listEntities();

        public ActionResult Category(CategoryMaster obj)
        {

            return View(obj);

        }
        [HttpPost]
        public ActionResult AddCategory(CategoryMaster model)
        {
            CategoryMaster obj = new CategoryMaster();
            if (ModelState.IsValid)
            {
                obj.CategoryId = model.CategoryId;

                obj.CategoryName = model.CategoryName;

                if (model.CategoryId == 0)
                {
                    dbObj.CategoryMasters.Add(obj);
                    dbObj.SaveChanges();
                }
                else
                {
                    dbObj.Entry(obj).State = EntityState.Modified;
                    dbObj.SaveChanges();
                }
                ModelState.Clear();

            }
            return RedirectToAction("CategoryList");
        }


        public ActionResult CategoryList(int PageNo = 1)
        {

            var res = dbObj.CategoryMasters.ToList();
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
            var res = dbObj.CategoryMasters.Where(x => x.CategoryId == id).First();
            dbObj.CategoryMasters.Remove(res);
            dbObj.SaveChanges();

            var list = dbObj.CategoryMasters.ToList();
            return RedirectToAction("CategoryList");
        }
    }
}