using System.Net.Mail;
using System.Text;

namespace ActivePCsWatchdog
{
    public static class MailUtils
    {
        public static (bool, string) SendEmail(string subject, string body, string sender, string[] receivers, string smtp_ip, int smtp_port)
        {
            if (receivers.Length == 0) {
                return (false, "No receivers specified.");
            }

            SmtpClient smtp_client = new SmtpClient(smtp_ip) {
                Port = smtp_port,
                UseDefaultCredentials = false,
                EnableSsl = false,
            };
            MailMessage mailMessage = new MailMessage {
                Subject = subject,
                Body = body,
                IsBodyHtml = false,
                From = new MailAddress(sender, sender, Encoding.UTF8)
            };
            foreach (string receiver in receivers) {
                mailMessage.To.Add(receiver);
            }

            bool sucess = false;
            try {
                smtp_client.Send(mailMessage);
                sucess = true;
            } catch { }

            return (sucess, "Wrong SMTP settings.");
        }

        public static string[] ParseMailsFromConfig(string emails) => emails.Split(';').Select(s => s.Trim()).ToArray();
    }
}
