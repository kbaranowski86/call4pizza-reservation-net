using Call4Pizza_reservation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Call4Pizza_reservation.Controllers
{
    [Authorize(Roles = "administrator")]
    public class MealsGroupController : Controller
    {
        call4PizzaDb db = new call4PizzaDb();

        // GET: MealsGroup
        public ActionResult Index()
        {
            List<MealsGroup> mgList = db.MealsGroup.ToList(); 

            return View(mgList);
        }

        // GET: MealsGroup/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MealsGroup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MealsGroup/Create
        [HttpPost]
        public ActionResult Create(MealsGroup mg)
        {
            if (ModelState.IsValid)
            {
                db.MealsGroup.Add(mg);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View(mg);
            }
        }

        // GET: MealsGroup/Edit/5
        public ActionResult Edit(int id)
        {
            MealsGroup mg = db.MealsGroup.Find(id);

            return View(mg);
        }

        // POST: MealsGroup/Edit/5
        [HttpPost]
        public ActionResult Edit(MealsGroup mg)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mg).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mg);
        }

        // GET: MealsGroup/Delete/5
        public ActionResult Delete(int id)
        {
            MealsGroup mg = db.MealsGroup.Find(id);

            return View(mg);
        }

        // POST: MealsGroup/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                db.Meal.RemoveRange(db.Meal.Where(ml => ml.mealsGroupId == id));
                MealsGroup mg = db.MealsGroup.Find(id);
                db.MealsGroup.Remove(mg);
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
