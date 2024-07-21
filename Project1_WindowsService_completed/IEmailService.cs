namespace Project_WindowsService
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailData emailData);
    }
}
