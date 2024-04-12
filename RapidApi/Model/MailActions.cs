using System.Net.Mail;
using System.Net;

namespace RapidApi.Model
{
    public class MailActions:IMailSender
    {

        public void SendMail(string mailAdress, string Key)
        {

            // SMTP sunucu bilgileri
            string smtpServer = "outlook.office365.com";
            int smtpPort = 587; // Genellikle 587 veya 465 kullanılır
            string smtpUsername = "";
            string smtpPassword = "";

            // gönderen ve alıcı adres
            string senderAddress = "";
            string recipientAddress = mailAdress;

            // Mail mesajı oluşturma
            MailMessage mail = new MailMessage(senderAddress, recipientAddress);
            mail.Subject = "Api Key Generated";
            mail.Body = $"Your Api Key Has Generated. Here: {Key}";

            // SMTP istemcisini oluşturma 
            SmtpClient client = new SmtpClient(smtpServer, smtpPort);
            client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            client.EnableSsl = true; // SSL/TLS kullanıyorsa true olarak ayarla

            client.Send(mail);
        }
    }
}
