INSERT INTO [dbo].[EmailTemplates]  (EmailCode, Subject, Body)
VALUES
('DAILY_REPORT', 'Daily Report Email', 'Hello {username}, This is your scheduled daily report. Please review the attached data.'),
('ONETIME_REMINDER', 'Reminder Email for Event', 'Hello {username}, This is a one-time reminder for your upcoming scheduled event. {event}'),
('MONTHLY_SUMMARY', 'Monthly Summary Email', 'Hello {username}, Please send your report activity for this month');
