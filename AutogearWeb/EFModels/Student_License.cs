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
    
    public partial class Student_License
    {
        public int ID { get; set; }
        public Nullable<int> S_Number { get; set; }
        public string License_No { get; set; }
        public int State_ID { get; set; }
        public System.DateTime Expiry_Date { get; set; }
        public string Class { get; set; }
        public Nullable<System.DateTime> License_passed_Date { get; set; }
        public string Remarks { get; set; }
    }
}
