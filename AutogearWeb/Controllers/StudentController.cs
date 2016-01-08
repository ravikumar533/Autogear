﻿using System.Web;
using System.Web.Mvc;
using AutogearWeb.Models;
using AutogearWeb.Repositories;
using Microsoft.AspNet.Identity;

//using Microsoft.AspNet.Identity;

namespace AutogearWeb.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IStudentRepo _iStudentRepo;
        private readonly IAutogearRepo _iAutogearRepo;
        private readonly IPostalRepo _iPostalRepo;
        public StudentController(IStudentRepo iStudentRepo,IAutogearRepo autogearRepo,IPostalRepo postalRepo)
        {
            _iStudentRepo = iStudentRepo;
            _iAutogearRepo = autogearRepo;
            _iPostalRepo = postalRepo;
        }
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var model = new StudentModel
            {
                GendersList = _iAutogearRepo.GenderListItems(),
                StatesList = _iStudentRepo.GetStateList(),
                LearningPackages = _iStudentRepo.GetPackages(),
                DrivingPackages = _iStudentRepo.GetPackages()
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentModel model)
        {
            var currentUser = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
            var packages = _iStudentRepo.GetPackages();
            if (ModelState.IsValid)
            {
                var suburb = _iPostalRepo.GetSuburb(model.SuburbName);
                if (suburb != null)
                    model.SuburbId = suburb.SuburbId;
                _iStudentRepo.SaveStudent(model,currentUser);
                return RedirectToAction("Index");
                //AddErrors(result);
            }
            model.GendersList = _iAutogearRepo.GenderListItems();
            model.StatesList = _iStudentRepo.GetStateList();
            model.LearningPackages = packages;
            model.DrivingPackages = packages;
            return View(model);
        }
        public ActionResult Edit(int studentId)
        {
            var model = _iStudentRepo.GetStudentById(studentId);
            model.GendersList = _iAutogearRepo.GenderListItems();
            
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentModel studentModel)
        {
            var currentUser = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                _iStudentRepo.SaveExistingStudent(currentUser, studentModel);
                return RedirectToAction("Index");
            }
            
            return View(studentModel);
        }
        public ActionResult Register()
        {
            return View();
        }

        //private void AddErrors(IdentityResult result)
        //{
        //    foreach (var error in result.Errors)
        //    {
        //        ModelState.AddModelError("", error);
        //    }
        //}
    }
}