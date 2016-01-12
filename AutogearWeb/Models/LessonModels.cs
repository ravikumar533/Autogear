using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AutogearWeb.Models
{
    public class BookingAppointment
    {
        public int BookingId { get; set; }
        [Display(Name = "Student Name")]
        public string StudentName { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Display(Name = "Instructor Name")]
        public string InstructorName { get; set; }
        [Required]
        public string InstructorNumber { get; set; }
        public string InstructorId { get; set; }
        [Required]
        [Display(Name = "Start Time")]
        public TimeSpan StartTime { get; set; }
        [Required]
        [Display(Name = "Stop Time")]
        public TimeSpan StopTime { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }
        [Required]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }
        public SelectList InstructorList { get; set; }
        public SelectList StudentList { get; set; }
    }
}
