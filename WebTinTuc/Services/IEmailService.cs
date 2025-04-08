namespace WebTinTuc.Services
{
    public interface IEmailService
    {
        Task SendEmailConfirmation(string email, string fullName, string confirmationLink);
    }
}
