namespace NextgenInovasiTest.Models;

public class MailSettings
{
    public string? SmtpServer { get; set; }
    public string? FromAddress { get; set; }
    public string? FromName { get; set; }
    public int SmtpPort { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
}
