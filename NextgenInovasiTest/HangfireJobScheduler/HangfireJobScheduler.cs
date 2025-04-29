using Hangfire;
using Microsoft.AspNetCore.Identity.UI.Services;
using NextgenInovasiTest.DatabaseContext;

namespace NextgenInovasiTest.HangfireJobScheduler;

public class HangfireJobScheduler
{
    private readonly ApplicationDbContext _context;
    private readonly IEmailSender _emailSender;
    public HangfireJobScheduler(ApplicationDbContext context, IEmailSender emailSender)
    {
        _emailSender = emailSender;
        _context = context;
    }
    public async Task SendDailyReport()
    {
        var users = _context.Users.ToList();
        var EmailTemplate = _context.EmailTemplates.FirstOrDefault(x => x.EmailCode == "DAILY_REPORT");

        foreach (var user in users)
        {
            string subject = EmailTemplate.Subject;
            string body = EmailTemplate.Body.Replace("{username}", user.UserName);
            await _emailSender.SendEmailAsync(user.Email, subject, body);
        }
    }

    public async Task SendMonthlyReport()
    {
        var users = _context.Users.ToList();
        var EmailTemplate = _context.EmailTemplates.FirstOrDefault(x => x.EmailCode == "MONTHLY_SUMMARY");

        foreach (var user in users)
        {
            string? subject = EmailTemplate?.Subject;
            string? body = EmailTemplate?.Body.Replace("{username}", user.UserName);
            await _emailSender.SendEmailAsync(user.Email, subject, body);
        }

    }
}
