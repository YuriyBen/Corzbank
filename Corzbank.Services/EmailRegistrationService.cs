using Corzbank.Data.Entities.Models;
using Corzbank.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Services
{
    public class EmailRegistrationService : IEmailRegistrationService
    {
        private readonly EmailSettingsModel _emailSettings;

        public EmailRegistrationService(EmailSettingsModel emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public void SendEmail(string email, string subject, string message)
        {
            MimeMessage emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_emailSettings.Name, _emailSettings.FromAddress));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            var client = new SmtpClient();
            {
                client.Connect(_emailSettings.Server, _emailSettings.Port, true);
                client.Authenticate(_emailSettings.FromAddress, _emailSettings.Password);
                client.Send(emailMessage);

                client.Disconnect(true);
            }
        }
    }
}
