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

        IQueryable<TblAddress> TblAddresses { get; set; }
        IQueryable<TblInstructor> TblInstructors { get; set; }
        IQueryable<TblBooking> TblBookings { get; set; }
        Task<IList<TblInstructor>> GetInstructorList(); // Fetch List
        Task<IList<InstructorBooking>> GetInstructorBookingEvents(string instructorId);
        Task<IList<StudentList>> GetStudentEvents(string currentUser); //Fetch Student List
        Task<IList<string>>  GetInstructorNames(); // Fetch Instructor Names
        Instructor GetInstructorByEmail(string email); // Fetch by Email
        Instructor GetInstructorByName(string name);
        InstructorModel GetInstructorByNumber(string instructorNumber);
        BookingAppointment GetBookingAppointmentById(int bookingAppointmentId);
        Task<IList<InstructorLeaveModel>> GetInstructorLeaves(string currentUser);
        int GetLatestInstructorId();
        Task<IList<PackageModel>> GetPackages();
        Task<IList<AreaModel>> GetArea();
        void SaveInstructor(RegisterViewModel model);
        void UpdateInstructor(InstructorModel model);
        void SaveInDatabase();  // Save Asynchronous
        void ApplyInstructorLeave(string currentUser, InstructorLeaveModel appliedLeave);
        void CreateNewPackage(string currentUser, PackageModel packageDetails);
        void UpdatePackageDetails(string currentUser, PackageModel packageDetails);
        void CreateNewArea(string currentUser, AreaModel areaDetails);
        void UpdateArea(string currentUser, AreaModel areaDetails);
    }
}
