using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutogearWeb.EFModels;
using AutogearWeb.Models;
using AutogearWeb.Repositories;
using Microsoft.AspNet.Identity;

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
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
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
                    // Create Instructor account
                    var instructor = new Instructor
                    {
                        Created_Date = DateTime.Now,
                        InstructorId = user.Id,
                        Created_By = User.Identity.GetUserId()
                    };
                    TryUpdateModel(instructor);
                    _instructorRepo.AddIntructor(instructor);
                    _instructorRepo.SaveInDatabase();
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

        public ActionResult UpdatePostalCodes()
        {
            var reader = new StreamReader(System.IO.File.OpenRead(Server.MapPath("~/Content/PostCodes.csv")));
            reader.ReadLine();
            while(!reader.EndOfStream)
            {
                string p = reader.ReadLine();
                if (p != null)
                {
                    string[] postalData = p.Split(',');
                    var postalCode = Convert.ToInt32(postalData[0]);
                    var suburbName = postalData[1].Replace("\"","");
                    var stateName = postalData[2].Replace("\"","");
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
                      suburub =  new Suburb
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
        public ActionResult Lesson()
        {
            return View();
        }

    }

}