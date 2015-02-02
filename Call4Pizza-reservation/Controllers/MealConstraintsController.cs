using Call4Pizza_reservation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Call4Pizza_reservation.Controllers
{
    [Authorize(Roles = "administrator")]
    public class MealConstraintsController : Controller
    {
        call4PizzaDb db = new call4PizzaDb();

        public ActionResult Create(int mealId, int ingredientId)
        {
            if( mealId == null || ingredientId == null )
            {
                return HttpNotFound();
            }

            db.MealConstraints.Add(new MealConstraints()
            {
                mealId = mealId,
                ingredientId = ingredientId,
                maxIngredientAmount = 1
            });
            db.SaveChanges();

            return RedirectToAction( "Edit", "Meal", new { id = mealId } );
        }

        public ActionResult MaxIngredientAmountIncrease( int mealId, int ingredientId )
        {
            if (mealId == null || ingredientId == null)
            {
                return HttpNotFound();
            }

            MealConstraints mc = db.MealConstraints.Where(r => r.mealId == mealId && r.ingredientId == ingredientId).FirstOrDefault();
            mc.maxIngredientAmount++;
            db.SaveChanges();

            return RedirectToAction("Edit", "Meal", new { id = mealId });
        }

        public ActionResult MaxIngredientAmountDecrease(int? mealId = null, int? ingredientId = null)
        {
            MealConstraints mc = db.MealConstraints.Where(r => r.mealId == mealId && r.ingredientId == ingredientId).FirstOrDefault();
            if (mealId != null && ingredientId != null && mc != null && mc.maxIngredientAmount > 0)
            {
                if (mc.maxIngredientAmount == 1)
                {
                    db.MealConstraints.Remove(mc);
                }
                else
                {
                    mc.maxIngredientAmount--;
                }
                db.SaveChanges();
            }
            return RedirectToAction("Edit", "Meal", new { id = mealId });
        }

        public ActionResult IngredientRemove(int? mealId = null, int? ingredientId = null)
        {
            MealConstraints mc = db.MealConstraints.Where(r => r.mealId == mealId && r.ingredientId == ingredientId).FirstOrDefault();
            if (mealId != null && ingredientId != null && mc != null)
            {
                db.MealConstraints.Remove(mc);
                db.SaveChanges();
            }
            return RedirectToAction("Edit", "Meal", new { id = mealId });
        }
    }
}
