using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Call4Pizza_reservation.Library
{
    public class TempOrderMeal
    {
        public int id { get; set; }
        public int mealAmount { get; set; }
        public Dictionary<int, int> mealIngredients { get; set; }

        /// <summary>
        /// TempOrderMeal constructor
        /// </summary>
        /// <param name="mealId">Id of meal</param>
        /// <param name="mealAmount">Amount of meal</param>
        /// <param name="mealIngredients">Dictionary of ingredients (ingredientId, ingredientAmount)</param>
        public TempOrderMeal( int mealId, int mealAmount, Dictionary<int, int> mealIngredients )
        {
            this.id = mealId;
            this.mealAmount = mealAmount;
            this.mealIngredients = mealIngredients;
        }
    }
}