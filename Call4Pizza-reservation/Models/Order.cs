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
    
    public partial class Order
    {
        public Order()
        {
            this.MealsOrder = new HashSet<MealsOrder>();
        }
    
        public int id { get; set; }
        public string userId { get; set; }
        public double price { get; set; }
        public System.DateTime created { get; set; }
        public Nullable<System.DateTime> received { get; set; }
    
        public virtual ICollection<MealsOrder> MealsOrder { get; set; }
    }
}
