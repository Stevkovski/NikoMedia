using DataAccess.Entities;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EmailServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);

    }
}
