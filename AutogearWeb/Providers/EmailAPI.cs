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
        public string SendEmail(List<string> sendEmail,string subject,string body)
        {
            try
            {
                string emailIds = GetEmailAddress(sendEmail);
                var fromAddress = new MailAddress("autogeartesting159@gmail.com", "AdminAutogear");
                var toAddress = new MailAddress(emailIds, "To Name");
                const string fromPassword = "Passw0rd12";
                
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var mailMessage = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(mailMessage);
                }
                return "0";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        private string GetEmailAddress(List<string> emails)
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