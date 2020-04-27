using Call4Pizza_reservation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Call4Pizza_reservation.Controllers
{
    public class MealsGroupConstraintsController : Controller
    {

        call4PizzaDb db = new call4PizzaDb();

        // GET: MealsGroupConstraints/Create
        public ActionResult Create(int mealsGroupId, int ingredientId)
        {
            if (mealsGroupId == null || ingredientId == null)
            {
                return HttpNotFound();
            }

            db.MealsGroupConstraints.Add(new MealsGroupConstraints()
            {
                mealsGroupId = mealsGroupId,
                ingredientId = ingredientId,
                maxIngredientAmount = 1
            });
            db.SaveChanges();

            return RedirectToAction("Edit", "MealsGroup", new { id = mealsGroupId });
        }

        public ActionResult MaxIngredientAmountIncrease(int mealsGroupId, int ingredientId)
        {
            if (mealsGroupId == null || ingredientId == null)
            {
                return HttpNotFound();
            }

            MealsGroupConstraints mc = db.MealsGroupConstraints.Where(r => r.mealsGroupId == mealsGroupId && r.ingredientId == ingredientId).FirstOrDefault();
            mc.maxIngredientAmount++;
            db.SaveChanges();

            return RedirectToAction("Edit", "MealsGroup", new { id = mealsGroupId });
        }

        public ActionResult MaxIngredientAmountDecrease(int? mealsGroupId = null, int? ingredientId = null)
        {
            MealsGroupConstraints mgc = db.MealsGroupConstraints.Where(r => r.mealsGroupId == mealsGroupId && r.ingredientId == ingredientId).FirstOrDefault();
            if (mealsGroupId != null && ingredientId != null && mgc != null && mgc.maxIngredientAmount > 0)
            {
                if (mgc.maxIngredientAmount == 1)
                {
                    db.MealsGroupConstraints.Remove(mgc);
                }
                else
                {
                    mgc.maxIngredientAmount--;
                }
                db.SaveChanges();
            }
            return RedirectToAction("Edit", "MealsGroup", new { id = mealsGroupId });
        }

        public ActionResult IngredientRemove(int? mealsGroupId = null, int? ingredientId = null)
        {
            MealsGroupConstraints mgc = db.MealsGroupConstraints.Where(r => r.mealsGroupId == mealsGroupId && r.ingredientId == ingredientId).FirstOrDefault();
            if (mealsGroupId != null && ingredientId != null && mgc != null)
            {
                db.MealsGroupConstraints.Remove(mgc);
                db.SaveChanges();
            }
            return RedirectToAction("Edit", "MealsGroup", new { id = mealsGroupId });
        }
    }
}
