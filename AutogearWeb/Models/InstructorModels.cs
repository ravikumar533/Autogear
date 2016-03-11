using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AutogearWeb.Models
{
    public class TblInstructor
    {
        public string InstructorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string InstructorNumber { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int AddressId { get; set; }
        public bool Status { get; set; }
        public string AreaIds { get; set; }
    }

    public class InstructorBooking
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string ClassName { get; set; }
        public int Column { get; set; }
    }
    
    public class StudentList
    {
        public int BookingId { get; set; }
        public string StudentName { get; set; }
        public string StartTime { get; set; }
        public string StopTime { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }

    public class CalendarModel
    {
        public bool IsUserAdmin { get; set; }
        public SelectList InstructorList { get; set; }
    }

    public class InstructorLeaveModel
    {
        public int Id { get; set; }
        public string InstructorId { get; set; }
        [Required]
        [Display(Name = "Leave Reason")]
        public string LeaveReason { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        [Required]
        [Display(Name = "Start Time")]
        [DisplayFormat(DataFormatString = "{0:yyyy dd MM}")]
        public TimeSpan? StartTime { get; set; }
        [Required]
        [Display(Name = "Stop Time")]
        [DisplayFormat(DataFormatString = "{0:yyyy dd MM}")]
        public TimeSpan? StopTime { get; set; }

        public string Status { get; set; }
    }

    public class AllInstructorsLeavesModel
    {
        public string InstructorName { get; set; }
        public InstructorLeaveModel InstructorDetails { get; set; }
    }

    public class InstructorModel
    {
        public string InstructorId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        public int Gender { get; set; }

        [Display(Name = "Home Phone")]
        public string Phone { get; set; }

        public string Mobile { get; set; }
        public string Password { get; set; }
        [Display(Name="InstructorId")]
        public string InstructorNumber { get; set; }
        public SelectList GendersList { get; set; }
        [Required]
         [Display(Name = "Address1")]
        public string AddressLine1 { get; set; }
         [Display(Name = "Address2")]
        public string AddressLine2 { get; set; }
       
        [Display(Name = "Areas")]
         public string[] AreaIds { get; set; }

        public string PostalCode { get; set; }
        public string SuburbName { get; set; }
        public int SuburbId { get; set; }
        public MultiSelectList Areas { get; set; }
        public bool Status { get; set; }
        public string CreatedUser { get; set; }
        public string AreaNames { get; set; }
    }

    public class PackageModel
    {
        public int PackageId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Hours must be numeric")]
        [Display(Name = "Hours")]
        public int Hours { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Cost must be numeric")]
        [Display(Name = "Cost")]
        public int Cost { get; set; }
    }

    public class AreaModel
    {
        public int AreaId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }

    public class Status
    {
        public string StatusName { get; set; }
        public string Message { get; set; }
    }

    public class Last7DaysInstructorHours
    {
        public TimeSpan Hours { get; set; }
        public string InstructorName { get; set; }
    }

    public class InstructorsDayCalendarModel
    {
        public int TotalInstructors { get; set; }
        public string[] InstructorNames { get; set; }
        public string Instructors { get; set; }
        public string StartDate { get; set; }
    }
}