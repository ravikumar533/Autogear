using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;

namespace AutogearWeb.Providers
{
    public class SMSAPI
    {
        public static void SendSMStoStudent(string studentName, string fromdate, string todate, string pickuplocation, string instructorName, string instructorMobileNumber, string studentMobileNumber)
        {
            string studentMessage = string.Format("Lesson Confirmation - from {0} to {1}. Instructor Name: {2}. Instructor Number : {3}", fromdate, todate, instructorName, instructorMobileNumber);
            string instructorMessage = string.Format("Lesson Confirmation - from {0} to {1}. Student Name: {2}. Student Number : {3}", fromdate, todate, studentName, studentMobileNumber);
            List<string> studentnumbers = new List<string>();
            studentnumbers.Add(studentMobileNumber);
            List<string> instructornumbers = new List<string>();
            instructornumbers.Add(instructorMobileNumber);

            SendSMS(studentMessage, studentnumbers);
            SendSMS(instructorMessage, instructornumbers);

        }
        public static string SendSMS(string message, List<string> phonenumbers)
        {
            try
            {
                string numbersList = GetPhoneNumbers(phonenumbers);
                WebClient client = new WebClient();
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                client.QueryString.Add("username", "testingapp");
                client.QueryString.Add("password", "Nagendra~1");
                client.QueryString.Add("to", numbersList);
                client.QueryString.Add("from", "Autogear");
                client.QueryString.Add("message", message);
                client.QueryString.Add("ref", "123");
                client.QueryString.Add("maxsplit", "2");
                string baseurl = "http://www.smsbroadcast.com.au/api-adv.php";
                Stream data = client.OpenRead(baseurl);
                StreamReader reader = new StreamReader(data);
                string s = reader.ReadToEnd();
                string[] response = s.Split(':');
                data.Close();
                reader.Close();
                if (response.Length > 0)
                    return response[0];
                else
                    return "ERROR";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static string GetPhoneNumbers(List<string> phonenumbers)
        {
            string phnNumbers = string.Empty;
            foreach (string phonenumber in phonenumbers)
            {
                phnNumbers += "," + phonenumber;
            }
            phnNumbers = phnNumbers.Remove(0, 1);
            return phnNumbers;
        }
    }
}