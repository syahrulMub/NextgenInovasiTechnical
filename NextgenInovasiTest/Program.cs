using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NextgenInovasiTest.Areas.Identity.Pages.Services;
using NextgenInovasiTest.BusinessLogic;
using NextgenInovasiTest.DatabaseContext;
using NextgenInovasiTest.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using NextgenInovasiTest.HangfireJobScheduler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<User>
(options => options.SignIn.RequireConfirmedAccount = true)
.AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<HangfireJobScheduler>();

builder.Services.AddScoped<IBusinessLogic<Item>, ItemBusinessLogic>();

builder.Services.AddHangfire(x => x.UseMemoryStorage());
builder.Services.AddHangfireServer();

builder.Services.AddRazorPages();

builder.Services.AddAuthentication()
.AddCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
});

builder.Services.AddAuthorization();


//add session
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = ".AspNetCore.Identity.Application";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    options.ReturnUrlParameter = "/Home";
    options.SlidingExpiration = false;
});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

CreateAdminOnDatabase.CreateAdminDataOnDatabase(app);
CreateRoleOnDatabase.CreateRole(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseHangfireDashboard();
RecurringJob.AddOrUpdate<HangfireJobScheduler>(
    "send-daily-report",
    job => job.SendDailyReport(),
    "0 8 * * *"
);
RecurringJob.AddOrUpdate<HangfireJobScheduler>(
    "send-minutes-report",
    job => job.SendMonthlyReport(),
    Cron.Minutely
);

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
