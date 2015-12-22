using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutogearWeb.EFModels;
using AutogearWeb.Models;

namespace AutogearWeb.Repositories
{
    public interface IInstructorRepo : IDisposable
    {
        AutogearDBEntities DataContext { get; set; }
  
        IQueryable<TblInstructor> TblInstructors { get; set; }
        IQueryable<TblBooking> TblBookings { get; set; }
        Task<IList<TblInstructor>> GetInstructorList(); // Fetch List
        Task<IList<InstructorBooking>> GetInstructorBookingEvents(string instructorId);
        Task<IList<StudentList>> GetStudentEvents(string currentUser); //Fetch Student List
        Task<IList<string>>  GetInstructorNames(); // Fetch Instructor Names
        Instructor GetInstructorByEmail(string email); // Fetch by Email
        Instructor GetInstructorByName(string name);
        BookingAppointment GetBookingAppointmentById(int bookingAppointmentId);
        Task<IList<InstructorLeaveModel>> GetInstructorLeaves(string currentUser);
        void AddIntructor(Instructor repo); // Add new instructor
        void SaveInDatabase();  // Save Asynchronous
        void ApplyInstructorLeave(string currentUser, InstructorLeaveModel appliedLeave);
    }
}
