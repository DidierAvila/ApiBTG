namespace ApiBTG.Application.Common.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(string to, string subject, string message);
    }
} 