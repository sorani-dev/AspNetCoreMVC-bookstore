using BookStoreMvc.Models;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreMvc.Services
{
    public class EmailService : IEmailService
    {
        private const string templatePath = @"EmailTemplate/{0}.html";
        private readonly SMTPConfigModel smtpConfig;

        public EmailService(IOptions<SMTPConfigModel> options)
        {
            this.smtpConfig = options.Value;
        }

        public async Task SendTestEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = "This is a test email subject for the store web app ";
            userEmailOptions.Body = GetEmailBody("TestEmail");

            await SendEmail(userEmailOptions);
        }

        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            MailMessage mail = new MailMessage()
            {
                Body = userEmailOptions.Body,
                Subject = userEmailOptions.Subject,
                From = new MailAddress(smtpConfig.SenderAddress, smtpConfig.SenderDisplayName),
                IsBodyHtml = smtpConfig.IsBodyHTML,
            };

            foreach (var toEmail in userEmailOptions.ToEmails)
            {
                mail.To.Add(toEmail);
            }

            NetworkCredential networkCredential = new NetworkCredential(smtpConfig.UserName, smtpConfig.Password);

            SmtpClient smtpClient = new SmtpClient
            {
                Host = smtpConfig.Host,
                Port = smtpConfig.Port,
                EnableSsl = smtpConfig.EnableSSL,
                UseDefaultCredentials = smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential
            };

            mail.BodyEncoding = Encoding.Default;


            await smtpClient.SendMailAsync(mail);

        }

        public string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(String.Format(templatePath, templateName));
            return body;
        }
    }
}
