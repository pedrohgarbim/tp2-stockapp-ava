public interface IEmailNotificationService
{
    void SendLowStockAlert(string emailAddress, string productName);
}