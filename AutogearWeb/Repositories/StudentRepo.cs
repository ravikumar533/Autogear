using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutogearWeb.EFModels;
using AutogearWeb.Models;

namespace AutogearWeb.Repositories
{
    public class StudentRepo : IStudentRepo
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public AutogearDBEntities DataContext { get; set; }

        public StudentRepo()
        {
            DataContext = new AutogearDBEntities();
        }

        private IQueryable<TblStudent> _tblStudents;
        public IQueryable<TblStudent> TblStudents
        {
            get
            {

                 _tblStudents = _tblStudents ??
                     (from student in DataContext.Students
                        let instructorStudent =
                            DataContext.Instructor_Student.FirstOrDefault(s => s.StudentId == student.Id)
                        where instructorStudent != null
                        select new TblStudent
                        {
                            StudentId = student.Id,
                            FirstName = student.FirstName,
                            LastName = student.LastName,
                            Gender = student.Gender,
                            StartDate = student.StartDate,
                            Status = student.Status,
                            AddressId = student.AddressId,
                            Email = student.Email,
                            InstructorName = instructorStudent.Instructor.FirstName + " " + instructorStudent.Instructor.LastName,
                            CreatedDate = student.CreatedDate,
                            StudentNumber = student.StudentNumber
                        });
                return _tblStudents;

            }
            set { _tblStudents = value; }
        }

        private IQueryable<TblStudentAddress> _tblStudentAddresses;
        public IQueryable<TblStudentAddress> TblStudentAddresses
        {
            get
            {
                _tblStudentAddresses = _tblStudentAddresses ??
                    
                                       DataContext.Addresses.Select(s => new TblStudentAddress {Address1 = s.Address1,Address2 = s.AddressLine2 , Mobile = s.Mobile, AddressId = s.AddressId ,Phone = s.Phone ,PostalCode = s.PostCode, SuburbId = s.SuburbID});
                return _tblStudentAddresses;
            }
            set { _tblStudentAddresses = value; }
        }

        private IQueryable<TblStudentLicense> _tblStudentLicenses;
        public IQueryable<TblStudentLicense> TblStudentLicenses
        {
            get
            {
                _tblStudentLicenses = _tblStudentLicenses ??
                                      DataContext.Student_License.Select(
                                          s =>
                                              new TblStudentLicense
                                              {
                                                  StudentId = s.StudentId,
                                                  ClassName = s.Class,
                                                  ExpiryDate = s.ExpiryDate,
                                                  Id = s.Id,
                                                  LicenseNumber = s.LicenseNo,
                                                  LicensePassedDate = s.License_passed_Date,
                                                  Remarks = s.Remarks,
                                                  IsInternationalLicensed = s.IsInternationalLicensed,
                                                  Country = s.Country
                                              });
                return _tblStudentLicenses;
            }
            set { _tblStudentLicenses = value; }
        }

        private IQueryable<TblBooking> _tblBookings;
        public IQueryable<TblBooking> TblStudentBookings
        {
            get
            {
                _tblBookings = _tblBookings ?? DataContext.Bookings.Select(s => new TblBooking { BookingId = s.BookingId, BookingDate = s.BookingDate, DropLocation = s.DropLocation, PickupLocation = s.PickupLocation, PackageId = s.PackageId, StartTime = s.StartTime, StopTime = s.EndTime, StartDate = s.StartDate, EndDate = s.EndDate, InstructorId = s.InstructorId, StudentId = s.StudentId, Type = s.Type, Discount = s.Discount });
                return _tblBookings;
            }
            set { _tblBookings = value;  }
        }

        private IQueryable<TblPackage> _tblPackages;
        public IQueryable<TblPackage> TblPackageDetails
        {
            get
            {
                _tblPackages = _tblPackages ?? DataContext.Package_Details.Select(s => new TblPackage {PackageId = s.PackageId, PackageDescription = s.Description, PackageName = s.Name});
                return _tblPackages;
            }
            set { _tblPackages = value; }
        }

        private IQueryable<TblState> _tblStateses;

        public IQueryable<TblState> TblStates
        {
            get
            {
                _tblStateses = _tblStateses ??
                               DataContext.States.Select(
                                   s => new TblState {StateId = s.StateId, StateName = s.State_Name});
                return _tblStateses;
            }
            set { _tblStateses = value; }
        }

        public async Task<List<TblStudent>> GetStudentList(string searchtext, string date, Boolean addInactiveStudents)
        {
            var results = (addInactiveStudents == false) ? TblStudents.Where(s => s.Status) : TblStudents;
            if (!string.IsNullOrEmpty(searchtext))
                results =
                    results.Where(
                        s =>
                            ((s.FirstName + " " + s.LastName).Contains(searchtext)) ||
                            (s.StudentNumber.Contains(searchtext)));
            if (!string.IsNullOrEmpty(date))
            {
                var startdate = Convert.ToDateTime(date);
                results = results.Where(s => s.StartDate >= startdate);
            }
            return await results.ToListAsync();
        }

        public IList<SelectListItem> GetPackages()
        {
            var packages = new List<SelectListItem> { new SelectListItem { Value = "", Text = "" } };
            foreach (var student in TblPackageDetails)
            {
                var name = student.PackageName;
                var packageId = student.PackageId;
                packages.Add(new SelectListItem { Value = packageId.ToString(), Text = name });
            }
            return packages;
        }

        public IList<SelectListItem> GetStateList()
        {
            var states = new List<SelectListItem> { new SelectListItem { Value = "", Text = "" } };
            foreach (var state in TblStates)
            {
                var name = state.StateName;
                var packageId = state.StateId;
                states.Add(new SelectListItem { Value = packageId.ToString(), Text = name });
            }
            return states;
        }

        public async Task<IList<string>> GetStudentNames()
        {
            return await TblStudents.Select(s => s.FirstName + " " + s.LastName).ToListAsync();
        }

        public IList<SelectListItem> GetStudents()
        {
            var students = new List<SelectListItem> { new SelectListItem { Value = "", Text = "" } };
            foreach (var student in TblStudents)
            {
                var name = student.FirstName + " " + student.LastName;
                var studentNumber = student.StudentId;
                students.Add(new SelectListItem { Value = studentNumber.ToString(), Text = name  });
            }
            return students;
        } 
        
        // Fetch Instructor Names
        // ReSharper disable once FunctionComplexityOverflow
        public StudentModel GetStudentByNumber(string studentId)
        {
            var student = TblStudents.FirstOrDefault(s => s.StudentNumber == studentId);
            var packages = GetPackages();
            var studentModel = new StudentModel
            {
                StatesList = GetStateList(),
                LearningPackages = packages,
                DrivingPackages = packages
            };
            if (student != null)
            {
               // student Info 
                studentModel.StudentId = student.StudentId;
                studentModel.FirstName = student.FirstName;
                studentModel.LastName = student.LastName;
                studentModel.Email = student.Email;
                studentModel.Gender = student.Gender;
                studentModel.StartDate = student.StartDate;
                studentModel.Status = student.Status;
                
                //Student Address
                var studentAddress = TblStudentAddresses.FirstOrDefault(s => s.AddressId == student.AddressId);
                if (studentAddress != null)
                {
                    studentModel.Address1 = studentAddress.Address1;
                    studentModel.Address2 = studentAddress.Address2;
                    studentModel.Mobile = studentAddress.Mobile;
                    studentModel.PostalCode = studentAddress.PostalCode.ToString();
                    var subUrb = DataContext.Suburbs.SingleOrDefault(s => s.SuburbId == studentAddress.SuburbId);
                    if (subUrb != null)
                    {
                        studentModel.SuburbName = subUrb.Suburb_Name;
                        studentModel.SuburbId = subUrb.SuburbId;
                    }
                }

                //Student License
                var studentLicense = TblStudentLicenses.FirstOrDefault(s => s.StudentId == student.StudentId);
                if (studentLicense != null)
                {
                    studentModel.LicenseNumber = studentLicense.LicenseNumber;
                    studentModel.LicensePassedDate = studentLicense.LicensePassedDate;
                    studentModel.ExpiryDate = studentLicense.ExpiryDate;
                    studentModel.ClassName = studentLicense.ClassName;
                    studentModel.Remarks = studentLicense.Remarks;
                    studentModel.IsInternationalLicensed = studentLicense.IsInternationalLicensed;
                    studentModel.Country = studentLicense.Country;
                    var instructor = DataContext.Instructor_Student.SingleOrDefault(s => s.StudentId == student.StudentId);
                    if (instructor != null)
                    {
                        studentModel.InstructorRemarks = instructor.Remarks;
                        var instructorDetails = DataContext.Instructors.SingleOrDefault(s => s.InstructorId == instructor.InstructorId);
                        if (instructorDetails != null)
                        {
                            studentModel.InstructorNumber = instructorDetails.InstructorNumber;
                        }
                    }
                }

                var studentBookings = TblStudentBookings.Where(s => s.StudentId == student.StudentId && s.Type == "Learning").ToList();
                if (studentBookings.Count>0)
                {
                    var firstBooking = studentBookings[0];
                    var lastBooking = studentBookings[studentBookings.Count - 1];
                    studentModel.BookingStartDate = firstBooking.StartDate;
                    studentModel.BookingEndDate = lastBooking.EndDate;
                    studentModel.PickupLocation = firstBooking.PickupLocation;
                    studentModel.StartTime = firstBooking.StartTime;
                    studentModel.StopTime = firstBooking.StopTime;
                    studentModel.DropLocation = firstBooking.DropLocation;
                    studentModel.PackageDisacount = firstBooking.Discount;
                    var package = packages.SingleOrDefault(s => s.Value == firstBooking.PackageId.ToString());
                    if (package != null)
                    {
                        if (!string.IsNullOrEmpty(package.Value))
                            studentModel.PackageId = Convert.ToInt32(package.Value);
                        studentModel.PackageDetails = package.Text;
                    } 

                }

                var studentDrivingTestBooking = TblStudentBookings.FirstOrDefault(s => s.StudentId == student.StudentId && s.Type == "Driving");
                if (studentDrivingTestBooking != null)
                {
                    studentModel.DrivingTestDate  = studentDrivingTestBooking.BookingDate;
                    studentModel.DrivingTestPickupLocation = studentDrivingTestBooking.PickupLocation;
                    studentModel.DrivingTestStartTime = studentDrivingTestBooking.StartTime;
                    studentModel.DrivingTestStopTime = studentDrivingTestBooking.StopTime;
                    studentModel.DrivingTestDropLocation = studentDrivingTestBooking.DropLocation;
                    studentModel.DrivingTestPackageDisacount = studentDrivingTestBooking.Discount;
                    var package = packages.SingleOrDefault(s => s.Value == studentDrivingTestBooking.PackageId.ToString());
                    if (package != null)
                    {
                        if (!string.IsNullOrEmpty(package.Value))
                            studentModel.DrivingTestPackageId = Convert.ToInt32(package.Value);
                        studentModel.DrivingTestPackageDetails = package.Text;
                    } 
                }
                
            }
            return studentModel;
        }
        public void SaveStudentAppointment(BookingAppointment bookingAppointment, string currentUser)
        {
            
            var studentDetails =
                TblStudents.FirstOrDefault(s => s.StudentId == bookingAppointment.StudentId);
            if (studentDetails != null)
            {

                var startDate = Convert.ToDateTime(bookingAppointment.StartDate);
                var endDate = Convert.ToDateTime( bookingAppointment.EndDate);
                var days =endDate.Subtract(startDate).Days;
                if (startDate == endDate)
                    days += 1;
                for (var i = 0; i < days; i++)
                {
                    var bookingDate = new DateTime(startDate.Year, startDate.Month, startDate.Day + i);
                    var bookingDetails =
                        DataContext.Bookings.FirstOrDefault(s => s.BookingId == bookingAppointment.BookingId && s.StartDate == bookingAppointment.StartDate ) ??
                        new Booking();

                    bookingDetails.InstructorId = bookingAppointment.InstructorId;
                    bookingDetails.BookingDate = DateTime.Now;
                    bookingDetails.StartTime = bookingAppointment.StartTime;
                    bookingDetails.EndTime = bookingAppointment.StopTime;
                    bookingDetails.Status = bookingAppointment.BookingType;
                    bookingDetails.StudentId = studentDetails.StudentId;
                    bookingDetails.CreatedBy = currentUser;
                    bookingDetails.CreatedDate = DateTime.Now;
                    bookingDetails.StartDate = bookingDate;
                    bookingDetails.EndDate = bookingDate;
                    bookingDetails.PickupLocation = bookingAppointment.PickupLocation;
                    bookingDetails.Type = bookingAppointment.BookingType;
                    bookingDetails.Remarks = bookingAppointment.RemarksForInstructor;
                    if (bookingAppointment.BookingId == 0)
                        DataContext.Bookings.Add(bookingDetails);
                    DataContext.SaveChanges();
                }
            }
        }

        public BookingAppointment GetBookingDetailsById(int bookingId)
        {
            var bookingDetailsById = new BookingAppointment();
            var bookingDetails = TblStudentBookings.SingleOrDefault(s => s.BookingId == bookingId);
            if (bookingDetails != null)
            {
                bookingDetailsById.BookingId = bookingDetails.BookingId;
                bookingDetailsById.InstructorId = bookingDetails.InstructorId;
                bookingDetailsById.StartDate = bookingDetails.StartDate;
                bookingDetailsById.EndDate = bookingDetails.EndDate;
                bookingDetailsById.BookingType = bookingDetails.Type;
                bookingDetailsById.PickupLocation = bookingDetails.PickupLocation;
                if (bookingDetails.StopTime != null) bookingDetailsById.StopTime = (TimeSpan) bookingDetails.StopTime;
                if (bookingDetails.StartTime != null) bookingDetailsById.StartTime = (TimeSpan) bookingDetails.StartTime;
                var studentDetails = TblStudents.SingleOrDefault(s => s.StudentId == bookingDetails.StudentId);
                if (studentDetails != null)
                {
                    bookingDetailsById.StudentId = studentDetails.StudentId;
                    bookingDetailsById.StudentName = studentDetails.FirstName + " " + studentDetails.LastName;
                }

            }
            return bookingDetailsById;
        }

        public void SaveStudent(StudentModel studentModel,string currentUser)
        {
            if (currentUser != null)
            {
                //Student Addresses
                var studentAddress = new Address
                {
                    Address1 = studentModel.Address1,
                    AddressLine2 = studentModel.Address2,
                    CreatedBy = currentUser,
                    CreatedDate = DateTime.Now,
                    Mobile = studentModel.Mobile,
                };
                if (Regex.IsMatch(studentModel.PostalCode, @"\d"))
                {
                    studentAddress.PostCode = Convert.ToInt32(studentModel.PostalCode);
                    // studentAddress.
                }
                if (studentModel.SuburbId != 0)
                    studentAddress.SuburbID = studentModel.SuburbId;
                DataContext.Addresses.Add(studentAddress);
                DataContext.SaveChanges();
                // Student Creation
                // last student
                var lastStudent = TblStudents.ToList().OrderByDescending(o => o.StudentId).FirstOrDefault();
                var studentNumber = "0001";
                if (lastStudent != null)
                {
                    var year = Convert.ToInt32(lastStudent.StudentNumber.Substring(3, 4));
                    var number = Convert.ToInt32(lastStudent.StudentNumber.Substring(7, 4));
                    if (DateTime.Now.Year == year)
                        studentNumber = (number + 1).ToString("0000");
                }
                var student = new Student
                {
                    FirstName = studentModel.FirstName,
                    Email = studentModel.Email,
                    LastName = studentModel.LastName,
                    Gender = studentModel.Gender,
                    Status = studentModel.Status,
                    CreatedBy = currentUser,
                    CreatedDate = DateTime.Now,
                    StartDate = studentModel.StartDate,
                    AddressId = studentAddress.AddressId,
                    StudentNumber = "SID" + DateTime.Now.Year.ToString() + studentNumber
                };
                DataContext.Students.Add(student);
                DataContext.SaveChanges();
                
                //License Information
                var studentLicense = new Student_License
                {
                    StudentId = student.Id,
                    ExpiryDate = Convert.ToDateTime(studentModel.ExpiryDate),
                    StateId = studentModel.StateId,
                    Class = studentModel.ClassName,
                    LicenseNo = studentModel.LicenseNumber,
                    License_passed_Date =studentModel.LicensePassedDate,
                    Remarks = studentModel.Remarks,
                    //International License details
                    IsInternationalLicensed = studentModel.IsInternationalLicensed,
                    Country = studentModel.Country
                };
                DataContext.Student_License.Add(studentLicense);
                DataContext.SaveChanges();

                if (!string.IsNullOrEmpty(studentModel.InstructorNumber))
                {
                    var instructor =
                        DataContext.Instructors.SingleOrDefault(
                            s => (s.InstructorNumber) == studentModel.InstructorNumber);
                    // Booking Information
                    if (instructor != null)
                    {

                        //Save instructor student
                        var instructorStudent = new Instructor_Student
                        {
                            InstructorId = instructor.InstructorId,
                            StudentId = student.Id,
                            CreatedBy = currentUser,
                            CreatedDate = DateTime.Now,
                        };
                        DataContext.Instructor_Student.Add(instructorStudent);
                        DataContext.SaveChanges();
                        var studentBooking = new Booking
                        {
                            StudentId = student.Id,
                            PackageId = studentModel.PackageId,
                            CreatedDate = DateTime.Now,
                            BookingDate = DateTime.Now,
                            StartDate = studentModel.BookingStartDate,
                            EndDate = studentModel.BookingEndDate,
                            InstructorId = instructor.InstructorId,
                            StartTime = studentModel.StartTime,
                            EndTime = studentModel.StopTime,
                            Discount = studentModel.PackageDisacount,
                            PickupLocation = studentModel.PickupLocation,
                            DropLocation = studentModel.DropLocation,
                            CreatedBy = currentUser,
                            Type = "Learning"
                        };
                        DataContext.Bookings.Add(studentBooking);
                        DataContext.SaveChanges();
                     
                        // Driving Test Information
                        var studentDrivingBooking = new Booking
                        {
                            StudentId = student.Id,
                            PackageId = studentModel.DrivingTestPackageId,
                            CreatedDate = DateTime.Now,
                            BookingDate = studentModel.DrivingTestDate,
                            InstructorId = instructor.InstructorId,
                            StartTime = studentModel.DrivingTestStartTime,
                            EndTime = studentModel.DrivingTestStopTime,
                            Discount = studentModel.DrivingTestPackageDisacount,
                            PickupLocation = studentModel.DrivingTestPickupLocation,
                            DropLocation = studentModel.DrivingTestDropLocation,
                            CreatedBy = currentUser,
                            Type = "Driving"
                        };
                        DataContext.Bookings.Add(studentDrivingBooking);
                        DataContext.SaveChanges();
                    }
                   
                }
            }
        }

        public void SaveExistingStudent(string currentUser, StudentModel studentModel)
        {
            var studentDetails = DataContext.Students.FirstOrDefault(s => s.Id == studentModel.StudentId) ??
                                 new Student();
            var addressDetails = DataContext.Addresses.FirstOrDefault(s => s.AddressId == studentDetails.AddressId) ?? new Address();
            addressDetails.Address1 = studentModel.Address1;
            addressDetails.AddressLine2 = studentModel.Address2;
            addressDetails.Mobile = studentModel.Mobile;
            addressDetails.PostCode = Convert.ToInt32(studentModel.PostalCode);
            addressDetails.ModifiedDate = DateTime.Now;
            addressDetails.ModifiedBy = currentUser;
            if (Regex.IsMatch(studentModel.PostalCode, @"\d"))
            {
                addressDetails.PostCode = Convert.ToInt32(studentModel.PostalCode);
                // studentAddress.
            }
            if (studentModel.SuburbId != 0)
                addressDetails.SuburbID = studentModel.SuburbId;

            if (addressDetails.AddressId == 0)
            {
                addressDetails.CreatedBy = currentUser;
                addressDetails.CreatedDate = DateTime.Now;
                DataContext.Addresses.Add(addressDetails);
            }
            DataContext.SaveChanges();

            
            studentDetails.FirstName = studentModel.FirstName;
            studentDetails.LastName = studentModel.LastName;
            studentDetails.Email = studentModel.Email;
            studentDetails.StartDate = studentModel.StartDate;
            studentDetails.Status = studentModel.Status;
            studentDetails.Gender = studentModel.Gender;
            studentDetails.AddressId = addressDetails.AddressId;
            if (studentDetails.Id == 0)
            {
                DataContext.Students.Add(studentDetails);
            }
            DataContext.SaveChanges();

            var studentLicenseDetails =
                DataContext.Student_License.FirstOrDefault(s => s.StudentId == studentModel.StudentId) ??
                new Student_License();
            studentLicenseDetails.LicenseNo = studentModel.LicenseNumber;
            studentLicenseDetails.License_passed_Date = studentModel.LicensePassedDate;
            studentLicenseDetails.Class = studentModel.ClassName;
            if (studentModel.ExpiryDate != null) studentLicenseDetails.ExpiryDate = (DateTime) studentModel.ExpiryDate;
            studentLicenseDetails.Remarks = studentModel.Remarks;
            if (studentModel.IsInternationalLicensed)
            {
                studentLicenseDetails.IsInternationalLicensed = studentModel.IsInternationalLicensed;
                studentLicenseDetails.Country = studentModel.Country;
                studentLicenseDetails.StateId = null;
            }
            else
            {
                studentLicenseDetails.IsInternationalLicensed = studentModel.IsInternationalLicensed;
                studentLicenseDetails.Country = null;
                studentLicenseDetails.StateId = studentModel.StateId;
            }
            if (studentLicenseDetails.StudentId == 0)
            {
                DataContext.Student_License.Add(studentLicenseDetails);
            }
            DataContext.SaveChanges();

            var instructor = DataContext.Instructors.SingleOrDefault(s => s.InstructorNumber == studentModel.InstructorNumber);
            var instructorDetails = DataContext.Instructor_Student.FirstOrDefault(s => s.StudentId == studentModel.StudentId);
            if (instructorDetails != null)
            {
                if (instructor != null)
                {
                    instructorDetails.InstructorId = instructor.InstructorId;
                    instructorDetails.Remarks = studentModel.InstructorRemarks;
                }
                instructorDetails.ModifiedBy = currentUser;
                instructorDetails.ModifiedDate = DateTime.Now;
                DataContext.SaveChanges();
            }

        }

        public TblStudent UpdateStudentDetails(string currentUser, TblStudent studentDetails)
        {
            
            return studentDetails;
        }

        public List<Last7DaysRegisterDetails> GetStudentRegisterDetails()
        {
            var last7DaysRegisteredStudents = new List<Last7DaysRegisterDetails>();
            var last7Day = DateTime.Now.AddDays(-7.0);
            for (var dt = last7Day; dt < DateTime.Now.Date; dt = dt.AddDays(1))
            {
                last7DaysRegisteredStudents.Add(new Last7DaysRegisterDetails
                {

                    Count = TblStudents.Count(s => DbFunctions.TruncateTime(s.CreatedDate) == dt.Date),
                    CreatedDate = dt.Date.ToShortDateString()
                });

            }
            return last7DaysRegisteredStudents;
        }
    }
}