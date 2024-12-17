using System.Net;
using System.Net.Mail;
using Kbs.Business.EmailNotification;

namespace Kbs.Data.EmailNotification;

public class EmailNotifier : IEmailNotifier
{
    public void SendEmailNotification(string address, string name, string subject, string body)
    {
        var fromAddress = new MailAddress("roeibootverenigingplankie@gmail.com", "From Roeibootvereniging Plankie");
        var toAddress = new MailAddress(address, $"To {name}");
        var fromPassword = "vswl vrrh jrgs wptw";
        
        var smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        };
        var message = new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = body
        };
        smtp.Send(message);
    }
}