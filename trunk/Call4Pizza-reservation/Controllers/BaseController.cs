using Call4Pizza_reservation.Library;
using Call4Pizza_reservation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Call4Pizza_reservation.Controllers
{
    public class BaseController : Controller
    {
        protected call4PizzaDb db;
        protected List<TempOrderMeal> order;

        public BaseController()
        {
            db = new call4PizzaDb();
            if (System.Web.HttpContext.Current.Session["order"] == null)
            {
                order = new List<TempOrderMeal>();
            }
            else
            {
                order = (List<TempOrderMeal>)System.Web.HttpContext.Current.Session["order"];
                double cost = 0;
                foreach( TempOrderMeal tol in order )
                {
                    cost += db.Meal.Find( tol.id ).basePrice;
                    foreach( KeyValuePair<int, int> ing in tol.mealIngredients )
                    {
                        cost += db.Ingredient.Find( ing.Key ).price * ing.Value;
                    }
                    cost *= tol.mealAmount;
                }

                ViewBag.totalCost = cost.ToString();
            }
        }
    }
}