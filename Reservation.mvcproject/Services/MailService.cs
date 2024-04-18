using Reservation.mvcproject.Interfaceses;
using System.Net.Http;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Reservation.mvcproject.Services
{
    public class MailService : IMailService
    {
        public async Task SendEmailAsync(string? toMail, string subject, string body)
        {
            string selfmail = "yesimceylan73@gmail.com";

            toMail ??= "mustafa.bati9@gmail.com";

            MailMessage newmail = new();
            SmtpClient smptClient = new();

            newmail.To.Add(toMail);
            newmail.Subject = subject;
            newmail.Body = body;
            newmail.From = new(selfmail, "Test", Encoding.UTF8);

            smptClient.Credentials = new NetworkCredential(selfmail, "pjgd moin bqnl bvkb");
            smptClient.EnableSsl = true;
            smptClient.Host = "smtp.gmail.com";
            smptClient.Port = 587;

            await smptClient.SendMailAsync(newmail);
        }
    }
}
