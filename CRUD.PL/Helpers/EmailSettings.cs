using CRUD.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace CRUD.PL.Helpers
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl=true;
            client.Credentials = new NetworkCredential("alaamamdouhalsayd1010@gmail.com", "izsesrmmfkezukar");
            client.Send("alaamamdouhalsayd1010@gmail.com", email.To, email.Subject, email.Body);

             

        }
    }
}
