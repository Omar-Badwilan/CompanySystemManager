using System.Net;
using System.Net.Mail;

namespace CompanySystem.Presentation.Utilites
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com", 587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("omar0000680@gmail.com", "password");
            Client.Send("omar0000680@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
