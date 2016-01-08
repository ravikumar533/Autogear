using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        public  InstructorRepo()
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
                                           EndDate = s.EndDate
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
                _tblInstructors = _tblInstructors ?? DataContext.Instructors.Select(s => new TblInstructor { Email = s.Email, FirstName = s.FirstName, LastName = s.LastName, InstructorId = s.InstructorId, Mobile = s.Mobile, Phone = s.Phone, InstructorNumber = s.InstructorNumber, CreatedDate = s.Created_Date,Gender = s.Gender, AddressId = s.AddressId ?? 0});
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
                                                   Id=s.Id,
                                                   InstructorId = s.InstructorId,
                                                   LeaveReason = s.Reason,
                                                   StartDate = s.StartDate,
                                                   EndDate = s.EndDate
                                               });
                return _tblInstructorLeaves;
            }
            set { _tblInstructorLeaves = value; }
        }

        public async Task<IList<TblInstructor>> GetInstructorList()
        {
            return await TblInstructors.ToListAsync();
        }
        
        public async Task<IList<InstructorBooking>> GetInstructorBookingEvents(string instructorId)
        {
            var instuctorBookings = new List<InstructorBooking>();
            foreach (var booking in TblBookings.Where(b=> b.StartDate != null && b.EndDate != null && b.InstructorId == instructorId))
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
                            Title = student.FirstName + " " + student.LastName
                        });
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

        public async Task<IList<string>> GetInstructorNames()
        {
            return await TblInstructors.Select(s => s.FirstName + " " + s.LastName).ToListAsync();
        }

        public BookingAppointment GetBookingAppointmentById(int bookingAppointmentId)
        {
            var booking = TblBookings.FirstOrDefault(s => s.BookingId == bookingAppointmentId);
            var bookingAppointment = new BookingAppointment {BookingId = bookingAppointmentId};
            if (booking != null)
            {
                var student = DataContext.Students.FirstOrDefault(s=>s.Id == booking.StudentId);
                if (student != null)
                {
                    bookingAppointment.StudentName = student.FirstName + " " + student.LastName;
                }
                booking.StartTime = bookingAppointment.StartTime;
                booking.StopTime = bookingAppointment.StopTime;
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

        public Instructor GetInstructorByName(string name)
        {
            return DataContext.Instructors.FirstOrDefault(s => (s.FirstName + " " + s.LastName) == name);
        }

        public async Task<IList<InstructorLeaveModel>> GetInstructorLeaves(string currentUser)
        {
            var instructor = DataContext.Instructors.SingleOrDefault(s => s.InstructorId == currentUser);
            var listOfLeaves = TblInstructorLeaves.Where(s => s.InstructorId == instructor.InstructorId).ToListAsync();
            return await Task.Run(() => listOfLeaves);
        }

        public void ApplyInstructorLeave(string userId, InstructorLeaveModel appliedLeave)
        {
            var appliedDetails = DataContext.Instructor_Leaves.FirstOrDefault(s => s.Id == appliedLeave.Id) ?? new Instructor_Leaves();
            appliedDetails.InstructorId = userId;
            appliedDetails.Reason = appliedLeave.LeaveReason;
            appliedDetails.StartDate = appliedLeave.StartDate;
            appliedDetails.EndDate = appliedLeave.EndDate;
            if (appliedDetails.Id == 0)
            {
                DataContext.Instructor_Leaves.Add(appliedDetails);
            }
            DataContext.SaveChanges();
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
                CreatedBy = model.CreatedUser
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
                Mobile =model.Mobile,
                Phone = model.Phone,
                AddressId =instructorAddress.AddressId
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
                instructor.Modified_Date = DateTime.Now;
                instructor.Modified_By = model.CreatedUser;
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
        public void SaveInDatabase()
        {
            DataContext.SaveChanges();
        }
    }
}