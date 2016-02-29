using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AutogearWeb.Models
{
    public class TblStudent
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; }
        public DateTime? StartDate { get; set; }
        public bool Status { get; set; }
        public string InstructorName { get; set; }
        public string StudentNumber { get; set; }
        public int? AddressId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string PhoneNumber { get; set; }
        public string InstructorId { get; set; }
    }

    public class TblStudentAddress
    {
        public int AddressId { get; set; }
        public int StudentId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int PostalCode { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public int SuburbId { get; set; }
    }

    public class TblBooking
    {
        public int BookingId { get; set; }
        public int StudentId { get; set; }
        public string InstructorId { get; set; }
        public DateTime? BookingDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? StopTime { get; set; }
        public string PickupLocation { get; set; }
        public string DropLocation { get; set; }
        public int? PackageId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Type { get; set; }
        public string Discount { get; set; }
    }

    public class TblStudentLicense
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string ClassName { get; set; }
        public DateTime? LicensePassedDate { get; set; }
        public string Remarks { get; set; }
        public bool IsInternationalLicensed { get; set; }
        public string Country { get; set; }
    }

    public class TblPackage
    {
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public string PackageDescription { get; set; }
    }

    public class TblState
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
    }

    public class StudentModel
    {
        public int StudentId { get; set; }
        
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
        [Display(Name = "Gender")]
        public int Gender { get; set; }
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }
        public bool Status { get; set; }
        public SelectList InstructorList { get; set; }
        [Required]
        [Display(Name="Instructor")]
        public string InstructorNumber { get; set; }
        [Display(Name = "Instructor Remarks")]
        public string InstructorRemarks { get; set; }
        [Required]
        [Display(Name = "License Number")]
        public string LicenseNumber { get; set; }
        [Display(Name = "Expiry Date")]
        public DateTime? ExpiryDate { get; set; }
        [Display(Name="State Name")]
        public int StateId { get; set; }
        [Required]
        [Display(Name="Address1")]
        public string Address1 { get; set; }
        [Display(Name = "Address2")]
        public string Address2 { get; set; }
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        [Required]
        [Display(Name = "Suburb Name")]
        public string SuburbName { get; set; }
        [Display(Name = "Class")]
        public string ClassName { get; set; }
        [Display(Name = "Passed Date")]
        public DateTime? LicensePassedDate { get; set; }
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }
        [Display(Name = "International License")]
        public bool IsInternationalLicensed { get; set; }
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Start Date")]
        public DateTime? BookingStartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime? BookingEndDate { get; set; }
        [Display(Name = "Start Time")]
        public TimeSpan? StartTime { get; set; }
        [Display(Name = "End Time")]
        public TimeSpan? StopTime { get; set; }
        [Display(Name = "Pickup Location")]
        public string PickupLocation { get; set; }
        [Display(Name = "Drop Location")]
        public string DropLocation { get; set; }
        [Display(Name = "PackageId")]
        public int? PackageId { get; set; }
        [Display(Name = "Package Description")]
        public string PackageDetails { get; set; }
        [Display(Name = "Disacount")]
        public string PackageDisacount { get; set; }

        [Display(Name = "Test Date")]
        public DateTime? DrivingTestDate { get; set; }
        [Display(Name = "Start Time")]
        public TimeSpan? DrivingTestStartTime { get; set; }
        [Display(Name = "End Time")]
        public TimeSpan? DrivingTestStopTime { get; set; }
        [Display(Name = "Pickup Location")]
        public string DrivingTestPickupLocation { get; set; }
        [Display(Name = "Drop Location")]
        public string DrivingTestDropLocation { get; set; }
        [Display(Name = "PackageId")]
        public int? DrivingTestPackageId { get; set; }
        [Display(Name = "Package Description")]
        public string DrivingTestPackageDetails { get; set; }
        [Display(Name = "Disacount")]
        public string DrivingTestPackageDisacount { get; set; }

        public int SuburbId { get; set; }
        public string StudentNumber { get; set; }
        public IList<SelectListItem> GendersList { get; set; }
        public IList<SelectListItem> StatesList { get; set; }
        public IList<SelectListItem> LearningPackages { get; set; }
        public IList<SelectListItem> DrivingPackages { get; set; }
    }

    public class Last7DaysRegisterDetails
    {
        public int Count { get; set; }
        public string CreatedDate { get; set; }
    }
    
}
