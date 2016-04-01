using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Net.Mail;

namespace AutogearWeb.Providers
{
    public class EmailAPI
    {        
        private const string emailSubject = "Driving Lesson Booking";
        public static string SendEmailToStudent(string studentName,string fromdate, string todate, string pickuplocation, string instructorName, string mobileNumber,string instructorEmail, string studentEmail)
        {          
            string message = string.Format("Dear {0}, \n Your driving lesson is booked on {1} to {2}. Your pickup address is {3} \n Your  instructor is {4} \n His mobile number is {5} \n He will contact you before the lesson to confirm booking. \n Thanks, \n Autogear driving school",studentName, fromdate, todate, pickuplocation, instructorName, mobileNumber);
            List<string> emails = new List<string>();
            emails.Add(instructorEmail);
            emails.Add(studentEmail);
            SendEmail(emails, emailSubject, message);
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