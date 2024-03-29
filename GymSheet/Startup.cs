﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GymSheet.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Rotativa.AspNetCore;
using GymSheet.Services;
using jsreport.AspNetCore;
using jsreport.Local;
using jsreport.Binary;

namespace GymSheet
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnDB")));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Administrators/Login";
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
            });

            services.AddScoped<MuscleGroupService>();
            services.AddScoped<AdministratorService>();
            services.AddScoped<ExerciseService>();
            services.AddScoped<TeacherService>();
            services.AddScoped<ObjectiveService>();
            services.AddScoped<StudentService>();
            services.AddScoped<SheetService>();
            services.AddScoped<ExerciseListService>();

            services.AddJsReport(new LocalReporting()
             .UseBinary(JsReportBinary.GetBinary())
             .AsUtility()
             .Create());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication();

            RotativaConfiguration.Setup(env);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Administrators}/{action=Login}/{id?}");
            });
        }
    }
}
