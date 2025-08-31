using CompanySystem.BusinessLogic.Common.Services.Attachments;
using CompanySystem.BusinessLogic.Services.Departments;
using CompanySystem.BusinessLogic.Services.Employees;
using CompanySystem.DataAccessLayer.Models.Identity;
using CompanySystem.DataAccessLayer.Persistence.Data.Contexts;
using CompanySystem.DataAccessLayer.Persistence.Repositories.Departments;
using CompanySystem.DataAccessLayer.Persistence.Repositories.Employees;
using CompanySystem.DataAccessLayer.Persistence.UnitOfWork;
using CompanySystem.Presentation.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CompanySystem.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services

            builder.Services.AddControllersWithViews();
            //builder.Services.AddScoped<ApplicationDbContext>();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options
                .UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddTransient<IAttachmentService, AttachmentService>();

            builder.Services.AddAutoMapper(M => M.AddProfile(typeof(MappingProfile)));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>((options) =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true; // #%$
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 1;

                options.User.RequireUniqueEmail = true;


                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(5);

            })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/SignIn";
                options.AccessDeniedPath = "/Home/Error";
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                //options.LogoutPath = "/Account/SignIn";
                //options.ForwardSignOut = "/Account/SignIn";
            });


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Identity.Application";
                options.DefaultChallengeScheme = "Identity.Application";
            })
                .AddCookie("Admin",".AspNetCore.Admin" ,options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Home/Error";
                    options.ExpireTimeSpan = TimeSpan.FromDays(10);
                    options.LogoutPath = "/Account/SignIn";
                });


            #endregion

            var app = builder.Build();

            #region Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"); 
            #endregion

            app.Run();
        }
    }
}
