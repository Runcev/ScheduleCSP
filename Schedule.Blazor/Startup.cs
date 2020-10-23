using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Radzen;
using Schedule.Blazor.Services;
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

            services.AddScoped<ScheduleService>();
            
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
                Name = "CS-2",
                StudentCount = 23
            };

            var specialty12 = new Specialty
            {
                Name = "CS-3",
                StudentCount = 43
            };

            var specialty21 = new Specialty
            {
                Name = "SE-2",
                StudentCount = 30
            };

            var specialty22 = new Specialty
            {
                Name = "SE-3",
                StudentCount = 35
            };

            var teacher01 = new Teacher()
            {
                Name = "Бублик"
            };

            var teacher02 = new Teacher()
            {
                Name = "Проценко"
            };

            var teacher03 = new Teacher()
            {
                Name = "Гулаєва"
            };

            var teacher04 = new Teacher()
            {
                Name = "Трон"
            };

            var teacher05 = new Teacher()
            {
                Name = "Вовк"
            };

            var teacher06 = new Teacher()
            {
                Name = "Ющенко"
            };

            var teacher07 = new Teacher()
            {
                Name = "Вознюк"
            };

            var teacher08 = new Teacher()
            {
                Name = "Бабич"
            };

            var teacher09 = new Teacher()
            {
                Name = "Франків"
            };

            var teacher10 = new Teacher()
            {
                Name = "Кирієнко"
            };

            var teacher11 = new Teacher()
            {
                Name = "Печкурова"
            };

            var teacher12 = new Teacher()
            {
                Name = "Олецький"
            };

            var teacher13 = new Teacher()
            {
                Name = "Глибовець"
            };

            var teacher14 = new Teacher()
            {
                Name = "Тригуб"
            };

            var teacher15 = new Teacher()
            {
                Name = "Франчук"
            };

            var teacher16 = new Teacher()
            {
                Name = "Карпович"
            };

            var teacher17 = new Teacher()
            {
                Name = "Бучко"
            };

            var teacher18 = new Teacher()
            {
                Name = "Кундик"
            };

            var teacher19 = new Teacher()
            {
                Name = "Афонін"
            };

            var teacher20 = new Teacher()
            {
                Name = "Жежерун"
            };

            var subject01 = new Subject()
            {
                Name = "Процедурне програмування",
                Specialty = specialty11
            };

            var subject02 = new Subject()
            {
                Name = "Бази Даних",
                Specialty = specialty11
            };

            var subject03 = new Subject()
            {
                Name = "МПА",
                Specialty = specialty11
            };

            var subject04 = new Subject()
            {
                Name = "Компютерні мережі",
                Specialty = specialty11
            };

            var subject05 = new Subject()
            {
                Name = "ФП",
                Specialty = specialty12
            };

            var subject06 = new Subject()
            {
                Name = "ОШІ",
                Specialty = specialty12
            };

            var subject07 = new Subject()
            {
                Name = "МООП",
                Specialty = specialty12
            };

            var subject08 = new Subject()
            {
                Name = "Веб",
                Specialty = specialty12
            };

            var subject09 = new Subject()
            {
                Name = "ММОЗ",
                Specialty = specialty21
            };

            var subject10 = new Subject()
            {
                Name = "ОЗ",
                Specialty = specialty21
            };

            var subject11 = new Subject()
            {
                Name = "НІТ",
                Specialty = specialty21
            };

            var subject12 = new Subject()
            {
                Name = "ОКА",
                Specialty = specialty21
            };

            var subject13 = new Subject()
            {
                Name = "Системи програмування",
                Specialty = specialty22
            };

            var subject14 = new Subject()
            {
                Name = "IOS",
                Specialty = specialty22
            };

            var subject15 = new Subject()
            {
                Name = "Комп вірусологія",
                Specialty = specialty22
            };

            var subject16 = new Subject()
            {
                Name = "Архітектура прикладних програм",
                Specialty = specialty22
            };

            var auditory01 = new Auditory()
            {
                Number = 121,
                Capacity = 40
            };

            var auditory02 = new Auditory()
            {
                Number = 221,
                Capacity = 60
            };

            var auditory03 = new Auditory()
            {
                Number = 308,
                Capacity = 12
            };

            var auditory04 = new Auditory()
            {
                Number = 313,
                Capacity = 15
            };

            var auditory05 = new Auditory()
            {
                Number = 309,
                Capacity = 12
            };

            var auditory06 = new Auditory()
            {
                Number = 223,
                Capacity = 50
            };

            var auditory07 = new Auditory()
            {
                Number = 109,
                Capacity = 12
            };
            
            var auditory08 = new Auditory()
            {
                Number = 800,
                Capacity = 50
            };
            
            var auditory09 = new Auditory()
            {
                Number = 801,
                Capacity = 50
            };
            
            var auditory10 = new Auditory()
            {
                Number = 802,
                Capacity = 50
            };
            
            var auditory11 = new Auditory()
            {
                Number = 803,
                Capacity = 50
            };
            
            var auditory12 = new Auditory()
            {
                Number = 804,
                Capacity = 50
            };
            
            var auditory13 = new Auditory()
            {
                Number = 805,
                Capacity = 50
            };
            
            var auditory14 = new Auditory()
            {
                Number = 806,
                Capacity = 50
            };

            context.Teachers.Add(teacher01);
            context.Teachers.Add(teacher02);
            context.Teachers.Add(teacher03);
            context.Teachers.Add(teacher04);
            context.Teachers.Add(teacher05);
            context.Teachers.Add(teacher06);
            context.Teachers.Add(teacher07);
            context.Teachers.Add(teacher08);
            context.Teachers.Add(teacher09);
            context.Teachers.Add(teacher10);
            context.Teachers.Add(teacher11);
            context.Teachers.Add(teacher12);
            context.Teachers.Add(teacher13);
            context.Teachers.Add(teacher14);
            context.Teachers.Add(teacher15);
            context.Teachers.Add(teacher16);

            context.Subjects.Add(subject01);
            context.Subjects.Add(subject02);
            context.Subjects.Add(subject03);
            context.Subjects.Add(subject04);
            context.Subjects.Add(subject05);
            context.Subjects.Add(subject06);
            context.Subjects.Add(subject07);
            context.Subjects.Add(subject08);
            context.Subjects.Add(subject09);
            context.Subjects.Add(subject10);
            context.Subjects.Add(subject11);
            context.Subjects.Add(subject12);
            context.Subjects.Add(subject13);
            context.Subjects.Add(subject14);
            context.Subjects.Add(subject15);
            context.Subjects.Add(subject16);


            context.Auditories.Add(auditory01);
            context.Auditories.Add(auditory02);
            context.Auditories.Add(auditory03);
            context.Auditories.Add(auditory04);
            context.Auditories.Add(auditory05);
            context.Auditories.Add(auditory06);
            context.Auditories.Add(auditory07);
            context.Auditories.Add(auditory08);
            context.Auditories.Add(auditory09);
            context.Auditories.Add(auditory10);
            context.Auditories.Add(auditory11);
            context.Auditories.Add(auditory12);
            context.Auditories.Add(auditory13);
            context.Auditories.Add(auditory14);
            
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
            var group3 = new Group
            {
                Number = 3
            };
            var group4 = new Group
            {
                Number = 4
            };
            var group5 = new Group
            {
                Number = 5,
            };

            var groups = new List<Group>();

            for (int i = 0; i < 50; i++)
            {
                groups.Add(new Group {Number = i});
                context.Groups.Add(groups.ElementAt(i));
            }


            context.Groups.Add(group1);
            context.Groups.Add(group2);
            context.Groups.Add(group3);
            context.Groups.Add(group4);
            context.Groups.Add(group5);


            var class01 = new Class
            {
                Type = ClassType.Lection,
                Teacher = teacher01,
            };

            var class02 = new Class
            {
                Type = ClassType.Lection,
                Teacher = teacher03,
            };

            var class03 = new Class
            {
                Type = ClassType.Lection,
                Teacher = teacher13,
            };

            var class04 = new Class
            {
                Type = ClassType.Lection,
                Teacher = teacher07,
            };

            var class05 = new Class
            {
                Type = ClassType.Lection,
                Teacher = teacher02,
            };

            var class06 = new Class
            {
                Type = ClassType.Practice,
                Teacher = teacher09,
            };

            var class07 = new Class
            {
                Type = ClassType.Lection,
                Teacher = teacher01,
            };

            var class08 = new Class
            {
                Type = ClassType.Lection,
                Teacher = teacher12,
            };

            var class09 = new Class
            {
                Type = ClassType.Lection,
                Teacher = teacher17,
            };

            var class10 = new Class
            {
                Type = ClassType.Lection,
                Teacher = teacher19,
            };

            var class11 = new Class
            {
                Type = ClassType.Lection,
                Teacher = teacher04,
            };

            var class12 = new Class
            {
                Type = ClassType.Lection,
                Teacher = teacher13,
            };

            var class13 = new Class
            {
                Type = ClassType.Lection,
                Teacher = teacher20,
            };

            var class14 = new Class
            {
                Type = ClassType.Lection,
                Teacher = teacher09,
            };

            var class15 = new Class
            {
                Type = ClassType.Lection,
                Teacher = teacher10,
            };

            var class16 = new Class
            {
                Type = ClassType.Lection,
                Teacher = teacher08,
            };

            var class17 = new Class
            {
                Type = ClassType.Practice,
                Teacher = teacher05,
            };

            var class18 = new Class
            {
                Type = ClassType.Practice,
                Teacher = teacher17,
            };

            var class19 = new Class
            {
                Type = ClassType.Practice,
                Teacher = teacher03,
            };

            var class20 = new Class
            {
                Type = ClassType.Practice,
                Teacher = teacher06,
            };

            context.Classes.Add(class01);
            context.Classes.Add(class02);
            context.Classes.Add(class03);
            context.Classes.Add(class04);
            context.Classes.Add(class05);
            context.Classes.Add(class06);
            context.Classes.Add(class07);
            context.Classes.Add(class08);
            context.Classes.Add(class09);
            context.Classes.Add(class10);
            context.Classes.Add(class11);
            context.Classes.Add(class12);
            context.Classes.Add(class13);
            context.Classes.Add(class14);
            context.Classes.Add(class15);
            context.Classes.Add(class16);
            context.Classes.Add(class17);
            context.Classes.Add(class18);
            context.Classes.Add(class19);
            context.Classes.Add(class20);


            context.Classes.Load();

            class01.Group = groups.ElementAt(1);
            class02.Group = groups.ElementAt(2);
            class03.Group = groups.ElementAt(3);
            class04.Group = groups.ElementAt(4);
            class05.Group = groups.ElementAt(5);
            class06.Group = groups.ElementAt(6);
            class07.Group = groups.ElementAt(7);
            class08.Group = groups.ElementAt(8);
            class09.Group = groups.ElementAt(9);
            class10.Group = groups.ElementAt(10);
            class11.Group = groups.ElementAt(11);
            class12.Group = groups.ElementAt(12);
            class13.Group = groups.ElementAt(13);
            class14.Group = groups.ElementAt(14);
            class15.Group = groups.ElementAt(15);
            class16.Group = groups.ElementAt(16);
            class17.Group = groups.ElementAt(17);
            class18.Group = groups.ElementAt(18);
            class19.Group = groups.ElementAt(19);
            class20.Group = groups.ElementAt(20);

            context.SaveChanges();
            
            subject01.AddClass(class01);
            subject02.AddClass(class02);
            subject03.AddClass(class03);
            subject04.AddClass(class04);
            subject04.AddClass(class17);
            subject05.AddClass(class05);
            subject06.AddClass(class06);
            subject07.AddClass(class07);
            subject08.AddClass(class08);
            subject09.AddClass(class09);
            subject10.AddClass(class10);
            subject11.AddClass(class11);
            subject11.AddClass(class19);
            subject11.AddClass(class20);
            subject12.AddClass(class12);
            subject13.AddClass(class13);
            subject14.AddClass(class14);
            subject15.AddClass(class15);
            subject16.AddClass(class16);
            subject16.AddClass(class18);

            for (int day = (int) Day.Mon; day < (int) Day.Sat; day++)
            {
                for (int number = 1; number <= 6; number++)
                {
                    context.DayTimes.Add(new DayTime
                    {
                        Day = (Day) day,
                        Number = number
                    });
                }
            }

            context.SaveChanges();
        }
    }
}