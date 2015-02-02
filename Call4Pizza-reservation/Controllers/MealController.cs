using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Call4Pizza_reservation.Models;
using System.Data.Entity;

namespace Call4Pizza_reservation.Controllers
{
    [Authorize(Roles = "administrator")]
    public class MealController : Controller
    {
        call4PizzaDb db = new call4PizzaDb();

        // GET: Meal
        public ActionResult Index()
        {
            List<Meal> meals = db.Meal.ToList();
            return View(meals);
        }

        // GET: Meal/Details/5
        public ActionResult Details(int id)
        {
            Meal ml = db.Meal.Find(id);

            return View(ml);
        }

        // GET: Meal/Create
        public ActionResult Create()
        {
            ViewData["mealsGroupId"] = new SelectList(db.MealsGroup.ToList(), "id", "name");

            return View();
        }

        // POST: Meal/Create
        [HttpPost]
        public ActionResult Create(Meal ml)
        {
            if (ModelState.IsValid)
            {
                Meal addedMeal = db.Meal.Add(ml);
                db.SaveChanges();

                return RedirectToAction("Edit", new { id = addedMeal.id });
            }
            else
            {
                return View(ml);
            }
        }

        // GET: Meal/Edit/5
        public ActionResult Edit(int id)
        {
            Meal ml = db.Meal.Find(id);
            ViewBag.ingredientsMealComposition = db.MealComposition.Where(mc => mc.mealId == id).Join(db.Ingredient, mc => mc.ingredientId, i => i.id, (mc, i) => new { MealComposition = mc, Ingredient = i } ).ToDictionary( k => k.Ingredient, v => v.MealComposition );
            ViewBag.ingredientsMealConstraints = db.MealConstraints.Where(mc => mc.mealId == id).Join(db.Ingredient, mc => mc.ingredientId, i => i.id, (mc, i) => new { MealConstraints = mc, Ingredient = i }).ToDictionary(k => k.Ingredient, v => v.MealConstraints);
            ViewBag.allIngredients = db.Ingredient.ToList();

            return View(ml);
        }

        // POST: Meal/Edit/5
        [HttpPost]
        public ActionResult Edit(Meal ml)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ml).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ml);
        }

        // GET: Meal/Delete/5
        public ActionResult Delete(int id)
        {
            Meal ml = db.Meal.Find(id);
            return View(ml);
        }

        // POST: Meal/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                db.MealComposition.RemoveRange(db.MealComposition.Where(mc => mc.mealId == id));
                db.MealsOrder.RemoveRange(db.MealsOrder.Where(mo => mo.mealId == id));
                Meal ml = db.Meal.Find(id);
                db.Meal.Remove(ml);
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
