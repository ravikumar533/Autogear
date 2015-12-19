using System;

namespace AutogearWeb.Models
{
    public class TblInstructor
    {
        public string InstructorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string Gender { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
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
}