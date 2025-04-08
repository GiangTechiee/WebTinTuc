using System.Net.Mail;
using System.Net;

namespace WebTinTuc.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailConfirmation(string email, string fullName, string confirmationLink)
        {
            var smtpClient = new SmtpClient(_configuration["Email:SmtpHost"])
            {
                Port = int.Parse(_configuration["Email:SmtpPort"]),
                Credentials = new NetworkCredential(
                    _configuration["Email:SmtpUsername"],
                    _configuration["Email:SmtpPassword"]),
                EnableSsl = true,
            };

            var fromEmail = _configuration["Email:FromEmail"];
            var fromName = _configuration["Email:FromName"];

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail, fromName),
                Subject = "Xác nhận đăng ký tài khoản",
                Body = $"Xin chào {fullName},<br/><br/>Vui lòng nhấp vào liên kết sau để xác nhận email của bạn: <a href='{confirmationLink}'>Xác nhận ngay</a>.<br/>Liên kết sẽ hết hạn sau 24 giờ.",
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
