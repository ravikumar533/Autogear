﻿using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutogearWeb.EFModels;
using AutogearWeb.Models;
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
        public InstructorController()
        {
        }
        public InstructorController(ApplicationUserManager userManager, ApplicationRoleManager roleManager, IInstructorRepo instructorRepo,IPostalRepo postalRepo,IAutogearRepo autogearRepo)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _instructorRepo = instructorRepo;
            _postalRepo = postalRepo;
            _autogearRepo = autogearRepo;
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
                GendersList = new SelectList(_autogearRepo.GenderListItems(), "Value", "Text")
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
                    //Add role instructor to created user
                    // Fetch role
                    var role = await _roleManager.FindByNameAsync("Instructor");
                    if (role != null)
                    {
                         _userManager.AddToRole(user.Id, role.Name);
                    }
                    model.LastInstructor = _instructorRepo.GetLatestInstructorId() + 1;
                    var suburb = _postalRepo.GetSuburb(model.SuburbName);
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
            var model = new CalendarModel
            {
                IsUserAdmin = currentUser.IsInRole("Admin")
            };
            return View(model);
        }

        public ActionResult BookingAppointment(int bookingId)
        {
            var appointment = _instructorRepo.GetBookingAppointmentById(bookingId);
            return View(appointment);
        }

        public ActionResult Edit(string instructorId)
        {
            var model = _instructorRepo.GetInstructorByNumber(instructorId);
            model.GendersList = new SelectList(_autogearRepo.GenderListItems(), "Value", "Text");
            var suburb = _postalRepo.GetSuburbById(model.SuburbId);
            if (suburb != null)
                model.SuburbName = suburb.Name;
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
                var suburb = _postalRepo.GetSuburb(model.SuburbName);
                if (suburb != null)
                    model.SuburbId = suburb.SuburbId;
                _instructorRepo.UpdateInstructor(model);
            }
            model = _instructorRepo.GetInstructorByNumber(model.InstructorNumber);
            model.GendersList = new SelectList(_autogearRepo.GenderListItems(), "Value", "Text");
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

            return View();
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

        public ActionResult EditPackage()
        {
            return View();
        }

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

        public ActionResult EditArea()
        {
            return View();
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