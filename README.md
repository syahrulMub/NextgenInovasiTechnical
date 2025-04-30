# 🔐 Portal Web Application with MFA/2FA, LDAP, and Email OTP

## ✅ 1. Authentication Feature

I have implemented the authentication feature using **ASP.NET Identity**, which includes:
- Multi-Factor Authentication (MFA)
- Two-Factor Authentication (2FA)
- Password reset via email

The password reset request is sent through email using SMTP configured with **MailKit**.

2FA login can also use **QR Code**, accessible via the user's profile > Two-Factor Authenticator.

*note : the OTP via email not yet implement*

---

## 📥 2. Menus

### 📤 Upload Transaction Menu
- I have built a feature that accepts Excel file uploads.
- I used the **NPOI** package to read the Excel data.
- Uploaded data will saved to the database.
- A download Excel template Feature is also implement and matching with database columns.

### 📥 Download / Export Transaction Menu
- Uploaded data is shown on the web application using a data table.
- Export options are available for:
  - **Excel**
  - **PDF**

> Export functionality is handled **on the client-side** using **DataTables export library**, which I chose for its speed and to avoid server-side errors within the tight deadline.

*note : the UI Export, Upload , and Grid Data showed in one menu*

---

## ⏰ 3. Email Notification Reminder via Background Scheduler

I have implemented notification and reminder demo (daily and monthly) using **Hangfire**.
- Email templates stored in the database
- User email from **ASP.NET Identity**

> While this implementation is not yet fully dynamic, it meets the current requirements. It could be extended using an **EmailQueue** system for better flexibility, but this would more advanced configuration. For now, using `User` and `EmailTemplate` tables provides a practical and effective solution.

---

## 🔒 4. Application Security

In addition to the built-in security features of ASP.NET Identity, I have added:
- **Session timeout management** for inactive users

> This addition further enhances security by automatically logging out users who are idle for a certain period, protecting the application from unauthorized access.
