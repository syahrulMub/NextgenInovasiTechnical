INSERT INTO [dbo].[EmailTemplates]  (EmailCode, Subject, Body)
VALUES
-- Daily Scheduler
('DAILY_REPORT', 'Daily Report Email', 'Hello {username}, This is your scheduled daily report. Please review the attached data.'),

-- Specific Date Scheduler
('ONETIME_REMINDER', 'Reminder Email for Event', 'Hello {username}, This is a one-time reminder for your upcoming scheduled event. {event}'),

-- Monthly Scheduler
('MONTHLY_SUMMARY', 'Monthly Summary Email', 'Hello {username}, Please send your report activity for this month');
