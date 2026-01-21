using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Text;

namespace BLL.Services
{
    public class EmailService
    {
        SmtpSettings settings;

        public EmailService(IConfiguration config)
        {
            settings = new SmtpSettings();
            config.GetSection("Smtp").Bind(settings);
        }

        public bool SendPurchaseEmail(string toEmail, string toName, string noteName, decimal amount, decimal remainingBalance)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(settings.FromName, settings.FromAddress));
                message.To.Add(new MailboxAddress(toName ?? toEmail, toEmail));
                message.Subject = $"Purchase confirmation — {noteName}";

                var bodyText =
$@"Hi {toName},

You have successfully purchased '{noteName}'.
Total amount: {amount:C}
Your remaining balance: {remainingBalance:C}

Thank you for using NoteKori.";

                message.Body = new TextPart("plain") { Text = bodyText };

                using var client = new MailKit.Net.Smtp.SmtpClient();
                client.Connect(settings.Host, settings.Port, settings.Secure ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTlsWhenAvailable);

                if (!string.IsNullOrWhiteSpace(settings.User))
                {
                    client.Authenticate(settings.User, settings.Pass);
                }

                client.Send(message);
                client.Disconnect(true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private class SmtpSettings
        {
            public string Host { get; set; }
            public int Port { get; set; } = 587;
            public bool Secure { get; set; } = false;
            public string User { get; set; }
            public string Pass { get; set; }
            public string FromName { get; set; }
            public string FromAddress { get; set; }
        }
    }
}
