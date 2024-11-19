using System.Net.Mail;
using HopsHub.Api.Services.Interfaces;

namespace HopsHub.Api.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var emailSettings = _configuration.GetSection("EmailSettings");

        string smtpServer = emailSettings["SmtpServer"];
        string smtpPortString = emailSettings["SmtpPort"];
        string senderEmail = emailSettings["SenderEmail"];
        string senderName = emailSettings["SenderName"];
        //string smtpUser = emailSettings["SmtpUser"];
        //string smtpPassword = emailSettings["SmtpPassword"];

        if (string.IsNullOrWhiteSpace(smtpServer) ||
            string.IsNullOrWhiteSpace(smtpPortString) ||
             string.IsNullOrWhiteSpace(senderEmail) ||
             string.IsNullOrWhiteSpace(senderName))
             //string.IsNullOrWhiteSpace(smtpUser) ||
             //string.IsNullOrWhiteSpace(smtpPassword))
        {
            throw new ArgumentNullException("Email settings are not configured correctly.");
        }

        if (!int.TryParse(smtpPortString, out int smtpPort))
        {
            throw new ArgumentException("SmtpPort must be a valid integer.");
        }

        //Todo: Enable ssl in production environment
        var smtpClient = new SmtpClient(smtpServer)
        {
            Port = smtpPort,
            //Credentials = new System.Net.NetworkCredential(smtpUser, smtpPassword),
            DeliveryMethod = SmtpDeliveryMethod.Network,
            //EnableSsl = true,
            UseDefaultCredentials = false
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(senderEmail, senderName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
        };

        mailMessage.To.Add(toEmail);

        await smtpClient.SendMailAsync(mailMessage);
    }
}

