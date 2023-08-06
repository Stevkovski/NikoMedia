using Azure.Identity;
using DataAccess.Entities;
using SendGrid;
using SendGrid.Helpers.Mail;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Services.EmailServices
{
    public class EmailService : IEmailService
    {

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("nik.stevkovski@gmail.com", "mjynpzydmhaxalmv"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("nik.stevkovski@gmail.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false,  // Set to true if the body contains HTML
                };

                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);

                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
        }
    }
}
