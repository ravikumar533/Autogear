using System;
using System.Web;
using System.Web.Mvc;
using AutogearWeb.Models;
using AutogearWeb.Repositories;
using Microsoft.AspNet.Identity;

namespace AutogearWeb.Controllers
{

    public class LessonController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationRoleManager _roleManager;
        private readonly IInstructorRepo _instructorRepo;
        private readonly IPostalRepo _postalRepo;
        private readonly IAutogearRepo _autogearRepo;
        private readonly IStudentRepo _studentRepo;

        public LessonController()
        {
        }
        public LessonController(ApplicationUserManager userManager, ApplicationRoleManager roleManager, IInstructorRepo instructorRepo, IPostalRepo postalRepo, IAutogearRepo autogearRepo, IStudentRepo studentRepo)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _instructorRepo = instructorRepo;
            _postalRepo = postalRepo;
            _autogearRepo = autogearRepo;
            _studentRepo = studentRepo;
        }

        // GET: Lesson
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var model = new BookingAppointment
            {
                StudentList = new SelectList(_studentRepo.GetStudents(), "Value", "Text"),
                InstructorList = new SelectList(_instructorRepo.GetInstructorNames(), "Value", "Text"),
                DrivingTypeList = new SelectList(_autogearRepo.DrivingTypeItems(),"Value","Text")
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(BookingAppointment bookingAppointment)
        {
            if (ModelState.IsValid)
            {
                var currentUser = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
                var studentId = Convert.ToInt32(bookingAppointment.StudentId);
                var student = _studentRepo.GetStudentById(studentId);
                if (student != null)
                {
                    bookingAppointment.StudentName = student.FirstName + " " + student.LastName;
                    bookingAppointment.StudentId = student.StudentId;
                }
                if (!string.IsNullOrEmpty(bookingAppointment.InstructorNumber))
                {
                    var instructor = _instructorRepo.GetInstructorById(bookingAppointment.InstructorNumber);
                    if (instructor != null)
                    {
                        bookingAppointment.InstructorId = instructor.InstructorId;
                        bookingAppointment.InstructorName = instructor.FirstName + " " + instructor.LastName;
                    }

                }
                _studentRepo.SaveStudentAppointment(bookingAppointment, currentUser);
                return View("Index");
            }
            return View();
        }

        public ActionResult Edit(int bookingId)
        {
            var model = _studentRepo.GetBookingDetailsById(bookingId);
            model.StudentList = new SelectList(_studentRepo.GetStudents(), "Value", "Text");
            model.InstructorList = new SelectList(_instructorRepo.GetInstructorNames(), "Value", "Text");
            var instructorDetails = _instructorRepo.GetInstructorDetailsById(model.InstructorId);
            if (instructorDetails == null) return View(model);
            model.InstructorName = instructorDetails.FirstName + " " + instructorDetails.LastName;
            model.InstructorNumber = instructorDetails.InstructorNumber;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BookingAppointment bookingAppointment)
        {
            if (ModelState.IsValid)
            {
                var currentUser = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
                var studentId = Convert.ToInt32(bookingAppointment.StudentId);
                var student = _studentRepo.GetStudentById(studentId);
                if (student != null)
                {
                    bookingAppointment.StudentName = student.FirstName + " " + student.LastName;
                    bookingAppointment.StudentId = student.StudentId;
                }
                if (!string.IsNullOrEmpty(bookingAppointment.InstructorNumber))
                {
                    var instructor = _instructorRepo.GetInstructorById(bookingAppointment.InstructorNumber);
                    if (instructor != null)
                    {
                        bookingAppointment.InstructorId = instructor.InstructorId;
                        bookingAppointment.InstructorName = instructor.FirstName + " " + instructor.LastName;
                    }

                }
                _studentRepo.SaveStudentAppointment(bookingAppointment, currentUser);
                return View("Index");
            }
            return View();
        }
    }
}