//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutogearWeb.EFModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class State
    {
        public State()
        {
            this.Suburbs = new HashSet<Suburb>();
        }
    
        public int State_ID { get; set; }
        public string State_Name { get; set; }
    
        public virtual ICollection<Suburb> Suburbs { get; set; }
    }
}
