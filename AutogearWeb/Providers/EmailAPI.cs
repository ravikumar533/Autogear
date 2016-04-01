using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Net.Mail;
using System.Threading;

namespace AutogearWeb.Providers
{
    public class EmailAPI
    {        
        private const string emailSubject = "Driving Lesson Booking";
        public static string SendEmailToStudent(string studentName,string fromdate, string todate, string pickuplocation, string instructorName, string mobileNumber,string instructorEmail, string studentEmail)
        {
            string message = string.Format("<b>Dear {0},</b><br><br>Your Driving lesson is booked form <b>{1}</b> to <b>{2}</b>.<br>Instructor Name : <b>{4}</b><br>Instructor Mobile : <b>{5}</b><br>Pickup Location : <b>{3}</b><br> Instructor will contact you before the lesson to confirm booking.<br><br>Thank you for choosing Autogear,<h4>Autogear Driving School <br>Phone: (02) 9868 6242 <br>Mobile: 0432 835 525 <br>Email: info@autogeardrivingschool.com.au<br>www.autogeardrivingschool.com.au</h4>",studentName, fromdate, todate, pickuplocation, instructorName, mobileNumber);            
            List<string> emails = new List<string>();            
            emails.Add(studentEmail);
            Thread studentEmailThread = new Thread(() => SendEmail(emails, emailSubject, message));
            studentEmailThread.Start();

            string instructorMessage = string.Format("<b>Dear {0},</b><br><br>A new lesson is booked form <b>{1}</b> to <b>{2}</b>.<br>Student Name : <b>{3}</b><br>Student Mobile : <b>{4}</b><br>Pickup Location : <b>{5}</b><br> Pls confirm booking before the lesson.<br><br><h4>Roy Nanayakkara,<br>Autogear Driving School <br>Phone: (02) 9868 6242 <br>Mobile: 0432 835 525 <br>Email: info@autogeardrivingschool.com.au<br>www.autogeardrivingschool.com.au</h4>", instructorName, fromdate, todate, studentName, mobileNumber, pickuplocation);
            List<string> instructorEmails = new List<string>();
            instructorEmails.Add("nagendrareddy.d@gmail.com");
            Thread instructorEmailThread = new Thread(() => SendEmail(instructorEmails, emailSubject, instructorMessage));
            instructorEmailThread.Start();      
            return string.Empty;
        }

        public static string SendEmail(List<string> sendEmail,string subject,string body)
        {
            try
            {
                var fromAddress = new MailAddress("autogearsydneydrivingschool@gmail.com", "AdminAutogear");
                const string fromPassword = "Adminroy@123";
                
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)                    
                };
                using (var mailMessage = new MailMessage()
                {             
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = body
                })
                {                    
                    mailMessage.From = fromAddress;                    
                    foreach(string email in sendEmail)
                    {
                        mailMessage.To.Add(email);
                    }
                    smtp.Send(mailMessage);
                }
                return "0";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        private static string GetEmailAddress(List<string> emails)
        {
            string emailIds = string.Empty;
            foreach (string email in emails)
            {
                emailIds += ";" + email;
            }
            emailIds = emailIds.Remove(0, 1);
            return emailIds;
        }
    }
}