using grad2021.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grad2021.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceProvider _serviceProvider;

        public DbInitializer(IServiceProvider serviceProvider, ApplicationDbContext context)
        {
            _serviceProvider = serviceProvider;
        }

        //This example just creates an Administrator role and one Admin users
        public async void Initialize()
        {
            using (var serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                //create database schema if none exists
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                //Role Creation
                var _roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                if (!(await _roleManager.RoleExistsAsync("Administrator")))
                {
                    //Create the Administartor Role
                    await _roleManager.CreateAsync(new IdentityRole("Administrator"));
                }
                if (!(await _roleManager.RoleExistsAsync("Student")))
                {
                    //Create the Student Role
                    await _roleManager.CreateAsync(new IdentityRole("Student"));
                }
                if (!(await _roleManager.RoleExistsAsync("Instructor")))
                {
                    //Create the Instructor Role
                    await _roleManager.CreateAsync(new IdentityRole("Instructor"));
                }
                if (!(await _roleManager.RoleExistsAsync("ControlAdmin")))
                {
                    //Create the ControlAdmin Role
                    await _roleManager.CreateAsync(new IdentityRole("ControlAdmin"));
                }
                if (!(await _roleManager.RoleExistsAsync("StudentAffairs")))
                {
                    //Create the StudentAffairs Role
                    await _roleManager.CreateAsync(new IdentityRole("StudentAffairs"));
                }

                //Create the default Admin account and apply the Administrator role
                var _userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

                // Create admin account
                string user = "admin";
                string password = Environment.GetEnvironmentVariable("ADMIN_PASS") ?? "a";
                if(await _userManager.FindByEmailAsync(user) == null)
                { 
                    var success = await _userManager.CreateAsync(new ApplicationUser
                    {
                        UserName = user,
                        Email = user,
                        EmailConfirmed = true,
                        Gender = Gender.أنثى,
                        AcademicYearID = 2021,
                        //LevelName = LevelName.خريج
                    }, password);
                    if (success.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(user), "Administrator");
                    }
                }

                //Create a ControlAdmin Account
                 user = "ControlAdmin";
                 password = Environment.GetEnvironmentVariable("ControlAdmin_PASS") ?? "a";
                if (await _userManager.FindByEmailAsync(user) == null)
                {
                    var success = await _userManager.CreateAsync(new ApplicationUser
                    {
                        UserName = user,
                        Email = user,
                        EmailConfirmed = true,
                        Gender = Gender.أنثى,
                        AcademicYearID = 2021,
                        //LevelName = LevelName.خريج
                    }, password);
                    if (success.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(user), "ControlAdmin");
                    }
                }


                //Create a StudentAffairs Account

                user = "esraa";
                password = "a";
                if (await _userManager.FindByEmailAsync(user) == null)
                {
                    var success = await _userManager.CreateAsync(new ApplicationUser
                    {
                        UserName = user,
                        Email = user,
                        EmailConfirmed = true,
                        Gender = Gender.أنثى,
                        AcademicYearID = 2021,
                        //LevelName = LevelName.خريج
                    }, password);
                    if (success.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(user), "StudentAffairs");
                    }
                }

                //List<string> selectionBranchNamesPrep = new()
                //{
                //    "الهندسة المدنية",
                //    "الهندسة المعمارية",
                //    "هندسة القوى والآلات الكهربية",
                //    "هندسة الإلكترونيات والاتصالات الكهربية",
                //    "هندسة الحاسبات والنظم",
                //    "الهندسة الميكانيكية",
                //    };
                //for (int i = 1; i < 6; i++)
                //{
                //    string Email = $"Name{i}";
                //    string branchName = "الرياضيات والفيزيقا الهندسية";
                //    if (await _userManager.FindByEmailAsync(Email) == null)
                //    {
                //        password = "a";
                //        var success = await _userManager.CreateAsync(new ApplicationUser
                //        {
                //            AcademicYearID = 2021,
                //            BranchName = branchName,
                //            UserName = $"Name{i}",
                //            Email = $"Name{i}",
                //            EmailConfirmed = true,
                //            NatId = $"{i}",
                //        }, password);
                //        ApplicationUser student = new();
                //        if (success.Succeeded)
                //        {
                //            await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync($"Name{i}"), "Student");
                //            student = await _userManager.FindByEmailAsync($"Name{i}");
                //        }
                //        StudentEnrollment studentEnrollment = new()
                //        {
                //            AcademicYearID = 2021,
                //            BranchName = branchName,
                //            LevelName = "الإعدادية",
                //            ApplicationUserID = student.Id,
                //        };
                //        context.Add(studentEnrollment);
                //        await context.SaveChangesAsync();
                //        int studentEnrollmentID = (await context.StudentEnrollments.FirstAsync(s => s.ApplicationUserID == student.Id)).StudentEnrollmentID;

                //        List<Selection> selections = new();
                //        for (int j = 0; j < selectionBranchNamesPrep.Count; j++)
                //        {
                //            //Random rnd = new Random();
                //            Selection selection = new()
                //            {
                //                //SelectionID = rnd.Next(1, int.MaxValue),
                //                SelectionBranchName = selectionBranchNamesPrep[j],
                //                CurrentBranchName = branchName,
                //                SelectionNo = j + 1,
                //                StudentEnrollmentID = studentEnrollmentID,
                //            };
                //            selections.Add(selection);
                //        }
                //        context.AddRange(selections);
                //        await context.SaveChangesAsync();

                //        var courseEnrollments = await context.CourseEnrollments
                //            .Where(m => m.LevelName == studentEnrollment.LevelName && m.BranchName == branchName)
                //            .ToListAsync();
                //        List<StudentCourse> studentCourses = new();
                //        foreach (CourseEnrollment courseEnrollment in courseEnrollments)
                //        {
                //            Random r = new Random();
                //            StudentCourse studentCourse = new()
                //            {
                //                StudentEnrollmentID = studentEnrollmentID,
                //                AcademicYearID = studentEnrollment.AcademicYearID,
                //                CourseEnrollmentID = courseEnrollment.CourseEnrollmentID,
                //                ApplicationUserID = student.Id,
                //                FinalExamMark = r.Next(60, 125),
                //            };
                //            studentCourses.Add(studentCourse);
                //        }
                //        context.AddRange(studentCourses);
                //        await context.SaveChangesAsync();
                //    }
                //}
            }
        }
    }
}
