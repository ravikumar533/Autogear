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
    
    public partial class Booking
    {
        public int Booking_ID { get; set; }
        public int S_Number { get; set; }
        public int I_Number { get; set; }
        public Nullable<System.DateTime> Booking_Date { get; set; }
        public Nullable<System.TimeSpan> Start_Time { get; set; }
        public Nullable<System.TimeSpan> End_Time { get; set; }
        public string Pick_Location { get; set; }
        public string Drop_Location { get; set; }
        public int Package_ID { get; set; }
        public string Discount { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> Cancelled_Date { get; set; }
        public string Cancelled_Reason { get; set; }
        public string Type { get; set; }
        public Nullable<int> RTA_ID { get; set; }
        public string DrivingTest_Status { get; set; }
        public Nullable<int> DrivingTest_Attempt { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public string Created_By { get; set; }
        public string Modified_By { get; set; }
    
        public virtual Booking Bookings1 { get; set; }
        public virtual Booking Booking1 { get; set; }
        public virtual Student Student { get; set; }
    }
}
