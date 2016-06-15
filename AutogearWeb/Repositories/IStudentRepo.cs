using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutogearWeb.EFModels;
using AutogearWeb.Models;

namespace AutogearWeb.Repositories
{
    public interface IStudentRepo : IDisposable
    {
        AutogearDBEntities DataContext { get; set; }
        IQueryable<TblStudent> TblStudents { get; set; }
        IQueryable<TblStudentAddress> TblStudentAddresses { get; set; }
        IQueryable<TblStudentLicense> TblStudentLicenses { get; set; }
        IQueryable<TblBooking> TblStudentBookings { get; set; }
        IQueryable<TblPackage> TblPackageDetails { get; set; }
        IQueryable<TblState> TblStates { get; set; }
        IList<SelectListItem> GetStudents(); // Fetch Instructor Names
        Task<List<TblStudent>> GetStudentList(string searchtext, string date,Boolean addInactiveStudents);
        Task<List<TblStudent>> GetInstructorStudentList(string searchtext, string date, Boolean addInactiveStudents, string instructorId);
        IList<SelectListItem> GetPackages();
        IList<SelectListItem> GetStateList();
        Task<IList<string>> GetStudentNames();
        StudentModel GetStudentByNumber(string studentId);
        BookingAppointment GetBookingDetailsById(int bookingId);
        void SaveStudentAppointment(BookingAppointment bookingAppointment, string currentUser);
        void SaveStudent(StudentModel studentModel,string currentUser);
        TblStudent UpdateStudentDetails(string currentUser, TblStudent studentDetails);
        void SaveExistingStudent(string currentUser, StudentModel studentModel);
        List<Last7DaysRegisterDetails> GetStudentRegisterDetails();
        IList<SelectListItem> GetInstructorStudents(string instructorId);
        StudentModel GetStudentById(int studentId);
        List<string> GetStudentPickUpLocationAndMobile(string studentNumber);
    }
}
