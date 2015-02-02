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
    public class IngredientController : Controller
    {
        call4PizzaDb db = new call4PizzaDb();

        // GET: Ingredient
        public ActionResult Index()
        {
            List<Ingredient> ings = db.Ingredient.ToList();

            return View(ings);
        }

        // GET: Ingredient/Details/5
        public ActionResult Details(int id)
        {
            Ingredient ing = db.Ingredient.Find(id);

            return View(ing);
        }

        // GET: Ingredient/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ingredient/Create
        [HttpPost]
        public ActionResult Create(Ingredient ing)
        {
            if (ModelState.IsValid)
            {
                db.Ingredient.Add(ing);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View(ing);
            }
        }

        // GET: Ingredient/Edit/5
        public ActionResult Edit(int id)
        {
            Ingredient ing = db.Ingredient.Find(id);

            return View(ing);
        }

        // POST: Ingredient/Edit/5
        [HttpPost]
        public ActionResult Edit(Ingredient ing)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ing);
        }

        // GET: Ingredient/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Ingredient/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Ingredient ing = db.Ingredient.Find(id);
                db.Ingredient.Remove(ing);
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
