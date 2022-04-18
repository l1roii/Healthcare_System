using System;
using HealthcareSystem.Areas.Identity.Data;
using HealthcareSystem.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(HealthcareSystem.Areas.Identity.IdentityHostingStartup))]
namespace HealthcareSystem.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<HealthcareDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("HealthcareDbContextConnection")));

                services.AddDefaultIdentity<Users>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<HealthcareDbContext>();
            });
        }
    }
}