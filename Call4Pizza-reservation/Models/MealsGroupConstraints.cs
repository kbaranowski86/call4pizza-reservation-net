//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Call4Pizza_reservation.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MealsGroupConstraints
    {
        public int mealsGroupId { get; set; }
        public int ingredientId { get; set; }
        public Nullable<int> maxIngredientAmount { get; set; }
    
        public virtual Ingredient Ingredient { get; set; }
        public virtual MealsGroup MealsGroup { get; set; }
    }
}
