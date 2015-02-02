using Call4Pizza_reservation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Call4Pizza_reservation.Controllers
{
    [Authorize(Roles = "administrator")]
    public class MealCompositionController : Controller
    {
        call4PizzaDb db = new call4PizzaDb();

        // GET: MealComposition
        public ActionResult Index()
        {
            List<MealComposition> mcList = db.MealComposition.ToList();

            return View(mcList);
        }

        // GET: MealComposition/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MealComposition/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MealComposition/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MealComposition/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MealComposition/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult IngredientAmountIncrease(int? mealId = null, int? ingredientId = null)
        {
            if (mealId != null && ingredientId != null)
            {
                MealComposition mc = db.MealComposition.Where(r => r.mealId == mealId && r.ingredientId == ingredientId).FirstOrDefault();
                if (mc.ingredientAmount < db.MealConstraints.Where(mco => mco.mealId == mealId && mco.ingredientId == ingredientId).FirstOrDefault().maxIngredientAmount)
                {
                    mc.ingredientAmount++;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Edit", "Meal", new { id = mealId });
        }

        public ActionResult IngredientAmountDecrease(int? mealId = null, int? ingredientId = null)
        {
            MealComposition mc = db.MealComposition.Where(r => r.mealId == mealId && r.ingredientId == ingredientId).FirstOrDefault();
            if (mealId != null && ingredientId != null && mc != null && mc.ingredientAmount > 0)
            {
                if (mc.ingredientAmount == 1)
                {
                    db.MealComposition.Remove(mc);
                }
                else
                {
                    mc.ingredientAmount--;
                }
                db.SaveChanges();
            }
            return RedirectToAction("Edit", "Meal", new { id = mealId });
        }

        public ActionResult IngredientRemove(int? mealId = null, int? ingredientId = null)
        {
            MealComposition mc = db.MealComposition.Where(r => r.mealId == mealId && r.ingredientId == ingredientId).FirstOrDefault();
            if (mealId != null && ingredientId != null && mc != null)
            {
                db.MealComposition.Remove(mc);
                db.SaveChanges();
            }
            return RedirectToAction("Edit", "Meal", new { id = mealId });
        }

        public ActionResult Create(int? mealId = null, int? ingredientId = null)
        {
            if (mealId != null && ingredientId != null && db.MealComposition.Where( r => r.mealId == mealId && r.ingredientId == ingredientId ).FirstOrDefault() == null )
            {
                db.MealComposition.Add(new MealComposition
                {
                    mealId = (int)mealId,
                    ingredientId = (int)ingredientId,
                    ingredientAmount = 1
                });
                db.SaveChanges();
            }
            return RedirectToAction("Edit", "Meal", new { id = mealId });
        }
    }
}
