using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using NextgenInovasiTest.Models;
namespace NextgenInovasiTest.Areas.Identity.Pages.Services;

public class EmailSender : IEmailSender
{

    private readonly MailSettings _mailSettings;

    public EmailSender(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        try
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_mailSettings.FromName, _mailSettings.FromAddress));
            emailMessage.To.Add(MailboxAddress.Parse(email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain")
            {
                Text = message
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(
                _mailSettings.SmtpServer,
                _mailSettings.SmtpPort,
                MailKit.Security.SecureSocketOptions.StartTls
            );
            await client.AuthenticateAsync(_mailSettings.Username, _mailSettings.Password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Gagal mengirim email: {ex.Message}");
            throw;
        }
    }
}
