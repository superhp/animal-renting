using AnimalRental.Models;
using System;
using System.Net.Mail;

namespace AnimalRental.Helper
{
    /// <summary>
    /// A static class for sending emails
    /// </summary>
    public static class Email
    {
        /// <summary>
        /// A static method to send the email about the model status change
        /// </summary>
        /// <param name="model"></param>
        public static void SendEmail(Animal model)
        {
            var message = new MailMessage();
            message.To.Add("saurimas@hotmail.com");
            message.Subject = "Animal named " + model.Name + " has been " + (model.Status==0 ? "returned" : "rented");
            message.From = new MailAddress("noreplay@rentanimal.end");
            message.Body = "Animal named " + model.Name + " has been " + (model.Status == 0 ? "returned" : "rented") + " at " + DateTime.Now;

            try
            {
                var smtp = new SmtpClient("localhost");
                smtp.Send(message);
            }
            catch 
            {
                //if smtp4dev is closed
            }
        }
    }
}