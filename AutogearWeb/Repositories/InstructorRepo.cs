using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
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

        public  InstructorRepo()
        {
            DataContext = new AutogearDBEntities();
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
                _tblInstructors = _tblInstructors ?? DataContext.Instructors.Select(s => new TblInstructor {Email = s.Email , FirstName = s.FirstName,LastName = s.LastName, InstructorId = s.InstructorId, Mobile = s.Mobile.ToString(), Phone = s.Phone.ToString()});
                return _tblInstructors;
            }
            set { _tblInstructors = value; }
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
            return await Task.Run(() => instuctorBookings);
        }
        public async Task<IList<string>> GetInstructorNames()
        {
            return await TblInstructors.Select(s => s.FirstName + " " + s.LastName).ToListAsync();
        }

        public BookingAppointment GetBookingAppointmentById(int bookingAppointmentId)
        {
            var booking = TblBookings.FirstOrDefault(s => s.BookingId == bookingAppointmentId);
            var bookingAppointment = new BookingAppointment();
            bookingAppointment.BookingId = bookingAppointmentId;
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
        public void AddIntructor(Instructor repo)
        {
            DataContext.Instructors.Add(repo);
        }

        public Instructor GetInstructorByEmail(string email)
        {
            return DataContext.Instructors.SingleOrDefault(s => s.Email == email);
        }

        public Instructor GetInstructorByName(string name)
        {
            return DataContext.Instructors.FirstOrDefault(s => (s.FirstName + " " + s.LastName) == name);
        }
        public void SaveInDatabase()
        {
            DataContext.SaveChangesAsync();
        }
    }
}