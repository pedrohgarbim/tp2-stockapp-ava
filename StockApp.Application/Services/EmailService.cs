using StockApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string to,  string subject, string body)
        {
            using (var smtpClient = new SmtpClient("smtp.example.com"))
            {
                var mailMessage = new MailMessage()
                {
                    From = new MailAddress("no-reply@example.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(to);

                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}
