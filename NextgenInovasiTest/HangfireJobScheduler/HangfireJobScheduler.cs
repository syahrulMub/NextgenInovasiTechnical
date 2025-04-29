using Hangfire;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace NextgenInovasiTest.HangfireJobScheduler;

public static class HangfireJobScheduler
{
    public static void RegisterJobs()
    {
        RecurringJob.AddOrUpdate<IEmailSender>(
            "send-minutes-report",
            sender => sender.SendEmailAsync("syahrul.mubarrok2@gmail.com", "daily Minutely", "this is Minutely job"),
            Cron.Minutely
        );
        RecurringJob.AddOrUpdate<IEmailSender>(

    "send-daily-report",
        sender => sender.SendEmailAsync("syahrul.mubarrok2@gmail.com", "daily job at 8 ", "this is daily job"),
         "0 8 * * *"
        );
    }
}
