using Call4Pizza_reservation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Call4Pizza_reservation.Library
{
    public class Utils
    {
        /// <summary>
        /// Checks if meal composition from order exists in db
        /// </summary>
        /// <param name="tol">TempOrderMeal instance</param>
        /// <returns>Whether meal composition from order exists in db</returns>
        public static bool MealExistsInDb(TempOrderMeal tol)
        {
            call4PizzaDb db = new call4PizzaDb();

            //Meal not found in db
            IQueryable<Meal> tmpMeal = db.Meal.Where( m => m.id == tol.id );
            if( tmpMeal.Count() == 0 )
            {
                return false;
            }

            //Meal found in db but with no ingredients
            IQueryable<MealComposition> tmpMealComp = db.MealComposition.Where(mc => mc.mealId == tol.id);
            if (tmpMeal.Count() == 1 && tmpMealComp.Count() == 0)
            {
                if (tol.mealIngredients.Count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            // Meal found in db with one or more ingredients
            if (tmpMealComp.Count() == tol.mealIngredients.Count)
            {
                int ingredientsCorrect = 0;
                foreach (KeyValuePair<int, int> ing in tol.mealIngredients)
                {
                    if (tmpMealComp.Select(x => new { x.ingredientId, x.ingredientAmount }).AsEnumerable().ToDictionary(k => k.ingredientId, v => v.ingredientAmount).Contains(ing))
                    {
                        ingredientsCorrect++;
                    }
                }
                if( ingredientsCorrect == tol.mealIngredients.Count )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}