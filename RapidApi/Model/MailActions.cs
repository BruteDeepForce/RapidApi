using System.Net.Mail;
using System.Net;

namespace RapidApi.Model
{
    public class MailActions
    {

        public void SendMail(string mailAdress, string Key)
        {

            // SMTP sunucu bilgileri
            string smtpServer = "outlook.office365.com";
            int smtpPort = 587; // Genellikle 587 veya 465 kullanılır
            string smtpUsername = "s.tuysuzoglu@outlook.com";
            string smtpPassword = "SlSl123456";

            // Mail gönderen ve alıcı adresi
            string senderAddress = "s.tuysuzoglu@outlook.com";
            string recipientAddress = mailAdress;

            // Mail mesajını oluştur
            MailMessage mail = new MailMessage(senderAddress, recipientAddress);
            mail.Subject = "Api Key Generated";
            mail.Body = $"Your Api Key Has Generated. Here: {Key}";

            // SMTP istemcisini oluştur
            SmtpClient client = new SmtpClient(smtpServer, smtpPort);
            client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            client.EnableSsl = true; // SSL/TLS kullanıyorsa true olarak ayarla

            // Mail'i gönder
            client.Send(mail);
        }
    }
}
