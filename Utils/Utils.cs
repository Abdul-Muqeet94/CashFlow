using System;
using System.Net.Mail;
using System.Text;

namespace CashFlow.Utils{
    public class Utils{
     public static bool sendEmail(string subject, string body, string email) {

            try {
                var myMessage = new SendGrid.SendGridMessage();
                myMessage.AddTo(email);
                myMessage.From = new MailAddress("abdulmuqeet@ngenc.com", "CashFlow");
                myMessage.Subject = subject;
                myMessage.Text = body;

                var transportWeb = new SendGrid.Web(Constant.SENDGRID_API_TOKEN);
                transportWeb.DeliverAsync(myMessage).Wait();

            } catch {
                return false;
            }

            return true;

        }
        // public static async Task sendEmail(string subject, string body, string email)
		// {
        //     return null;
        //     // try {


        //     //     string apiKey = Constant.SENDGRID_API_TOKEN; 
        //     // 	dynamic sg = new SendGridAPIClient(apiKey); 
        //     // 	Email from = new Email("no-reply@lazyjar.com", "Lazy Jar");
        //     // 	Email to = new Email(email); 
        //     // 	Content content = new Content("text/plain", subject);
        //     // 	Mail mail = new Mail(from, subject, to, content);
        //     // 	Personalization personalization = new Personalization(); 
        //     // 	mail.AddHeader("Authorization", "Bearer " + Constant.SENDGRID_API_TOKEN); 
        //     // 	dynamic response = await sg.client.mail.send.post(requestBody: mail.Get());
        //     // 	return true; 
        //     // } catch (Exception ex) { 
        //     // 	return false; 
        //     // 	} 
        // }
        // public static bool sendEmail(string subject, string body, string email)
        // {
        //     try
        //     {

        //         var message = new MimeMessage();

        //         message.From.Add(
        //             new MailboxAddress(
        //                 Constant.MAIL_FROM_ADDRESS_NAME,
        //                 Constant.MAIL_FROM_ADDRESS_EMAIL)
        //         );

        //         message.To.Add(new MailboxAddress("", email));
        //         message.Subject = subject;
        //         message.Body = new TextPart("plain")
        //         {
        //             Text = body
        //         };

        //         using (var client = new SmtpClient())
        //         {
        //             client.Connect(Constant.MAIL_HOST_ADDRESS, Constant.MAIL_HOST_PORT, false);


        //             client.AuthenticateAsync(
        //                 Constant.MAIL_AUTHENTICAITON_USER,
        //                 Constant.MAIL_AUTHENTICATION_PASSWORD
        //             ).Wait();


        //             client.Send(message);
        //             client.Disconnect(true);
        //         }

        //         return true;


        //     }
        //     catch (Exception)
        //     {
        //         return false;
        //     }
        // }

        public static DateTime getDateFromString(string date)
        {
            return DateTime.ParseExact(date, "yyyy-MM-dd",
                                           System.Globalization.CultureInfo.InvariantCulture);
        }

        public static double convertKMtoMile(double km)
        {
            if (km == 0)
                return 0;
            else
                return km / 1.60934;
        }

        // public static string getConnectionString()
        // {
        // 	if (DBUrl == null)
        // 	{
        // 		DBUrl = Configuration.ConnectionStrings["DB"].ConnectionString;
        // 	}
        // 	return DBUrl;
        // }


        public static String calculateTimeDifference(String timestamp)
        {
            DateTime datetime;
            datetime = Convert.ToDateTime(timestamp);
            String date;

            TimeSpan span = DateTime.Now.Subtract(datetime);
            if (span.Days == 0)
            {
                if (span.Hours == 0)
                {
                    date = span.Minutes.ToString() + "m ago";
                }
                else
                {
                    date = span.Hours.ToString() + "hr ago";
                }
            }
            else if (span.Days >= 365)
            {
                date = "Year ago";
            }

            else if (span.Days == 1)
            {
                date = "Yesterday";
            }
            else if (span.Days <= 6 && span.Days > 0)
            {
                date = datetime.DayOfWeek.ToString();
            }
            else
            {
                // Same as datetime.ToShortDateString();
                date = datetime.ToString("d");
            }

            return date;

        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}