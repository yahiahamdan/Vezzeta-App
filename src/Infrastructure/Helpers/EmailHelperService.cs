using Application.Interfaces.Helpers;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace Infrastructure.Helpers
{
    public class EmailHelperService : IEmailHelperService
    {
        private readonly IConfiguration config;

        public EmailHelperService(IConfiguration config)
        {
            this.config = config;
        }

        public void SendEmail(string to, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(this.config["MailSettings:RealMail"]));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Text) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect(this.config["MailSettings:Host"], 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(
                this.config["MailSettings:RealMail"],
                this.config["MailSettings:Password"]
            );
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
