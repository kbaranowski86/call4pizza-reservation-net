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
    
    public partial class Ingredient
    {
        public Ingredient()
        {
            this.MealComposition = new HashSet<MealComposition>();
            this.MealConstraints = new HashSet<MealConstraints>();
            this.MealsGroupConstraints = new HashSet<MealsGroupConstraints>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
    
        public virtual ICollection<MealComposition> MealComposition { get; set; }
        public virtual ICollection<MealConstraints> MealConstraints { get; set; }
        public virtual ICollection<MealsGroupConstraints> MealsGroupConstraints { get; set; }
    }
}
