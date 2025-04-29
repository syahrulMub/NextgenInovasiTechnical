using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using NextgenInovasiTest.Areas.Identity.Pages.Services;

[Route("email")]
public class EmailTestController : Controller
{
    private readonly IEmailSender _emailSender;

    public EmailTestController(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    [HttpGet("send-test")]
    public async Task<IActionResult> SendTestEmail()
    {
        try
        {
            var toEmail = "syahrul.mubarrok4@gmail.com";
            var subject = "Test Email dari ASP.NET Core";
            var htmlMessage = "<strong>Email ini berhasil dikirim!</strong><br/>Dikirim via MailKit dan Google SMTP.";

            await _emailSender.SendEmailAsync(toEmail, subject, htmlMessage);

            return Ok("✅ Email berhasil dikirim!");
        }
        catch (Exception ex)
        {
            return BadRequest($"❌ Gagal mengirim email: {ex.Message}");
        }

    }
}