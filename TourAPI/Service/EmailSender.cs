using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using TourAPI.Interfaces;

namespace TourAPI.Service
{
    public class EmailSender : IEmailSender
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;

        public EmailSender(string smtpHost, int smtpPort, string smtpUser, string smtpPass)
        {
            _smtpHost = smtpHost;
            _smtpPort = smtpPort;
            _smtpUser = smtpUser;
            _smtpPass = smtpPass;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Console.WriteLine($"Sending to: '{email}'");
            Console.WriteLine($"SMTP Host: {_smtpHost}, User: {_smtpUser}");

            try
            {
                // Kiểm tra và chuẩn hóa email
                if (string.IsNullOrWhiteSpace(_smtpUser) || string.IsNullOrWhiteSpace(_smtpHost))
                {
                    throw new ArgumentException("SMTP configuration values cannot be null or empty.");
                }

                var emailAddress = new MailAddress(email); // Kiểm tra định dạng email

                using (var client = new SmtpClient(_smtpHost, _smtpPort))
                {
                    client.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
                    client.EnableSsl = true; // Bật chế độ SSL

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_smtpUser), // Đảm bảo _smtpUser là email hợp lệ
                        Subject = subject,
                        Body = htmlMessage,
                        IsBodyHtml = true,
                    };

                    mailMessage.To.Add(emailAddress); // Sử dụng email đã được xác thực

                    await client.SendMailAsync(mailMessage);
                    Console.WriteLine("Email sent successfully.");
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Invalid email format: " + ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("Null value error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }

    }

}
