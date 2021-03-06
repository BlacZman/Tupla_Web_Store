using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tupla.Data.Context;
using Tupla.Data.Core.CodeData;
using Tupla.Data.Core.CompanyData;
using Tupla.Data.Core.CreditCard;
using Tupla.Data.Core.CustomerData;
using Tupla.Data.Core.GameData;
using Tupla.Data.Core.PictureData;
using Tupla.Data.Core.PlatformData;
using Tupla.Data.Core.PromotionData;
using Tupla.Data.Core.ReviewData;
using Tupla.Data.Core.Shopping.CartData;
using Tupla.Data.Core.Shopping.OrderDetailData;
using Tupla.Data.Core.Shopping.TransactionData;
using Tupla.Data.Core.Tag;
using Tupla.Data.Core.WishList;

namespace Tupla_Web_Store
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<TuplaContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DataContextConnection"));
            });
            services.AddScoped<ICompany, SqlCompanyData>();
            services.AddScoped<IGame, SqlGameData>();
            services.AddScoped<IGamePicture, SqlGamePictureData>();
            services.AddScoped<ICompanyPicture, SqlCompanyPictureData>();
            services.AddScoped<ICustomerPicture, SqlCustomerPictureData>();
            services.AddScoped<IPlatform, SqlPlatformData>();
            services.AddScoped<IPlatformOfGame, SqlPlatformOfGameData>();
            services.AddScoped<ITag, SqlTagData>();
            services.AddScoped<IGameTag, SqlGameTagData>();
            services.AddScoped<IWishList, SqlWishListData>();
            services.AddScoped<ICreditCard, SqlCreditCardData>();
            services.AddScoped<ICart, SqlCartData>();
            services.AddScoped<IOrderDetail, SqlOrderDetailData>();
            services.AddScoped<ITransaction, SqlTransactionData>();
            services.AddScoped<IReview, SqlReviewData>();
            services.AddScoped<ICode, SqlCodeData>();
            services.AddScoped<IEventPromotion, SqlEventPromotionData>();
            services.AddScoped<IPromotion, SqlPromotionData>();
            services.AddRazorPages();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjU1MTQ2QDMxMzgyZTMxMmUzMGlYNUNPMUhuZzB4bGU5YzZWR0txelByUThPVjVzUEE3OVpuajFPUE9meUU9");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
