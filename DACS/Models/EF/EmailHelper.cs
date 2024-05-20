using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace DACS.Models
{
    public class EmailHelper
    {
        public bool SendEmailPasswordReset(string userEmail, string link)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("doantogiabao@gmail.com");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Cảm ơn bạn đã đóng góp";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = link;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("doantogiabao@gmail.com", "uurh uckd egxf jtgf");
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }
    }
}
