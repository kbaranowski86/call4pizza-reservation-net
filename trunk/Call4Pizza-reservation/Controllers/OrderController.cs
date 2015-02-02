using Call4Pizza_reservation.Library;
using Call4Pizza_reservation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Call4Pizza_reservation.Controllers
{
    public class OrderController : BaseController
    {
        [Authorize(Roles = "administrator")]
        public ActionResult Index()
        {
            List<Order> orders = db.Order.OrderByDescending( o => o.received ).ToList();
            return View(orders);
        }

        public ActionResult Delete(int id)
        {
            Order ml = db.Order.Find(id);
            return View(ml);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                db.MealsOrder.RemoveRange(db.MealsOrder.Where(mo => mo.orderId == id));
                Order o = db.Order.Find(id);
                db.Order.Remove(o);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "administrator")]
        public ActionResult Details( int? orderId = null )
        {
            if(orderId == null)
            {
                return HttpNotFound();
            }
            else
            {
                Order order = db.Order.Find( orderId );
                return View( order );
            }
        }

        public ActionResult ListMealsGroups()
        {
            return View(db.MealsGroup.ToList());
        }

        /// <summary>
        /// Add meal to order
        /// </summary>
        /// <param name="mealId">Id of a meal</param>
        /// <returns>Action result</returns>
        public ActionResult AddMeal( int? mealsGroupId = null, int? mealId = null)
        {
            // list meals to select one
            if( mealId == null && mealsGroupId != null )
            {
                ViewBag.mealsGroupName = db.MealsGroup.Find( mealsGroupId ).name;
                List<Meal> meals = db.Meal.Where( m => m.mealOriginId == null && m.mealsGroupId == mealsGroupId ).ToList();
                return View(meals);
            }
            // save selection and go to the ingredients selection
            else if (mealsGroupId == null && mealId != null)
            {
                Meal orderReqRes = db.Meal.Find( mealId );
                if(orderReqRes == null)
                {
                    return HttpNotFound();
                }

                Dictionary<int, int> tmpIngredientsDict = new Dictionary<int,int>();
                
                // add ingredients to meal
                foreach( MealComposition mc in orderReqRes.MealComposition )
                {
                    tmpIngredientsDict.Add(mc.ingredientId, mc.ingredientAmount);
                }
                TempOrderMeal tom = new TempOrderMeal((int)mealId, 1, tmpIngredientsDict);

                // add meal to order
                order.Add(tom);
                Session["order"] = order;

                return RedirectToAction("SelectIngredients");
            }
            else
            {
                return HttpNotFound();
            }
            
        }

        /// <summary>
        /// Select ingredients for meals add to order
        /// </summary>
        /// <returns>Action result</returns>
        public ActionResult SelectIngredients()
        {
            if(Session["order"] == null)
            {
                return RedirectToAction("AddMeal");
            }
            else
            {
                ViewBag.allIngredients = db.Ingredient.ToList();

                List<int> orderMealsIds = order.Select(x => x.id).ToList();
                ViewBag.selectedMealsObj = db.Meal.Where(m => orderMealsIds.Contains(m.id)).ToList();
                ViewBag.selectedMealsConstraints = db.MealConstraints.Where(mc => orderMealsIds.Contains(mc.mealId)).ToList();

                return View(order);
            }
        }

        public ActionResult IngredientAmountIncrease( int? mealListIndex = null, int? ingredientId = null )
        {
            int mealId = order[(int)mealListIndex].id;
            if (mealListIndex != null && ingredientId != null && order[(int)mealListIndex].mealIngredients.Where(kv => kv.Key == ingredientId).FirstOrDefault().Value < db.MealConstraints.Where(mc => mc.mealId == mealId && mc.ingredientId == ingredientId).FirstOrDefault().maxIngredientAmount)
            {
                order[(int)mealListIndex].mealIngredients[(int)ingredientId]++;
                Session["order"] = order;
            }
            return RedirectToAction("SelectIngredients");
        }

        public ActionResult IngredientAmountDecrease( int? mealListIndex = null, int? ingredientId = null )
        {
            if( mealListIndex != null && ingredientId != null && order[(int)mealListIndex].mealIngredients[(int)ingredientId] > 0 )
            {
                if( order[(int)mealListIndex].mealIngredients[(int)ingredientId] == 1 )
                {
                    order[(int)mealListIndex].mealIngredients.Remove((int)ingredientId);
                }
                else
                {
                    order[(int)mealListIndex].mealIngredients[(int)ingredientId] --;
                }
                Session["order"] = order;
            }
            return RedirectToAction("SelectIngredients");
        }

        public ActionResult IngredientRemove( int? mealListIndex = null, int? ingredientId = null )
        {
            if( mealListIndex != null && ingredientId != null )
            {
                order[(int)mealListIndex].mealIngredients.Remove((int)ingredientId);
                Session["order"] = order;
            }
            return RedirectToAction("SelectIngredients");
        }

        public ActionResult IngredientAdd( int? mealListIndex = null, int? ingredientId = null )
        {
            int mealId = order[(int)mealListIndex].id;
            if( mealListIndex != null && ingredientId != null &&
                order[(int)mealListIndex].mealIngredients.ContainsKey((int)ingredientId) == false &&
                db.MealConstraints.Where(mc => mc.mealId == mealId && mc.ingredientId == ingredientId).FirstOrDefault() != null
            )    
            {
                order[(int)mealListIndex].mealIngredients.Add((int)ingredientId, 1);
                Session["order"] = order;
            }
            return RedirectToAction("SelectIngredients");
        }

        public ActionResult MealAmountIncrease( int? mealListIndex = null )
        {
            if( mealListIndex != null )
            {
                order[(int)mealListIndex].mealAmount ++;
                Session["order"] = order;
            }
            return RedirectToAction("SelectIngredients");
        }

        public ActionResult MealAmountDecrease(int? mealListIndex = null)
        {
            if (mealListIndex != null && order[(int)mealListIndex].mealAmount > 0)
            {
                if( order[(int)mealListIndex].mealAmount == 1 )
                {
                    order.RemoveAt((int)mealListIndex);
                }
                else
                {
                    order[(int)mealListIndex].mealAmount --;
                }
                               
                Session["order"] = order;
            }
            return RedirectToAction("SelectIngredients");
        }

        public ActionResult MealRemove( int? mealListIndex = null )
        {
            if( mealListIndex != null )
            {
                order.RemoveAt((int)mealListIndex);
                Session["order"] = order;
            }
            return RedirectToAction("SelectIngredients");
        }

        public ActionResult Create()
        {
            if (Session["order"] == null && order.Count > 0)
            {
                return RedirectToAction("AddMeal");
            }
            else
            {
                Order newOrder = db.Order.Add(new Order
                {
                    created = DateTime.Now,
                    price = Double.Parse(ViewBag.totalCost),
                    userId = "xxx"
                });

                foreach( TempOrderMeal tol in order )
                {
                    if( Utils.MealExistsInDb(tol) )
                    {
                        db.MealsOrder.Add(new MealsOrder
                        {
                            amount = tol.mealAmount,
                            mealId = tol.id,
                            orderId = newOrder.id
                        });
                    }
                    else
                    {
                        Meal originalMeal = db.Meal.Find(tol.id);
                        Meal newMeal = db.Meal.Add(new Meal
                        {
                            basePrice = originalMeal.basePrice,
                            description = originalMeal.description,
                            mealOriginId = originalMeal.id,
                            name = originalMeal.name
                        });

                        foreach( KeyValuePair<int, int> ing in tol.mealIngredients )
                        {
                            db.MealComposition.Add(new MealComposition
                            {
                                mealId = newMeal.id,
                                ingredientId = ing.Key,
                                ingredientAmount = ing.Value
                            });
                        }

                        db.MealsOrder.Add(new MealsOrder
                        {
                            amount = tol.mealAmount,
                            mealId = newMeal.id,
                            orderId = newOrder.id
                        });
                    }
                }
                db.SaveChanges();
                Session["order"] = null;
            }

            return View();
        }

    }
}