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
            
            var student001 = new Student
            {
                Name = "Avramenko",
                Specialty = specialty11
            };
            
            var student002 = new Student
            {
                Name = "Andreikiv",
                Specialty = specialty11
            };
            
            var student003 = new Student
            {
                Name = "Andruntsiv",
                Specialty = specialty11
            };
            
            var student004 = new Student
            {
                Name = "Babiak",
                Specialty = specialty11
            };
            
            var student005 = new Student
            {
                Name = "Bakay",
                Specialty = specialty11
            };
            
            var student006 = new Student
            {
                Name = "Bulavinets",
                Specialty = specialty11
            };
            
            var student007 = new Student
            {
                Name = "Haburjak",
                Specialty = specialty11
            };
            
            var student008 = new Student
            {
                Name = "Holovaty",
                Specialty = specialty11
            };
            
            var student009 = new Student
            {
                Name = "Evashchuk",
                Specialty = specialty11
            };
            
            var student010 = new Student
            {
                Name = "Yelenyuk",
                Specialty = specialty11
            };
            
            var student011 = new Student
            {
                Name = "Zavhorodniy",
                Specialty = specialty11
            };
            
            var student012 = new Student
            {
                Name = "Zelenko",
                Specialty = specialty11
            };
            
            var student013 = new Student
            {
                Name = "Klyuka",
                Specialty = specialty11
            };
            
            var student014 = new Student
            {
                Name = "Kurivczak",
                Specialty = specialty11
            };
            
            var student015 = new Student
            {
                Name = "Movchan",
                Specialty = specialty11
            };
            
            var student016 = new Student
            {
                Name = "Motryuk",
                Specialty = specialty11
            };
            
            var student017 = new Student
            {
                Name = "Pokotylo",
                Specialty = specialty11
            };
            
            var student018 = new Student
            {
                Name = "Skripchenko",
                Specialty = specialty11
            };
            
            var student019 = new Student
            {
                Name = "Tytovych",
                Specialty = specialty11
            };
            
            var student020 = new Student
            {
                Name = "Shchernulka",
                Specialty = specialty11
            };
            
            var student021 = new Student
            {
                Name = "Yanukovych",
                Specialty = specialty21
            };
            
            var student022 = new Student
            {
                Name = "Chykatilo",
                Specialty = specialty21
            };
            
            var student023 = new Student
            {
                Name = "Khomiak",
                Specialty = specialty21
            };
            
            var student024 = new Student
            {
                Name = "Suprunyuk",
                Specialty = specialty21
            };
            
            var student025 = new Student
            {
                Name = "Silchenko",
                Specialty = specialty21
            };
            
            var student026 = new Student
            {
                Name = "Serben",
                Specialty = specialty21
            };
            
            var student027 = new Student
            {
                Name = "Lazarenko",
                Specialty = specialty21
            };
            
            var student028 = new Student
            {
                Name = "Kolodne",
                Specialty = specialty21
            };
            
            var student029 = new Student
            {
                Name = "Kyva",
                Specialty = specialty21
            };
            
            var student030 = new Student
            {
                Name = "Zavhorodniy",
                Specialty = specialty21
            };
            
            var student031 = new Student
            {
                Name = "Evashchuk",
                Specialty = specialty21
            };
            
            var student032 = new Student
            {
                Name = "Gogol",
                Specialty = specialty21
            };
            
            var student033 = new Student
            {
                Name = "Gavrilyuk",
                Specialty = specialty21
            };
            
            var student034 = new Student
            {
                Name = "Hordiyenko",
                Specialty = specialty21
            };
            
            var student035 = new Student
            {
                Name = "Bublik",
                Specialty = specialty21
            };
            
            var student036 = new Student
            {
                Name = "Bilokhatniuk",
                Specialty = specialty21
            };
            
            var student037 = new Student
            {
                Name = "Arkhipenko",
                Specialty = specialty21
            };
            
            var student038 = new Student
            {
                Name = "Andrichuk",
                Specialty = specialty21
            };
            
            var student039 = new Student
            {
                Name = "Haponenko",
                Specialty = specialty21
            };
            
            var student040 = new Student
            {
                Name = "Hryshchuk",
                Specialty = specialty21
            };
            
            var student041 = new Student
            {
                Name = "Borodaikevych",
                Specialty = specialty12
            };

            var student042 = new Student
            {
                Name = "Kurochkin",
                Specialty = specialty12
            };
            
            var student043 = new Student
            {
                Name = "Kenyiz",
                Specialty = specialty12
            };
            
            var student044 = new Student
            {
                Name = "Dzyubenko",
                Specialty = specialty12
            };
            
            var student045 = new Student
            {
                Name = "Evanishyn",
                Specialty = specialty12
            };
            
            var student046 = new Student
            {
                Name = "Zherdev",
                Specialty = specialty12
            };
            
            var student047 = new Student
            {
                Name = "Zunchenko",
                Specialty = specialty12
            };
            
            var student048 = new Student
            {
                Name = "Zvizdaryk",
                Specialty = specialty12
            };
            
            var student049 = new Student
            {
                Name = "Yeliashkevych",
                Specialty = specialty12
            };
            
            var student050 = new Student
            {
                Name = "Dobryvechir",
                Specialty = specialty12
            };
            
            var student051 = new Student
            {
                Name = "Gulka",
                Specialty = specialty12
            };
            
            var student052 = new Student
            {
                Name = "Zahara",
                Specialty = specialty12
            };
            
            var student053 = new Student
            {
                Name = "Kolesnyk",
                Specialty = specialty12
            };
            
            var student054 = new Student
            {
                Name = "Kosh",
                Specialty = specialty12
            };
            
            var student055 = new Student
            {
                Name = "Loboda",
                Specialty = specialty12
            };
            
            var student056 = new Student
            {
                Name = "Kupriy",
                Specialty = specialty12
            };
            
            var student057 = new Student
            {
                Name = "Mayko",
                Specialty = specialty12
            };
            
            var student058 = new Student
            {
                Name = "Nahnybida",
                Specialty = specialty12
            };
            
            var student059 = new Student
            {
                Name = "Oliynyk",
                Specialty = specialty12
            };
            
            var student060 = new Student
            {
                Name = "Onopka",
                Specialty = specialty12
            };

            var student061 = new Student
            {
                Name = "Pymonenko",
                Specialty = specialty22
            };
            
            var student062 = new Student
            {
                Name = "Roshcha",
                Specialty = specialty22
            };
            var student063 = new Student
            {
                Name = "Semko",
                Specialty = specialty22
            };
            var student064 = new Student
            {
                Name = "Solar",
                Specialty = specialty22
            };
            var student065 = new Student
            {
                Name = "Tereshchenko",
                Specialty = specialty22
            };
            var student066 = new Student
            {
                Name = "Tytovsky",
                Specialty = specialty22
            };
            var student067 = new Student
            {
                Name = "Ustymovych",
                Specialty = specialty22
            };
            var student068 = new Student
            {
                Name = "Fochuk",
                Specialty = specialty22
            };
            var student069 = new Student
            {
                Name = "Khachula",
                Specialty = specialty22
            };
            
            var student070 = new Student
            {
                Name = "Shastko",
                Specialty = specialty22
            };
            
            var student071 = new Student
            {
                Name = "Shcherba",
                Specialty = specialty22
            };
            
            var student072 = new Student
            {
                Name = "Yavorsky",
                Specialty = specialty22
            };
            
            var student073 = new Student
            {
                Name = "Lyashka",
                Specialty = specialty22
            };
            var student074 = new Student
            {
                Name = "Kudleychuk",
                Specialty = specialty22
            };
            var student075 = new Student
            {
                Name = "Kupriy",
                Specialty = specialty22
            };
            var student076 = new Student
            {
                Name = "Kondratyuk",
                Specialty = specialty22
            };
            var student077 = new Student
            {
                Name = "Yovenko",
                Specialty = specialty22
            };
            var student078 = new Student
            {
                Name = "Dudnyk",
                Specialty = specialty22
            };
            var student079 = new Student
            {
                Name = "Gavrilyuk",
                Specialty = specialty22
            };
            var student080 = new Student
            {
                Name = "Hryhorenko",
                Specialty = specialty22
            };
            
            var teacher01 = new Teacher()
            {
                Name = "A"
            };

            var subject01 = new Subject()
            {
                Name = "S"
            };
            
            var auditory01 = new Auditory()
            {
                Number = 101,
                Capacity = 40
            };
            
            var class01 = new Class()
            {
                Type = ClassType.Lection,
                Day = null,
                Number = null,
                Teacher = teacher01,
                Auditory = auditory01,
                Subject = subject01
            };
            
            
            context.Teachers.Add(teacher01);
            context.Subjects.Add(subject01);
            context.Auditories.Add(auditory01);
            context.Classes.Add(class01);
            
            context.Students.Add(student001);
            context.Students.Add(student002);
            context.Students.Add(student003);
            context.Students.Add(student004);
            context.Students.Add(student005);
            context.Students.Add(student006);
            context.Students.Add(student007);
            context.Students.Add(student008);
            context.Students.Add(student009);
            context.Students.Add(student010);
            context.Students.Add(student011);
            context.Students.Add(student012);
            context.Students.Add(student013);
            context.Students.Add(student014);
            context.Students.Add(student015);
            context.Students.Add(student016);
            context.Students.Add(student017);
            context.Students.Add(student018);
            context.Students.Add(student019);
            context.Students.Add(student020);
            context.Students.Add(student021);
            context.Students.Add(student022);
            context.Students.Add(student023);
            context.Students.Add(student024);
            context.Students.Add(student025);
            context.Students.Add(student026);
            context.Students.Add(student027);
            context.Students.Add(student028);
            context.Students.Add(student029);
            context.Students.Add(student030);
            context.Students.Add(student031);
            context.Students.Add(student032);
            context.Students.Add(student033);
            context.Students.Add(student034);
            context.Students.Add(student035);
            context.Students.Add(student036);
            context.Students.Add(student037);
            context.Students.Add(student038);
            context.Students.Add(student039);
            context.Students.Add(student040);
            context.Students.Add(student041);
            context.Students.Add(student042);
            context.Students.Add(student043);
            context.Students.Add(student044);
            context.Students.Add(student045);
            context.Students.Add(student046);
            context.Students.Add(student047);
            context.Students.Add(student049);
            context.Students.Add(student050);
            context.Students.Add(student051);
            context.Students.Add(student052);
            context.Students.Add(student053);
            context.Students.Add(student054);
            context.Students.Add(student055);
            context.Students.Add(student056);
            context.Students.Add(student057);
            context.Students.Add(student058);
            context.Students.Add(student059);
            context.Students.Add(student060);
            context.Students.Add(student061);
            context.Students.Add(student062);
            context.Students.Add(student063);
            context.Students.Add(student064);
            context.Students.Add(student065);
            context.Students.Add(student066);
            context.Students.Add(student067);
            context.Students.Add(student068);
            context.Students.Add(student069);
            context.Students.Add(student070);
            context.Students.Add(student071);
            context.Students.Add(student072);
            context.Students.Add(student073);
            context.Students.Add(student074);
            context.Students.Add(student075);
            context.Students.Add(student076);
            context.Students.Add(student077);
            context.Students.Add(student078);
            context.Students.Add(student079);
            context.Students.Add(student080);
       
            context.Specialties.Add(specialty11);
            context.Specialties.Add(specialty12);
            context.Specialties.Add(specialty21);
            context.Specialties.Add(specialty22);
                
            context.SaveChanges();
        }
    }
}