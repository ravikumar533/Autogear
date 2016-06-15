using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutogearWeb.Models;
using AutogearWeb.Repositories;
using Microsoft.AspNet.Identity;

namespace AutogearWeb.Controllers
{
    [Authorize]
    public class AutogearApiController : ApiController
    {
        private readonly IInstructorRepo _instructorRepo;
        private readonly IStudentRepo _studentRepo;
        private readonly IPostalRepo _postalRepo;
        private readonly ApplicationUserManager _userManager;

        public AutogearApiController()
        {
            
        }

        public AutogearApiController(IInstructorRepo instructorRepo, IStudentRepo studentRepo, IPostalRepo postalRepo, ApplicationUserManager userManager)
        {
            _instructorRepo = instructorRepo;
            _studentRepo = studentRepo;
            _postalRepo = postalRepo;
            _userManager = userManager;
        }

        public async Task<IList<TblInstructor>> GetInstructors(string searchtext, string area)
        {
            return await _instructorRepo.GetInstructorList(searchtext,area);
        }

        public async Task<IList<TblStudent>> GetStudents(string searchtext, string phoneNumber, Boolean addInactiveStudents)
        {
            if (User.IsInRole("Admin"))
                return await _studentRepo.GetStudentList(searchtext, phoneNumber, addInactiveStudents);
            else
                return await _studentRepo.GetInstructorStudentList(searchtext, phoneNumber, addInactiveStudents, User.Identity.GetUserId());
        }

        public async Task<IList<int>> GetPostCodes(string suburbName)
        {
            return await _postalRepo.GetPostcodes(suburbName);
        }

        public IList<string> GetSubUrbs()
        {
            return  _postalRepo.GetSuburbNames();
        }

        public async Task<IList<TblPostCodeSuburbModel>> GetPostalCodewithSuburbs()
        {
            return await _postalRepo.GetPostCodeWithSuburbs();
        }
        //commented by RK
        //public async Task<IList<string>> GetInstructorNames()
        //{
        //    return await _instructorRepo.GetInstructorNames();
        //}
       
        public async Task<IList<StudentList>> GetStudentEvents()
        {
            var currentUser = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
            return await _instructorRepo.GetStudentEvents(currentUser);
        }
        public IList<string> GetStudentPickUpandMobile(string studentNumber)
        {
            return _studentRepo.GetStudentPickUpLocationAndMobile(studentNumber);
        }

        [HttpGet]
        public async Task<IList<InstructorBooking>> GetBookingEvents(string instructorNumber)
        {
            var currentUser = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
            if (!string.IsNullOrEmpty(instructorNumber))
            {
                var instructor = _instructorRepo.GetInstructorById(instructorNumber);
                if (instructor != null)
                    currentUser = instructor.InstructorId;
            }
            return await _instructorRepo.GetInstructorBookingEvents(currentUser);
        }
        public async Task<IList<InstructorBooking>> GetInstructorDayEvents(string start)
        {
            DateTime startDate = Convert.ToDateTime(start);
            return await _instructorRepo.GetInstructorsDayEvents(startDate);
        }

        public async Task<IList<string>> GetStudentNames()
        {
            return await _studentRepo.GetStudentNames();
        }

        public void SaveBookingAppointment(BookingAppointment bookingAppointment)
        {
            var currentUser = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
            if (!string.IsNullOrEmpty(bookingAppointment.InstructorNumber))
            {
                var instructor = _instructorRepo.GetInstructorById(bookingAppointment.InstructorNumber);
                if (instructor != null)
                    bookingAppointment.InstructorId  = instructor.InstructorId;
            }
            _studentRepo.SaveStudentAppointment(bookingAppointment, currentUser);
        }

        public async Task<IList<InstructorLeaveModel>> GetInstructorLeaves()
        {
            var currentUser = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
            return await _instructorRepo.GetInstructorLeaves(currentUser);
        }

        public async Task<IList<AllInstructorsLeavesModel>> GetAllInstructorsLeaves()
        {
            return await _instructorRepo.GetAllInstructorsLeaves();
        }

        public async Task<IList<AllInstructorsLeavesModel>> GetInstructorLeaveByName(string instructorNumber)
        {
            return await _instructorRepo.GetInstructorLeavesByName(instructorNumber);
        }

        public void UpdateStudentDetails(TblStudent studentDetails)
        {
            var currentUser = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
            _studentRepo.UpdateStudentDetails(currentUser, studentDetails);
        }

        #region Packages

        public async Task<IList<PackageModel>> GetPackages()
        {
            return await _instructorRepo.GetPackages();
        }

        public void CreateNewPackage(PackageModel packageDetails)
        {
            var currentUser = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
            _instructorRepo.CreateNewPackage(currentUser, packageDetails);
        }

        public void UpdatePackageDetails(PackageModel packageDetails)
        {
            var currentUser = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
            _instructorRepo.UpdatePackageDetails(currentUser, packageDetails);
        }

        #endregion

        #region Area

        public async Task<IList<AreaModel>> GetArea()
        {
            return await _instructorRepo.GetArea();
        }

        #endregion

        #region Student Register Details

        public List<Last7DaysRegisterDetails> GetStudentRegisterDetails()
        {
            return _studentRepo.GetStudentRegisterDetails();
        }

        #endregion

        #region Instructor Hour Details in Last 7 Days

        public List<Last7DaysInstructorHours> GetLast7DaysInstructorHour()
        {
            return _instructorRepo.GetLast7DaysInstructorHour();
        }

        #endregion
    }
}
