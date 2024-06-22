using System.Net.Mail;

public class EmailNotificationService : IEmailNotificationService
{
    private readonly SmtpClient _smtpClient;

    public EmailNotificationService(SmtpClient smtpClient)
    {
        _smtpClient = smtpClient;
    }

    public void SendLowStockAlert(string emailAddress, string productName)
    {
        var mailMessage = new MailMessage("noreply@stockapp.com", emailAddress)
        {
            Subject = "Low Stock Alert",
            Body = $"The product {productName} is low on stock."
        };

        _smtpClient.Send(mailMessage);
    }
}