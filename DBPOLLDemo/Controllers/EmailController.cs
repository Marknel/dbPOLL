using System;
using System.Net.Mail;
using System.Web.Mvc;

namespace DBPOLLDemo.Controllers
{
    public class EmailController : Controller
    {
        private string password;
        private string username;
        private string destAddress;
        private string body;


        /// <summary>
        /// Constructor for EmailController Object.
        /// </summary>
        /// <param name="username">Username of user </param>
        /// <param name="password">Password of user</param>
        /// <param name="destAddress">Email address of user</param>
        public EmailController(string username, string password, string destAddress)
        {
            this.username = username;
            this.password = password;
            this.destAddress = destAddress;
            buildBody();
        }


        private void buildBody()
        {
            string Body = @"Here are your new login details<br /><br />";

            Body += "<strong>Username: </strong>" +
                    @"&nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; "
                        + this.username + "<br />";
            Body += "<strong>password: </strong>" +
                    @"&nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;
                                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;"
                    + this.password + "<br />";
            this.body = Body;
        }


        public string send()
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("groupg3004@gmail.com");
                mail.IsBodyHtml = true;
                mail.To.Add(this.destAddress);
                mail.Subject = "Your new account details";
                mail.Body = this.body;


                //smtp.Host = "mailhub.itee.uq.edu.au";
                //smtp.Port = 25;    

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("groupg3004@gmail.com", "csse3004");
                smtp.EnableSsl = true;

                smtp.Send(mail);
            }
            catch (Exception e)
            {
                return "An error occured while sending the email: " + e.Message;
            }
            return "Email sent successfully";
        }

           
        


    }
}
