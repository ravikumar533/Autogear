﻿using System;
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

    }

    public class InstructorBooking
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
    }

    public class BookingAppointment
    {
        public int BookingId { get; set; }
        public string StudentName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan StopTime { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
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
    }

    public class InstructorLeaveModel
    {
        public int Id { get; set; }
        public string InstructorId { get; set; }
        public string LeaveReason { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
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
        [Required]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        [Required]
        [Display(Name = "Suburb Name")]
        public string SuburbName { get; set; }
        public int SuburbId { get; set; }
       // [Required]
        [Display(Name="Area")]
        public string Areas { get; set; }
        public string CreatedUser { get; set; }
    }
}