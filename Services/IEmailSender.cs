namespace LandingServer.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string name, string email, string phone);
    }
}
