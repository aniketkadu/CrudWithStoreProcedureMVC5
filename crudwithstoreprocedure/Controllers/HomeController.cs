using crudwithstoreprocedure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace crudwithstoreprocedure.Controllers
{
    public class HomeController : Controller
    {
        CustomerContext db = new CustomerContext();

        public HomeController()
        {
            db.Database.Log = l => Debug.Write(l);
        }

        public ActionResult FillCity(int state)
        {
            var cities = db.City4.Where(c => c.StateId == state);
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View(db.CustomerViewModels.ToList());
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            return View(db.CustomerViewModels.Find(id));
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            
            ViewBag.StateList = db.State4;
            var model = new CustomerViewModel();
            return View(model);
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(CustomerViewModel model)
        {
            var Statnm = db.State4.Find(Convert.ToInt32(model.State));
            var citynm = db.City4.Find(Convert.ToInt32(model.City));
            if (ModelState.IsValid)
            {
                CustomerViewModel obj = new CustomerViewModel();
                obj.Name = model.Name;
                obj.Email = model.Email;
                obj.address = model.address;
                obj.City = citynm.CityName;
                obj.State = Statnm.StateName;


                db.CustomerViewModels.Add(obj);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.StateList = db.State4;
            return View(model);

        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.StateList = db.State4;
         
            return View(db.CustomerViewModels.Find(id));
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CustomerViewModel model)
        {
            var Statnm = db.State4.Find(Convert.ToInt32(model.State));
            var citynm = db.City4.Find(Convert.ToInt32(model.City));
            if (ModelState.IsValid)
            {
                var obj = db.CustomerViewModels.Find(id);
                obj.Name = model.Name;
                obj.Email = model.Email;
                obj.address = model.address;
                obj.City = citynm.CityName;
                obj.State = Statnm.StateName;

                //db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StateList = db.State4;
            return View(model);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View(db.CustomerViewModels.Find(id));
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, CustomerViewModel model)
        {
            try
            {
                // TODO: Add delete logic here
                var res = db.CustomerViewModels.Find(id);
                db.CustomerViewModels.Remove(res);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
