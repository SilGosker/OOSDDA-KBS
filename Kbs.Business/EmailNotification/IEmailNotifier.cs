namespace Kbs.Business.EmailNotification;

public interface IEmailNotifier
{
    public void SendEmailNotification(string address, string name, string subject, string body);
}