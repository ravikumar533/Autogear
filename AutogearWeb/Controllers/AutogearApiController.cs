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

        public AutogearApiController()
        {
            
        }

        public AutogearApiController(IInstructorRepo instructorRepo,IStudentRepo studentRepo,IPostalRepo postalRepo)
        {
            _instructorRepo = instructorRepo;
            _studentRepo = studentRepo;
            _postalRepo = postalRepo;
        }

        public async Task<IList<TblInstructor>> GetInstructors()
        {
            return await _instructorRepo.GetInstructorList();
        }

        public async Task<IList<TblStudent>> GetStudents()
        {
            return await _studentRepo.GetStudentList();
        }

        public async Task<IList<int>> GetPostCodes(string suburbName)
        {
            return await _postalRepo.GetPostcodes(suburbName);
        }

        public async Task<IList<string>> GetSubUrbs(int? postCode)
        {
            return await _postalRepo.GetSuburbNames(postCode);
        }

        public async Task<IList<TblPostCodeSuburbModel>> GetPostalCodewithSuburbs()
        {
            return await _postalRepo.GetPostCodeWithSuburbs();
        }

        public async Task<IList<string>> GetInstructorNames()
        {
            return await _instructorRepo.GetInstructorNames();
        }
       
        public async Task<IList<StudentList>> GetStudentEvents()
        {
             var currentUser = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
            return await _instructorRepo.GetStudentEvents(currentUser);
        }

        public async Task<IList<InstructorBooking>> GetBookingEvents(string instructorName)
        {
            var currentUser = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
            if (!string.IsNullOrEmpty(instructorName))
            {
                var instructor = _instructorRepo.GetInstructorByName(instructorName);
                if (instructor != null)
                    currentUser = instructor.InstructorId;
            }
            return await _instructorRepo.GetInstructorBookingEvents(currentUser);
        }

        public async Task<IList<string>> GetStudentNames()
        {
            return await _studentRepo.GetStudentNames();
        }

        public void SaveBookingAppointment(BookingAppointment bookingAppointment)
        {
            var currentUser = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
            _studentRepo.SaveStudentAppointment(bookingAppointment, currentUser);
        }

        public async Task<IList<InstructorLeaveModel>> GetInstructorLeaves()
        {
            var currentUser = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
            return await _instructorRepo.GetInstructorLeaves(currentUser);
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
    }
}
