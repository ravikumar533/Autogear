using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutogearWeb.EFModels;
using AutogearWeb.Models;

namespace AutogearWeb.Repositories
{
    public class InstructorRepo : IInstructorRepo
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public AutogearDBEntities DataContext { get; set; }

        public InstructorRepo()
        {
            DataContext = new AutogearDBEntities();
        }

        private IQueryable<TblAddress> _tblAddresses;

        public IQueryable<TblAddress> TblAddresses
        {
            get
            {
                _tblAddresses = _tblAddresses ?? DataContext.Addresses.Select(s => new TblAddress
                {
                    AddressId = s.AddressId,
                    Mobile = s.Mobile,
                    Phone = s.Phone,
                    AddressLine1 = s.Address1,
                    AddressLine2 = s.AddressLine2,
                    SuburbId = s.SuburbID,
                    PostalCode = s.PostCode
                });
                return _tblAddresses;
            }
            set { _tblAddresses = value; }
        }

        private IQueryable<TblBooking> _tblBookings;
        public IQueryable<TblBooking> TblBookings
        {
            get
            {
                _tblBookings = _tblBookings ??
                               DataContext.Bookings.Select(
                                   s =>
                                       new TblBooking
                                       {
                                           InstructorId = s.InstructorId,
                                           BookingId = s.BookingId,
                                           StartTime = s.StartTime,
                                           StopTime = s.EndTime,
                                           BookingDate = s.BookingDate,
                                           StudentId = s.StudentId,
                                           StartDate = s.StartDate,
                                           EndDate = s.EndDate,
                                           DropLocation = s.DropLocation,
                                           PickupLocation =s.PickupLocation,
                                           PackageId = s.PackageId,
                                           Discount = s.Discount,
                                           Type = s.Type
                                           
                                       });
                return _tblBookings;
            }
            set { _tblBookings = value; }
        }

        private IQueryable<TblInstructor> _tblInstructors;
        public IQueryable<TblInstructor> TblInstructors
        {
            get
            {
                _tblInstructors = _tblInstructors ?? DataContext.Instructors.Select(s => new TblInstructor { Email = s.Email, FirstName = s.FirstName, LastName = s.LastName, InstructorId = s.InstructorId, Mobile = s.Mobile, Phone = s.Phone, InstructorNumber = s.InstructorNumber, CreatedDate = s.Created_Date, Gender = s.Gender, AddressId = s.AddressId ?? 0, Status = s.Status ?? false ,AreaIds = s.Areas});
                return _tblInstructors;
            }
            set { _tblInstructors = value; }
        }

        private IQueryable<InstructorLeaveModel> _tblInstructorLeaves;

        public IQueryable<InstructorLeaveModel> TblInstructorLeaves
        {
            get
            {
                _tblInstructorLeaves = _tblInstructorLeaves ??
                                       DataContext.Instructor_Leaves.Select(
                                           s =>
                                               new InstructorLeaveModel
                                               {
                                                   Id = s.Id,
                                                   InstructorId = s.InstructorId,
                                                   LeaveReason = s.Reason,
                                                   StartDate = s.StartDate,
                                                   EndDate = s.EndDate
                                               });
                return _tblInstructorLeaves;
            }
            set { _tblInstructorLeaves = value; }
        }

        private IQueryable<PackageModel> _tblPackages;
        public IQueryable<PackageModel> TblPackages
        {
            get
            {
                _tblPackages = _tblPackages ??
                               DataContext.Package_Details.Select(
                                   s =>
                                       new PackageModel
                                       {
                                           PackageId = s.PackageId,
                                           Name = s.Name,
                                           Description = s.Description,
                                           Cost = s.Cost,
                                           Hours = s.NumberOfHours
                                       });
                return _tblPackages;
            }
            set { _tblPackages = value; }
        }

        private IQueryable<AreaModel> _tblArea;
        public IQueryable<AreaModel> TblArea
        {
            get
            {
                _tblArea = _tblArea ??
                           DataContext.Areas.Select(
                               s =>
                                   new AreaModel
                                   {
                                       AreaId = s.AreaId,
                                       Name = s.Name,
                                       Description = s.Description
                                   });
                return _tblArea;
            }
            set { _tblArea = value; }
        }

        public async Task<IList<TblInstructor>> GetInstructorList()
        {
            return await TblInstructors.ToListAsync();
        }

        public Boolean CheckIsAnyAppointmentsForInsturcotrOrStudent(BookingAppointment appointment)
        {
            Boolean flag = false;
            var instructor = TblInstructors.FirstOrDefault(s => s.InstructorNumber == appointment.InstructorNumber);
            var instructorBookings =
                TblBookings.Where(
                    s =>
                        s.InstructorId  == instructor.InstructorId &&
                        (appointment.StartDate >= s.StartDate && appointment.EndDate <= s.EndDate) &&
                        ((appointment.StartTime >= s.StartTime && appointment.StartTime <= s.StopTime) || (appointment.StopTime >= s.StartTime && appointment.StopTime <= s.StopTime))).ToList();
            var studentBookings =
                TblBookings.Where(
                    s =>
                        s.StudentId == appointment.StudentId &&
                        (appointment.StartDate >= s.StartDate && appointment.EndDate <= s.EndDate) &&
                        ((appointment.StartTime >= s.StartTime && appointment.StartTime <= s.StopTime) || (appointment.StopTime >= s.StartTime && appointment.StopTime <= s.StopTime))).ToList();
            var leaves =
                TblInstructorLeaves.Where(
                    s => s.InstructorId == appointment.InstructorId &&
                         (appointment.StartDate >= s.StartDate && appointment.EndDate <= s.EndDate)
                    ).ToList();
            if (instructorBookings.Count > 0 || studentBookings.Count > 0 || leaves.Count > 0)
                flag = true;
            return flag;
        }
        public async Task<IList<InstructorBooking>> GetInstructorBookingEvents(string instructorId)
        {
            var instuctorBookings = new List<InstructorBooking>();
            var tblInstructorLeaves = TblInstructorLeaves.Where(s => s.InstructorId == instructorId);
            var tblInstructorBookings =
                TblBookings.Where(b => b.StartDate != null && b.EndDate != null && b.InstructorId == instructorId);
            foreach (var booking in tblInstructorBookings)
            {
                var student = DataContext.Students.FirstOrDefault(s => s.Id == booking.StudentId);
                if (student != null)
                {
                    if (booking.StartDate != null && booking.StartTime != null && booking.EndDate != null &&
                        booking.StopTime != null)
                    {
                        var startTime = booking.StartDate.Value.Add(booking.StartTime.Value);
                        var stopTime = booking.EndDate.Value.Add(booking.StopTime.Value);
                        instuctorBookings.Add(new InstructorBooking
                        {
                            Id = booking.BookingId,
                            Start = startTime.ToString("yyyy-MM-dd'T'HH:mm:ss"),
                            End = stopTime.ToString("yyyy-MM-dd'T'HH:mm:ss"),
                            Title = student.FirstName + " " + student.LastName,
                            ClassName = "label-success"
                        });
                    }
                }
            }
            // Fetch Instructor Leaves
            foreach (var leave in tblInstructorLeaves)
            {
                if (leave.StartDate != null && leave.EndDate != null)
                {
                    var startDate = leave.StartDate.Value;
                    var endDate = leave.EndDate.Value;
                    for (var j = 0; j < endDate.Subtract(startDate).Days + 1; j++)
                    {
                        var bookingDate = startDate.AddDays(j);
                        instuctorBookings.Add(
                            new InstructorBooking
                            {
                                Id = 0,
                                Start = new DateTime(bookingDate.Year, bookingDate.Month, bookingDate.Day,06,00,00).ToString("yyyy-MM-dd'T'HH:mm:ss"),
                                End = new DateTime(bookingDate.Year, bookingDate.Month, bookingDate.Day,20,00,00).ToString("yyyy-MM-dd'T'HH:mm:ss"),
                                Title = leave.LeaveReason,
                                ClassName = "label-yellow"
                            }
                            );
                    }
                }
            }

            return await Task.Run(() => instuctorBookings);
        }

        public async Task<IList<StudentList>> GetStudentEvents(string currentUser)
        {
            var instuctorBookings = new List<StudentList>();
            foreach (var booking in TblBookings.Where(b => b.StartDate != null && b.EndDate != null))
            {
                var student = DataContext.Students.FirstOrDefault(s => s.Id == booking.StudentId);
                if (student != null)
                {
                    if (booking.StartDate != null && booking.StartTime != null && booking.EndDate != null &&
                        booking.StopTime != null)
                    {
                        var startTime = booking.StartDate.Value.Add(booking.StartTime.Value);
                        var stopTime = booking.EndDate.Value.Add(booking.StopTime.Value);
                        instuctorBookings.Add(new StudentList
                        {
                            BookingId = booking.BookingId,
                            StartTime = startTime.ToString("HH:mm:ss"),
                            StopTime = stopTime.ToString("HH:mm:ss"),
                            StudentName = student.FirstName + " " + student.LastName,
                            StartDate = startTime.ToString("yyyy-MM-dd"),
                            EndDate = stopTime.ToString("yyyy-MM-dd"),
                        });
                    }
                }
            }
            return await Task.Run(() => instuctorBookings);
        }

        public InstructorModel GetInstructorByNumber(string instructorNumber)
        {
            var instructor = TblInstructors.FirstOrDefault(s => s.InstructorNumber == instructorNumber);
            var model = new InstructorModel();
            if (instructor != null)
            {

                model.FirstName = instructor.FirstName;
                model.LastName = instructor.LastName;
                model.InstructorNumber = instructor.InstructorNumber;
                model.Email = instructor.Email;
                model.Mobile = instructor.Mobile;
                model.Phone = instructor.Phone;
                model.Status = instructor.Status;
                model.AreaIds = instructor.AreaIds;
                model.AreaNames = instructor.AreaIds;
                if (!string.IsNullOrEmpty(instructor.Gender))
                    model.Gender = Convert.ToInt32(instructor.Gender);
                var instructorAddress = TblAddresses.FirstOrDefault(s => s.AddressId == instructor.AddressId);
                if (instructorAddress != null)
                {
                    model.AddressLine1 = instructorAddress.AddressLine1;
                    model.AddressLine2 = instructorAddress.AddressLine2;
                    model.PostalCode = instructorAddress.PostalCode.ToString();
                    model.SuburbId = instructorAddress.SuburbId;
                }

            }
            return model;
        }

        public IList<SelectListItem> GetInstructorNames()
        {
            var instructors = new List<SelectListItem> { new SelectListItem { Value = "", Text = "" } };
            foreach (var instructor in TblInstructors)
            {
                var name = instructor.FirstName + " " + instructor.LastName;
                var instructorNumber = instructor.InstructorNumber;
                instructors.Add(new SelectListItem { Value = instructorNumber , Text = name + " "+ instructorNumber });
            }
            return instructors;
        }

        public TblInstructor GetInstructorDetailsById(string instructorId)
        {
            return TblInstructors.SingleOrDefault(s => s.InstructorId == instructorId);
        }

        public BookingAppointment GetBookingAppointmentById(int bookingAppointmentId)
        {
            var booking = TblBookings.FirstOrDefault(s => s.BookingId == bookingAppointmentId);
            var bookingAppointment = new BookingAppointment { BookingId = bookingAppointmentId };
            if (booking != null)
            {
                bookingAppointment.StudentId = booking.StudentId;
                var instructor = TblInstructors.FirstOrDefault(s => s.InstructorId == booking.InstructorId);
                if (instructor != null)
                    bookingAppointment.InstructorNumber = instructor.InstructorNumber;
                if (booking.StartTime != null)
                    bookingAppointment.StartTime = booking.StartTime.Value;
                if (booking.StopTime != null)
                    bookingAppointment.StopTime = booking.StopTime.Value;
                bookingAppointment.StartDate = booking.StartDate;
                bookingAppointment.EndDate = booking.EndDate;
                bookingAppointment.PickupLocation = booking.PickupLocation;
                bookingAppointment.BookingType = booking.Type;
                
            }
            return bookingAppointment;
        }

        public Instructor GetInstructorByEmail(string email)
        {
            return DataContext.Instructors.SingleOrDefault(s => s.Email == email);
        }

        public int GetLatestInstructorId()
        {
            var instructorNumber = 1000;
            var latestInstructor = TblInstructors.OrderByDescending(o => o.CreatedDate).FirstOrDefault();
            if (latestInstructor != null)
            {
                if (!string.IsNullOrEmpty(latestInstructor.InstructorNumber))
                {
                    var sids = latestInstructor.InstructorNumber.Split('-');
                    instructorNumber = Convert.ToInt32(sids[1]);
                }
            }
            return instructorNumber;
        }

        public Instructor GetInstructorById(string instructorNumber)
        {
            return DataContext.Instructors.SingleOrDefault(s => s.InstructorNumber == instructorNumber);
        }
        public Instructor GetInstructorByName(string name)
        {
            return DataContext.Instructors.FirstOrDefault(s => (s.FirstName + " " + s.LastName) == name);
        }

        public async Task<IList<InstructorLeaveModel>> GetInstructorLeaves(string currentUser)
        {
            var listOfLeaves = TblInstructorLeaves.Where(s => s.InstructorId == currentUser).ToListAsync();
            return await Task.Run(() => listOfLeaves);
        }

        public async Task<IList<AllInstructorsLeavesModel>> GetAllInstructorsLeaves()
        {
            var allLeaves = new List<AllInstructorsLeavesModel>();
            var listOfLeaves = TblInstructorLeaves.ToList();
            foreach (var leave in listOfLeaves)
            {
                var instructor = TblInstructors.SingleOrDefault(s => s.InstructorId == leave.InstructorId);
                if (instructor != null)
                {
                    var instructorLeave = new AllInstructorsLeavesModel
                    {
                        InstructorName = instructor.FirstName+" "+instructor.LastName,
                        InstructorDetails = leave
                    };
                    allLeaves.Add(instructorLeave);
                }
            }
            
            return await Task.Run(() => allLeaves);
        }

        public async Task<IList<AllInstructorsLeavesModel>> GetInstructorLeavesByName(string instructorNumber)
        {
            var allLeaves = new List<AllInstructorsLeavesModel>();
            var instructorDetail = TblInstructors.SingleOrDefault(s => s.InstructorNumber == instructorNumber);
            var listOfLeaves = TblInstructorLeaves.Where(s => s.InstructorId == instructorDetail.InstructorId).ToList();
            foreach (var leave in listOfLeaves)
            {
                if (instructorDetail != null)
                {
                    var instructorLeave = new AllInstructorsLeavesModel
                    {
                        InstructorName = instructorDetail.FirstName+" "+instructorDetail.LastName,
                        InstructorDetails = leave
                    };
                    allLeaves.Add(instructorLeave);
                }
            }
            
            return await Task.Run(() => allLeaves);
        }

        public InstructorLeaveModel GetInstructorLeaveById(int leaveId)
        {
            return TblInstructorLeaves.SingleOrDefault(s => s.Id == leaveId);
        }

        public void ApplyInstructorLeave(string userId, InstructorLeaveModel appliedLeave)
        {
            var appliedDetails = DataContext.Instructor_Leaves.FirstOrDefault(s => s.Id == appliedLeave.Id) ?? new Instructor_Leaves();
            appliedDetails.InstructorId = userId;
            appliedDetails.Reason = appliedLeave.LeaveReason;
            appliedDetails.StartDate = appliedLeave.StartDate;
            appliedDetails.EndDate = appliedLeave.EndDate;
            appliedDetails.ModifiedDate = DateTime.Now;
            appliedDetails.ModifiedBy = userId;
            if (appliedDetails.Id == 0)
            {
                appliedDetails.CreatedDate = DateTime.Now;
                appliedDetails.CreatedBy = userId;
                DataContext.Instructor_Leaves.Add(appliedDetails);
            }
            SaveInDatabase();
        }

        public void UpdateInstructorLeave(string currentUser, InstructorLeaveModel updateLeave)
        {
            var appliedDetails = DataContext.Instructor_Leaves.FirstOrDefault(s => s.Id == updateLeave.Id) ?? new Instructor_Leaves();
            appliedDetails.InstructorId = currentUser;
            appliedDetails.Reason = updateLeave.LeaveReason;
            appliedDetails.StartDate = updateLeave.StartDate;
            appliedDetails.EndDate = updateLeave.EndDate;
            appliedDetails.ModifiedDate = DateTime.Now;
            appliedDetails.ModifiedBy = currentUser;
            if (appliedDetails.Id == 0)
            {
                appliedDetails.CreatedDate = DateTime.Now;
                appliedDetails.CreatedBy = currentUser;
                DataContext.Instructor_Leaves.Add(appliedDetails);
            }
            SaveInDatabase();
        }

        public void SaveInstructor(RegisterViewModel model)
        {

            // Save Address
            var instructorAddress = new Address
            {
                Address1 = model.AddressLine1,
                AddressLine2 = model.AddressLine2,
                Phone = model.Phone,
                Mobile = model.Mobile,
                PostCode = Convert.ToInt32(model.PostalCode),
                CreatedDate = DateTime.Now,
                CreatedBy = model.CreatedUser,
            };
            if (model.SuburbId != 0)
                instructorAddress.SuburbID = model.SuburbId;
            DataContext.Addresses.Add(instructorAddress);
            SaveInDatabase();
            //  _instructorRepo.
            // Create Instructor account
            var instructor = new Instructor
            {
                Created_Date = DateTime.Now,
                InstructorId = model.InstructorId,
                Created_By = model.CreatedUser,
                InstructorNumber = "INS-" + model.LastInstructor,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Gender.ToString(),
                Mobile = model.Mobile,
                Phone = model.Phone,
                AddressId = instructorAddress.AddressId,
                Status = model.Status,
                Areas = model.AreaIds
            };
            DataContext.Instructors.Add(instructor);
            SaveInDatabase();
        }

        public void UpdateInstructor(InstructorModel model)
        {
            var instructor = DataContext.Instructors.FirstOrDefault(s => s.InstructorNumber == model.InstructorNumber);
            if (instructor != null)
            {
                instructor.FirstName = model.FirstName;
                instructor.LastName = model.LastName;
                instructor.Gender = model.Gender.ToString();
                instructor.Mobile = model.Mobile;
                instructor.Phone = model.Phone;
                instructor.Status = model.Status;
                instructor.Modified_Date = DateTime.Now;
                instructor.Modified_By = model.CreatedUser;
                instructor.Areas = model.AreaIds;
                var instructorAddress = DataContext.Addresses.FirstOrDefault(s => s.AddressId == instructor.AddressId);
                if (instructorAddress != null)
                {
                    instructorAddress.Address1 = model.AddressLine1;
                    instructorAddress.AddressLine2 = model.AddressLine2;
                    instructorAddress.Phone = model.Phone;
                    instructorAddress.Mobile = model.Mobile;
                    instructorAddress.PostCode = Convert.ToInt32(model.PostalCode);
                    instructorAddress.ModifiedDate = DateTime.Now;
                    instructorAddress.SuburbID = model.SuburbId;
                    instructorAddress.ModifiedBy = model.CreatedUser;
                }
                SaveInDatabase();
            }
        }

        #region Packages

        public async Task<IList<PackageModel>> GetPackages()
        {
            return await TblPackages.ToListAsync();
        }

        public PackageModel GetPackagesById(int packageId)
        {
            return TblPackages.SingleOrDefault(s => s.PackageId == packageId);
        }

        public void CreateNewPackage(string userId, PackageModel package)
        {
            var packageDetails = new Package_Details
            {
                ModifiedBy = userId,
                ModifiedDate = DateTime.Now,
                Name = package.Name,
                Description = package.Description,
                NumberOfHours = package.Hours,
                Cost = package.Cost,
                Type = string.Empty,
            };

            if (packageDetails.PackageId == 0)
            {
                packageDetails.CreatedDate = DateTime.Now;
                packageDetails.CreatedBy = userId;
                DataContext.Package_Details.Add(packageDetails);
            }

            SaveInDatabase();
        }

        public void UpdatePackageDetails(string userId, PackageModel package)
        {
            var packageDetails = DataContext.Package_Details.FirstOrDefault(s => s.PackageId == package.PackageId) ??
                                 new Package_Details();
            packageDetails.ModifiedBy = userId;
            packageDetails.ModifiedDate = DateTime.Now;
            packageDetails.Name = package.Name;
            packageDetails.Description = package.Description;
            packageDetails.NumberOfHours = package.Hours;
            packageDetails.Cost = package.Cost;
            packageDetails.Type = string.Empty;


            if (packageDetails.PackageId == 0)
            {
                packageDetails.CreatedDate = DateTime.Now;
                packageDetails.CreatedBy = userId;
                DataContext.Package_Details.Add(packageDetails);
            }

            SaveInDatabase();
        }

        #endregion

        #region Area

        public async Task<IList<AreaModel>> GetArea()
        {
            return await TblArea.ToListAsync();
        }
        public IList<SelectListItem> GetAreasList(string areaIds)
        {
            var areas = new List<SelectListItem> { new SelectListItem { Value = "", Text = "" } };
            
            foreach (var area in TblArea)
            {
                areas.Add(new SelectListItem { Value = area.AreaId.ToString(), Text = area.Name  });
            }
            if (!string.IsNullOrEmpty(areaIds))
            {
                string[] areaNames = areaIds.Split(',');
                foreach (var areaname in areaNames)
                {
                    var option = areas.FirstOrDefault(s => s.Value == areaname);
                    if (option != null)
                        option.Selected = true;
                }
            }
            return areas;
        }

        public AreaModel GetAreaById(int areaId)
        {
            return TblArea.SingleOrDefault(s => s.AreaId == areaId);
        }

        public void CreateNewArea(string userId, AreaModel area)
        {
            var areaDetails = DataContext.Areas.FirstOrDefault(s => s.AreaId == area.AreaId) ??
                                 new Area();
            areaDetails.Name = area.Name;
            areaDetails.Description = area.Description;
            if (areaDetails.AreaId == 0)
            {
                areaDetails.CreatedDate = DateTime.Now;
                areaDetails.CreatedBy = userId;
                DataContext.Areas.Add(areaDetails);
            }

            SaveInDatabase();
        }

        public void UpdateArea(string userId, AreaModel area)
        {
            var areaDetails = DataContext.Areas.FirstOrDefault(s => s.AreaId == area.AreaId) ??
                                 new Area();
            areaDetails.Name = area.Name;
            areaDetails.Description = area.Description;
            if (areaDetails.AreaId == 0)
            {
                areaDetails.CreatedDate = DateTime.Now;
                areaDetails.CreatedBy = userId;
                DataContext.Areas.Add(areaDetails);
            }

            SaveInDatabase();
        }

        #endregion

        #region Last 7Days Instructor Hours

        public List<Last7DaysInstructorHours> GetLast7DaysInstructorHour()
        {
            var last7DaysInstructorHours = new List<Last7DaysInstructorHours>();
            var hours = new TimeSpan();
            var last7ThDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-6.0);
            foreach (var instructor in TblInstructors.ToList())
            {
                var instructorDetail = instructor;
                var bookingDetailsList = TblBookings.Where(s => s.InstructorId == instructorDetail.InstructorId && s.StartTime != null && s.StopTime != null && s.StartDate >= last7ThDay).ToList();
                hours = (from bookingHour in bookingDetailsList where bookingHour.StartTime != null && bookingHour.StopTime != null select (bookingHour.StopTime.Value - bookingHour.StartTime.Value)).Aggregate(hours, (current, hour) => current + hour);
                last7DaysInstructorHours.Add(new Last7DaysInstructorHours
                {
                    InstructorName = instructor.FirstName + " " + instructor.LastName,
                    Hours = hours
                });
                hours = new TimeSpan();
            }

            return last7DaysInstructorHours;
        }

        #endregion

        public void SaveInDatabase()
        {
            DataContext.SaveChanges();
        }
    }
}