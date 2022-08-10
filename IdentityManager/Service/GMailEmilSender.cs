
using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Mail;

namespace IdentityManager.Service
{
    public class GMailEmilSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        public GMailOptions _gmailOptions;

        public GMailEmilSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                _gmailOptions = _configuration.GetSection("Gmail").Get<GMailOptions>();
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(_gmailOptions.email, _gmailOptions.password),
                    EnableSsl = true,
                };
                //MailMessage.Attachments.Add(new Attachment(model.Attachment.OpenReadStream(), fileName));
                client.Send(_gmailOptions.email, email, subject, htmlMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
