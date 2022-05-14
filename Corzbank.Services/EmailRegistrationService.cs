using Corzbank.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Corzbank.Services
{
    public class EmailRegistrationService: IEmailRegistrationService
    {
        public void SendEmail(string email, string subject, string message)
        {
            MimeMessage emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Corzbank", "corzbank@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

             var client = new SmtpClient();
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("corzbank@gmail.com", "corzbank123");
                client.Send(emailMessage);

                client.Disconnect(true);
            }
        }
    }
}
