using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Radzen;
using Schedule.DAL.Context;
using Schedule.DAL.Entities;
using Schedule.DAL.Enums;

namespace Schedule.Blazor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddScoped<NotificationService>();

            services.AddDbContext<ScheduleContext>(opt => opt.UseInMemoryDatabase("Schedule"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            SeedDatabase(app);
        }

        private static void SeedDatabase(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<ScheduleContext>();
                
            context.Database.EnsureCreated();
            
            var specialty11 = new Specialty
            {
                Name = "CS-1",
                StudentCount = 23
            };
            
            var specialty12 = new Specialty
            {
                Name = "CS-2",
                
            };
            
            var specialty21 = new Specialty
            {
                Name = "SE-1",
                
            };
            
            var specialty22 = new Specialty
            {
                Name = "SE-2",
                
            };

            var teacher01 = new Teacher()
            {
                Name = "A"
            };

            var subject01 = new Subject()
            {
                Name = "S",
                Specialty = specialty11
            };
            
            var auditory01 = new Auditory()
            {
                Number = 101,
                Capacity = 40
            };

            context.Teachers.Add(teacher01);
            context.Subjects.Add(subject01);
            context.Auditories.Add(auditory01);

            context.Specialties.Add(specialty11);
            context.Specialties.Add(specialty12);
            context.Specialties.Add(specialty21);
            context.Specialties.Add(specialty22);

            context.SaveChanges();
            
            var group1 = new Group
            {
                Number = 1
            };
            var group2 = new Group
            {
                Number = 2
            };
            
            context.Groups.Add(group1);
            context.Groups.Add(group2);

            var class01 = new Class
            {
                Type = ClassType.Lection,
                Day = null,
                Number = null,
                Teacher = teacher01,
                Auditory = auditory01,
                Subject = subject01
            };
            
            context.Classes.Add(class01);
            context.Classes.Load();

            class01.Groups = new List<Group> {group1, group2};
            
            context.SaveChanges();
        }
    }
}