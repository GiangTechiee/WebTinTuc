namespace WebTinTuc.Services
{
    public interface IEmailService
    {
        Task SendWelcomeEmail(string email, string fullName);
        Task SendEmailConfirmation(string email, string fullName, string confirmationLink);
    }
}
