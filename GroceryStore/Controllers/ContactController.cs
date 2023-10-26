using SMG_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Email()
        {
            return View();
        }

        public ActionResult SendMail(EmailModel email_data)
        {
            string psw = "I Am Groot07*";
            MailMessage mail = new MailMessage();
            mail.To.Add(email_data.To);
            mail.From = new MailAddress(email_data.From, "Sample For testing Send Mail", System.Text.Encoding.UTF8);
            mail.Subject = "This mail is send from asp.net application";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = "<html><body>This Email is just Testing Purpus Only don't reply Thanks!</body></html>";
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = true;
            client.Credentials = new System.Net.NetworkCredential(email_data.From, psw);
            client.Port = 587;
            client.Host = "smtp.live.com";
            client.EnableSsl = true;
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                
            }
            return null;
        }
    }
}