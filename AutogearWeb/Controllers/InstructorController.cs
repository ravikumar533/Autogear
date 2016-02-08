using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutogearWeb.EFModels;
using AutogearWeb.Models;
using AutogearWeb.Providers;
using AutogearWeb.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace AutogearWeb.Controllers
{
    [Authorize]
    public class InstructorController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationRoleManager _roleManager;
        private readonly IInstructorRepo _instructorRepo;
        private readonly IPostalRepo _postalRepo;
        private readonly IAutogearRepo _autogearRepo;
        private readonly IStudentRepo _studentRepo;

        public InstructorController()
        {
        }
        public InstructorController(ApplicationUserManager userManager, ApplicationRoleManager roleManager, IInstructorRepo instructorRepo,IPostalRepo postalRepo,IAutogearRepo autogearRepo,IStudentRepo studentRepo)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _instructorRepo = instructorRepo;
            _postalRepo = postalRepo;
            _autogearRepo = autogearRepo;
            _studentRepo = studentRepo;
        }

        // GET: Instructor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var model = new RegisterViewModel
            {
                GendersList = new SelectList(_autogearRepo.GenderListItems(), "Value", "Text"),
                Areas = new MultiSelectList(_instructorRepo.GetAreasList(string.Empty), "Value", "Text"),
                Status = true
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //Sending Email
                     EmailAPI emp = new EmailAPI();
                    var emails = new List<string> {model.Email};
                    var subJect = "Account Creation";
                    //Add role instructor to created user
                    // Fetch role
                    var role = await _roleManager.FindByNameAsync("Instructor");
                    if (role != null)
                    {
                         _userManager.AddToRole(user.Id, role.Name);
                    }
                    model.LastInstructor = _instructorRepo.GetLatestInstructorId() + 1;
                    string[] addressInfo = model.SuburbName.Split(',');
                    var suburb = _postalRepo.GetSuburb(addressInfo[0]);
                    model.PostalCode = addressInfo[2];
                    if (suburb != null)
                    {
                        model.SuburbId = suburb.SuburbId;
                    }
                    model.InstructorId = user.Id;
                    model.CreatedUser = User.Identity.GetUserId();
                   _instructorRepo.SaveInstructor(model);
                    //await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }
            return View(model);
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public ActionResult Calendar()
        {
            var currentUser = Request.GetOwinContext().Authentication.User;
            var currentUserId = currentUser.Identity.GetUserId();
            var currentInstructor = _instructorRepo.TblInstructors.FirstOrDefault(s=>s.InstructorId == currentUserId);
            var model = new CalendarModel
            {
                IsUserAdmin = currentUser.IsInRole("Admin"),
                InstructorList = currentInstructor != null ? new SelectList(_instructorRepo.GetInstructorNames(), "Value", "Text",currentInstructor.InstructorNumber) : new SelectList(_instructorRepo.GetInstructorNames(), "Value", "Text")
            };
            return View(model);
        }

        public ActionResult InstructorsDayCalendar()
        {
            var model = new InstructorsDayCalendarModel {InstructorNames = _instructorRepo.GetInstructors()};
            model.TotalInstructors = model.InstructorNames.Length;
            model.Instructors = string.Join(",", model.InstructorNames);
            model.StartDate = DateTime.Now.ToString("yyyy-MM-dd");
            return View(model);
        }
        [HttpPost]
        public ActionResult SaveAppointment(BookingAppointment model)
        {
            var currentUser = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                if (_instructorRepo.CheckIsAnyAppointmentsForInsturcotrOrStudent(model) && model.BookingId == 0)
                {
                    return Json(new Status { StatusName = "Error", Message = "Instructor or Student are booked in this timings" });
                }
                if (!string.IsNullOrEmpty(model.InstructorNumber))
                    {
                        var instructor = _instructorRepo.GetInstructorById(model.InstructorNumber);
                        if (instructor != null)
                            model.InstructorId = instructor.InstructorId;
                    }
                    _studentRepo.SaveStudentAppointment(model, currentUser);
                    return Json(new Status {StatusName = "Success", Message = ""});
                
                
                
            }
            model.StudentList = new SelectList(_studentRepo.GetStudents(), "Value", "Text", model.StudentId);
            model.InstructorList = new SelectList(_instructorRepo.GetInstructorNames(), "Value", "Text",
                model.InstructorNumber);
            model.DrivingTypeList = new SelectList(_autogearRepo.DrivingTypeItems(), "Value", "Text",
                model.BookingType);

            return View("BookingAppointment", model);
        }

        [HttpGet]
        public ActionResult BookingAppointment(int bookingId)
        {
            var appointment = _instructorRepo.GetBookingAppointmentById(bookingId) ?? new BookingAppointment
            {
                EndDate = DateTime.Now,
                StartDate = DateTime.Now
            };
            if (string.IsNullOrEmpty(appointment.InstructorName))
            {
                var currentUser = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
                var instructor = _instructorRepo.GetInstructorDetailsById(currentUser);
                appointment.InstructorName = instructor.FirstName + " " + instructor.LastName+" "+ instructor.InstructorNumber;
            }
            if (string.IsNullOrEmpty(appointment.BookingType))
            {
                appointment.BookingType = "Learning";
            }
            appointment.StudentList = bookingId ==0?new SelectList(_studentRepo.GetStudents(),"Value","Text"): new SelectList(_studentRepo.GetStudents(), "Value", "Text", appointment.StudentId);
            appointment.InstructorList = new SelectList(_instructorRepo.GetInstructorNames(), "Value", "Text", appointment.InstructorNumber);
            appointment.DrivingTypeList = new SelectList(_autogearRepo.DrivingTypeItems(), "Value", "Text", appointment.BookingType);
            return View(appointment);
        }

        public ActionResult Edit(string instructorId)
        {
            
            var model = _instructorRepo.GetInstructorModelByNumber(instructorId);
            model.GendersList = new SelectList(_autogearRepo.GenderListItems(), "Value", "Text");
            var areas = _instructorRepo.GetAreasList(model.AreaIds);
            model.Areas = new SelectList(areas, "Value", "Text");
            var suburb = _postalRepo.GetSuburbById(model.SuburbId);
            if (suburb != null)
            {
                model.SuburbName = suburb.Name;
                var state = _postalRepo.GetStateById(suburb.StateId);
                if (state != null)
                    model.SuburbName += "," + state.Name + "," + model.PostalCode;
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(InstructorModel model)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.Password))
                {
                    var user = _userManager.FindByEmail(model.Email);
                    var provider = Startup.DataProtectionProvider;
                    _userManager.UserTokenProvider =
                       new DataProtectorTokenProvider<ApplicationUser>(provider.Create("ASP.NET Identity"))
                       {
                           TokenLifespan = TimeSpan.FromHours(1)
                       };
                    string code =  _userManager.GeneratePasswordResetToken(user.Id);
                     _userManager.ResetPassword(user.Id, code, model.Password);
                }
                model.CreatedUser = User.Identity.GetUserId();
                string[] addressInfo = model.SuburbName.Split(',');
                var suburb = _postalRepo.GetSuburb(addressInfo[0]);
                model.PostalCode = addressInfo[2];
                if (suburb != null)
                {
                    model.SuburbId = suburb.SuburbId;
                }
                _instructorRepo.UpdateInstructor(model);
            }
            model = _instructorRepo.GetInstructorModelByNumber(model.InstructorNumber);
            model.GendersList = new SelectList(_autogearRepo.GenderListItems(), "Value", "Text");
            model.Areas = new SelectList(_instructorRepo.GetAreasList(model.AreaNames), "Value", "Text");
          
            return View(model);
        }
        public ActionResult Lesson()
        {
            return View();
        }

        public ActionResult InstructorLeaves()
        {
            return View();
        }

        public ActionResult NewLeaveByInstructor(string instructorId)
        {
            var model = new InstructorLeaveModel
            {
                StartTime = new TimeSpan(0,0,0),
                StopTime = new TimeSpan(23,59,0)
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult NewLeaveByInstructor(InstructorLeaveModel appliedLeave)
        {
            if (ModelState.IsValid)
            {
                var currentUser = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
                _instructorRepo.ApplyInstructorLeave(currentUser, appliedLeave);
                return View("InstructorLeaves");
            }

            return View();
        }

        public ActionResult UpdateLeave(int leaveId)
        {
            var model = _instructorRepo.GetInstructorLeaveById(leaveId);
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateLeave(InstructorLeaveModel updateLeave)
        {
            if (ModelState.IsValid)
            {
                var currentUser = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
                _instructorRepo.UpdateInstructorLeave(currentUser, updateLeave);
                return View("InstructorLeaves");
            }
            return View();
        }

        public ActionResult AllLeaves()
        {
            var currentUser = Request.GetOwinContext().Authentication.User;
            var model = new CalendarModel
            {
                IsUserAdmin = currentUser.IsInRole("Admin"),
                InstructorList = new SelectList(_instructorRepo.GetInstructorNames(), "Value", "Text")
            };
            return View(model);
        }

        public ActionResult UpdatePostalCodes()
        {
            var reader = new StreamReader(System.IO.File.OpenRead(Server.MapPath("~/Content/PostCodes.csv")));
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                string p = reader.ReadLine();
                if (p != null)
                {
                    string[] postalData = p.Split(',');
                    var postalCode = Convert.ToInt32(postalData[0]);
                    var suburbName = postalData[1].Replace("\"", "");
                    var stateName = postalData[2].Replace("\"", "");
                    // Creating State
                    var state = _postalRepo.GetState(stateName);
                    if (state == null)
                    {
                        state = new State
                        {
                            State_Name = stateName
                        };
                        _postalRepo.SaveState(state);
                        _postalRepo.SaveChanges();
                    }
                    var suburub = _postalRepo.GetSuburb(suburbName);
                    if (suburub == null)
                    {
                        suburub = new Suburb
                        {
                            StateId = state.StateId,
                            Suburb_Name = suburbName
                        };
                        _postalRepo.SaveSubUrb(suburub);
                        _postalRepo.SaveChanges();
                    }
                    var postCode = _postalRepo.GetPostCode(postalCode, suburub.SuburbId);
                    if (postCode == null)
                    {
                        postCode = new PostCode
                        {
                            PostCode1 = postalCode,
                            SuburbID = suburub.SuburbId
                        };
                        _postalRepo.SavePostCode(postCode);
                        _postalRepo.SaveChanges();
                    }
                }
            }
            return Json("ok");
        }

        #region Packages

        public ActionResult Packages()
        {
            return View();
        }

        public ActionResult CreatePackage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePackage(PackageModel packageDetails)
        {
            if (ModelState.IsValid)
            {
                var currentUser = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
                _instructorRepo.CreateNewPackage(currentUser, packageDetails);
                return View("Packages");
            }
            return View();
        }

        public ActionResult EditPackage(int packageId)
        {
            var model = _instructorRepo.GetPackagesById(packageId);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditPackage(PackageModel packageDetails)
        {
            if (ModelState.IsValid)
            {
                var currentUser = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
                _instructorRepo.UpdatePackageDetails(currentUser, packageDetails);
                return View("Packages");
            }
            return View();
        }

        #endregion

        #region Area

        public ActionResult Area()
        {
            return View();
        }

        public ActionResult CreateArea()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateArea(AreaModel areaDetails)
        {
            if (ModelState.IsValid)
            {
                var currentUser = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
                _instructorRepo.CreateNewArea(currentUser, areaDetails);
                return View("Area");
            }
            return View();
        }

        public ActionResult EditArea(int areaId)
        {
            var model = _instructorRepo.GetAreaById(areaId);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditArea(AreaModel areaDetails)
        {
            if (ModelState.IsValid)
            {
                var currentUser = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
                _instructorRepo.UpdateArea(currentUser, areaDetails);
                return View("Area");
            }
            return View();
        }

        #endregion

    }

}