using grad2021.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace grad2021.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseEnrollment> CourseEnrollments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentCode> DepartmentCodes { get; set; }
        public DbSet<InstructorEnrollment> InstructorEnrollments { get; set; }
        public DbSet<InstructorProfession> InstructorProfessions { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Selection> Selections { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<StudentEnrollment> StudentEnrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //modelBuilder.Entity<AcademicYear>()
            //    .HasKey(d => d.AcademicYearID);

                modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.HasOne(bh => bh.Branch)
                .WithMany()
                .HasForeignKey(bf => bf.BranchName)
                .OnDelete(DeleteBehavior.Restrict);
                b.HasOne(bh => bh.AcademicYear)
                .WithMany()
                .HasForeignKey(bf => bf.AcademicYearID)
                .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<Branch>(b =>
            {
                b.HasKey(b => b.BranchName);
                b.HasOne(bh => bh.Department)
                .WithMany(bw => bw.Branches)
                .HasForeignKey(bf => bf.DepartmentName)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Course>(b =>
            {
                b.HasKey(c => c.CourseName);
                b.HasOne(ch => ch.DepartmentCode)
                .WithMany(cw => cw.Courses)
                .HasForeignKey(cf => cf.DepartmentCodeValue)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CourseEnrollment>(b =>
            {
                //b.HasKey(ce => ce.CourseEnrollmentID);
                //b.HasAlternateKey(a => new { a.Course, a.BranchName });
                b.HasOne(ceh => ceh.Course)
                .WithMany(cew => cew.CourseEnrollments)
                .HasForeignKey(dcf => dcf.CourseName)
                .OnDelete(DeleteBehavior.Restrict);
                b.HasOne(ceh => ceh.Branch)
                .WithMany(cew => cew.CourseEnrollments)
                .HasForeignKey(dcf => dcf.BranchName)
                .OnDelete(DeleteBehavior.Restrict);
                b.HasOne(ceh => ceh.Level)
                .WithMany(cew => cew.CourseEnrollments)
                .HasForeignKey(dcf => dcf.LevelName)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Department>()
                .HasKey(d => d.DepartmentName);

            modelBuilder.Entity<DepartmentCode>(b =>
            {
                b.HasKey(dc => dc.DepartmentCodeValue);
                b.HasOne(dch => dch.Department)
                .WithMany(dcw => dcw.DepartmentCodes)
                .HasForeignKey(dcf => dcf.DepartmentName)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<InstructorEnrollment>(b =>
            {
                //b.HasKey(ie => ie.InstructorEnrollmentID);
                b.HasOne(a => a.ApplicationUser)
                .WithMany(a => a.InstructorEnrollments)
                .HasForeignKey(a => a.ApplicationUserID)
                .OnDelete(DeleteBehavior.Restrict);
                b.HasOne(a => a.CourseEnrollment)
                .WithMany(a => a.InstructorEnrollments)
                .HasForeignKey(a => a.CourseEnrollmentID)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<InstructorProfession>(b =>
            {
                //b.HasKey(ie => ie.InstructorProfessionID);
                b.HasOne(p => p.ApplicationUser)
                .WithMany(b => b.InstructorProfessions)
                .HasForeignKey(s => s.ApplicationUserID)
                .OnDelete(DeleteBehavior.Cascade);
                b.HasOne(p => p.Level)
                .WithMany(b => b.InstructorProfessions)
                .HasForeignKey(s => s.ProfessionDegree)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Level>(b =>
            {
                b.HasKey(ie => ie.LevelName);
            });

            modelBuilder.Entity<Selection>(b =>
            {
                //    b.HasKey(ie => ie.SelectionID);
                b.HasOne(ie => ie.StudentEnrollment)
                 .WithMany(b => b.Selections)
                 .HasForeignKey(ie => ie.StudentEnrollmentID)
                .OnDelete(DeleteBehavior.Restrict);
                b.HasOne(ie => ie.CurrentBranch)
                 .WithMany(b => b.CurrentBranches)
                 .HasForeignKey(s => s.CurrentBranchName)
                .OnDelete(DeleteBehavior.Restrict);
                b.HasOne(ie => ie.SelectionBranch)
                 .WithMany(b => b.SelectionBranches)
                 .HasForeignKey(s => s.SelectionBranchName)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<StudentCourse>(b =>
            {
                //b.HasKey(ie => ie.StudentCourseID);
                b.HasOne(p => p.StudentEnrollment)
                .WithMany(b => b.StudentCourses)
                .HasForeignKey(s => s.StudentEnrollmentID)
                .OnDelete(DeleteBehavior.Restrict);
                b.HasOne(p => p.ApplicationUser)
                .WithMany(b => b.StudentCourses)
                .HasForeignKey(s => s.ApplicationUserID)
                .OnDelete(DeleteBehavior.Cascade);
                b.HasOne(p => p.CourseEnrollment)
                .WithMany(b => b.StudentCourses)
                .HasForeignKey(s => s.CourseEnrollmentID)
                .OnDelete(DeleteBehavior.Restrict);
                b.HasOne(p => p.AcademicYear)
                .WithMany(b => b.StudentCourses)
                .HasForeignKey(s => s.AcademicYearID)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<StudentEnrollment>(b =>
            {
                //b.HasKey(s => s.StudentEnrollmentID);
                b.HasOne(p => p.ApplicationUser)
                .WithMany(b => b.StudentEnrollments)
                .HasForeignKey(s => s.ApplicationUserID)
                .OnDelete(DeleteBehavior.Cascade);
                b.HasOne(p => p.AcademicYear)
                .WithMany(b => b.StudentEnrollments)
                .HasForeignKey(s => s.AcademicYearID)
                .OnDelete(DeleteBehavior.Cascade);
                b.HasOne(p => p.Branch)
                .WithMany(b => b.StudentEnrollments)
                .HasForeignKey(s => s.BranchName)
                .OnDelete(DeleteBehavior.Restrict);
                b.HasOne(ceh => ceh.Level)
                .WithMany(cew => cew.StudentEnrollments)
                .HasForeignKey(dcf => dcf.LevelName)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<AcademicYear>().HasData(new AcademicYear { AcademicYearID = 2021 });

            modelBuilder.Entity<Level>().HasData(new Level { LevelName = "الإعدادية", MaxFailures = 2 });
            modelBuilder.Entity<Level>().HasData(new Level { LevelName = "الأولى", MaxFailures = 2 });
            modelBuilder.Entity<Level>().HasData(new Level { LevelName = "الثانية", MaxFailures = 2 });
            modelBuilder.Entity<Level>().HasData(new Level { LevelName = "الثالثة", MaxFailures = 2 });
            modelBuilder.Entity<Level>().HasData(new Level { LevelName = "الرابعة", MaxFailures = 100 });
            modelBuilder.Entity<Level>().HasData(new Level { LevelName = "معيد", MaxFailures = 0 });
            modelBuilder.Entity<Level>().HasData(new Level { LevelName = "مدرس مساعد", MaxFailures = 0 });
            modelBuilder.Entity<Level>().HasData(new Level { LevelName = "مدرس", MaxFailures = 0 });
            modelBuilder.Entity<Level>().HasData(new Level { LevelName = "أستاذ مساعد", MaxFailures = 0 });
            modelBuilder.Entity<Level>().HasData(new Level { LevelName = "أستاذ", MaxFailures = 0 });
            modelBuilder.Entity<Level>().HasData(new Level { LevelName = "أستاذ متفرغ", MaxFailures = 0 });
            modelBuilder.Entity<Level>().HasData(new Level { LevelName = "إداري", MaxFailures = 0 });

            modelBuilder.Entity<Department>().HasData(new Department { DepartmentName = "الرياضيات والفيزيقا الهندسية", DepartmentDescription = "وصف قسم الرياضيات والفيزيقا الهندسية" });
            modelBuilder.Entity<Department>().HasData(new Department { DepartmentName = "الهندسة المدنية", DepartmentDescription = "وصف قسم الهندسة المدنية" });
            modelBuilder.Entity<Department>().HasData(new Department { DepartmentName = "الهندسة الكهربية", DepartmentDescription = "وصف قسم الهندسة الكهربية" });
            modelBuilder.Entity<Department>().HasData(new Department { DepartmentName = "الهندسة المعمارية", DepartmentDescription = "وصف قسم الهندسة المعمارية" });
            modelBuilder.Entity<Department>().HasData(new Department { DepartmentName = "الهندسة الميكانيكية", DepartmentDescription = "وصف قسم الهندسة الميكانيكية" });

            modelBuilder.Entity<Branch>().HasData(new Branch { BranchName = "الهندسة المدنية", DepartmentName = "الهندسة المدنية", BranchDescription = "وصف قسم الهندسة المدنية", FullCapacity = 2, SelectionStatus = SelectionStatus.أولى }); ;
            modelBuilder.Entity<Branch>().HasData(new Branch { BranchName = "الهندسة المعمارية", DepartmentName = "الهندسة المعمارية", BranchDescription = "وصف قسم الهندسة المعمارية", FullCapacity = 2, SelectionStatus = SelectionStatus.أولى });
            modelBuilder.Entity<Branch>().HasData(new Branch { BranchName = "الرياضيات والفيزيقا الهندسية", DepartmentName = "الرياضيات والفيزيقا الهندسية", BranchDescription = "وصف قسم الرياضيات والفيزيقا الهندسية", FullCapacity = 2, SelectionStatus = SelectionStatus.إعدادي });
            modelBuilder.Entity<Branch>().HasData(new Branch { BranchName = "هندسة القوى والآلات الكهربية", DepartmentName = "الهندسة الكهربية", BranchDescription = "وصف شعبة هندسة القوى والآلات الكهربية", FullCapacity = 2 , SelectionStatus = SelectionStatus.أولى });
            modelBuilder.Entity<Branch>().HasData(new Branch { BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", DepartmentName = "الهندسة الكهربية", BranchDescription = "وصف شعبة هندسة الإلكترونيات والاتصالات الكهربية", FullCapacity = 2 , SelectionStatus = SelectionStatus.أولى });
            modelBuilder.Entity<Branch>().HasData(new Branch { BranchName = "هندسة الحاسبات والنظم", DepartmentName = "الهندسة الكهربية", BranchDescription = "وصف شعبة هندسة الحاسبات والنظم", FullCapacity = 2, SelectionStatus = SelectionStatus.أولى });
            modelBuilder.Entity<Branch>().HasData(new Branch { BranchName = "الهندسة الميكانيكية", DepartmentName = "الهندسة الميكانيكية", BranchDescription = "وصف قسم الهندسة الميكانيكية", FullCapacity = 2, SelectionStatus = SelectionStatus.أولى });
            modelBuilder.Entity<Branch>().HasData(new Branch { BranchName = "هندسة الإنتاج والتصميم الميكانيكي", DepartmentName = "الهندسة الميكانيكية", BranchDescription = "وصف شعبة هندسة الإنتاج والتصميم الميكانيكي", FullCapacity = 2, SelectionStatus = SelectionStatus.رابعة_ميكانيكا });
            modelBuilder.Entity<Branch>().HasData(new Branch { BranchName = "الهندسة الصناعية", DepartmentName = "الهندسة الميكانيكية", BranchDescription = "وصف شعبة الهندسة الصناعية", FullCapacity = 2, SelectionStatus = SelectionStatus.رابعة_ميكانيكا });
            modelBuilder.Entity<Branch>().HasData(new Branch { BranchName = "هندسة القوى الميكانيكية", DepartmentName = "الهندسة الميكانيكية", BranchDescription = "وصف شعبة هندسة القوى الميكانيكية", FullCapacity = 2, SelectionStatus = SelectionStatus.رابعة_ميكانيكا });


            modelBuilder.Entity<DepartmentCode>().HasData(new DepartmentCode { DepartmentCodeValue = "ريض", DepartmentName = "الرياضيات والفيزيقا الهندسية" });
            modelBuilder.Entity<DepartmentCode>().HasData(new DepartmentCode { DepartmentCodeValue = "فيز", DepartmentName = "الرياضيات والفيزيقا الهندسية" });
            modelBuilder.Entity<DepartmentCode>().HasData(new DepartmentCode { DepartmentCodeValue = "ميك", DepartmentName = "الرياضيات والفيزيقا الهندسية" });
            modelBuilder.Entity<DepartmentCode>().HasData(new DepartmentCode { DepartmentCodeValue = "عام", DepartmentName = "الرياضيات والفيزيقا الهندسية" });
            modelBuilder.Entity<DepartmentCode>().HasData(new DepartmentCode { DepartmentCodeValue = "هند", DepartmentName = "الرياضيات والفيزيقا الهندسية" });
            modelBuilder.Entity<DepartmentCode>().HasData(new DepartmentCode { DepartmentCodeValue = "مدن", DepartmentName = "الهندسة المدنية" });
            modelBuilder.Entity<DepartmentCode>().HasData(new DepartmentCode { DepartmentCodeValue = "عمر", DepartmentName = "الهندسة المعمارية" });
            modelBuilder.Entity<DepartmentCode>().HasData(new DepartmentCode { DepartmentCodeValue = "كھع", DepartmentName = "الهندسة الكهربية" });
            modelBuilder.Entity<DepartmentCode>().HasData(new DepartmentCode { DepartmentCodeValue = "كهق", DepartmentName = "الهندسة الكهربية" });
            modelBuilder.Entity<DepartmentCode>().HasData(new DepartmentCode { DepartmentCodeValue = "كهت", DepartmentName = "الهندسة الكهربية" });
            modelBuilder.Entity<DepartmentCode>().HasData(new DepartmentCode { DepartmentCodeValue = "كهح", DepartmentName = "الهندسة الكهربية" });
            modelBuilder.Entity<DepartmentCode>().HasData(new DepartmentCode { DepartmentCodeValue = "تمج", DepartmentName = "الهندسة الميكانيكية" });
            modelBuilder.Entity<DepartmentCode>().HasData(new DepartmentCode { DepartmentCodeValue = "صنع", DepartmentName = "الهندسة الميكانيكية" });
            modelBuilder.Entity<DepartmentCode>().HasData(new DepartmentCode { DepartmentCodeValue = "قوى", DepartmentName = "الهندسة الميكانيكية" });

            //courses

            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 1", CourseCode = "1", DepartmentCodeValue = "ريض" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 2", CourseCode = "2", DepartmentCodeValue = "فيز" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 3", CourseCode = "3", DepartmentCodeValue = "ميك" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 4", CourseCode = "4", DepartmentCodeValue = "عام" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 5", CourseCode = "5", DepartmentCodeValue = "هند" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 6", CourseCode = "6", DepartmentCodeValue = "مدن" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 7", CourseCode = "7", DepartmentCodeValue = "كھع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 8", CourseCode = "8", DepartmentCodeValue = "كهق" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 9", CourseCode = "9", DepartmentCodeValue = "كهت" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 10", CourseCode = "10", DepartmentCodeValue = "كهح" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 11", CourseCode = "11", DepartmentCodeValue = "قوى" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 12", CourseCode = "12", DepartmentCodeValue = "تمج" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 13", CourseCode = "13", DepartmentCodeValue = "صنع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 14", CourseCode = "14", DepartmentCodeValue = "عمر" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 15", CourseCode = "15", DepartmentCodeValue = "ريض" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 16", CourseCode = "16", DepartmentCodeValue = "فيز" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 17", CourseCode = "17", DepartmentCodeValue = "ميك" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 18", CourseCode = "18", DepartmentCodeValue = "عام" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 19", CourseCode = "19", DepartmentCodeValue = "هند" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 20", CourseCode = "20", DepartmentCodeValue = "مدن" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 21", CourseCode = "21", DepartmentCodeValue = "كھع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 22", CourseCode = "22", DepartmentCodeValue = "كهق" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 23", CourseCode = "23", DepartmentCodeValue = "كهت" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 24", CourseCode = "24", DepartmentCodeValue = "كهح" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 25", CourseCode = "25", DepartmentCodeValue = "قوى" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 26", CourseCode = "26", DepartmentCodeValue = "تمج" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 27", CourseCode = "27", DepartmentCodeValue = "صنع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 28", CourseCode = "28", DepartmentCodeValue = "عمر" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 29", CourseCode = "29", DepartmentCodeValue = "ريض" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 30", CourseCode = "30", DepartmentCodeValue = "فيز" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 31", CourseCode = "31", DepartmentCodeValue = "ميك" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 32", CourseCode = "32", DepartmentCodeValue = "عام" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 33", CourseCode = "33", DepartmentCodeValue = "هند" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 34", CourseCode = "34", DepartmentCodeValue = "مدن" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 35", CourseCode = "35", DepartmentCodeValue = "كھع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 36", CourseCode = "36", DepartmentCodeValue = "كهق" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 37", CourseCode = "37", DepartmentCodeValue = "كهت" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 38", CourseCode = "38", DepartmentCodeValue = "كهح" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 39", CourseCode = "39", DepartmentCodeValue = "قوى" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 40", CourseCode = "40", DepartmentCodeValue = "تمج" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 41", CourseCode = "41", DepartmentCodeValue = "صنع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 42", CourseCode = "42", DepartmentCodeValue = "عمر" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 43", CourseCode = "43", DepartmentCodeValue = "ريض" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 44", CourseCode = "44", DepartmentCodeValue = "فيز" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 45", CourseCode = "45", DepartmentCodeValue = "ميك" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 46", CourseCode = "46", DepartmentCodeValue = "عام" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 47", CourseCode = "47", DepartmentCodeValue = "هند" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 48", CourseCode = "48", DepartmentCodeValue = "مدن" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 49", CourseCode = "49", DepartmentCodeValue = "كھع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 50", CourseCode = "50", DepartmentCodeValue = "كهق" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 51", CourseCode = "51", DepartmentCodeValue = "كهت" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 52", CourseCode = "52", DepartmentCodeValue = "كهح" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 53", CourseCode = "53", DepartmentCodeValue = "قوى" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 54", CourseCode = "54", DepartmentCodeValue = "تمج" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 55", CourseCode = "55", DepartmentCodeValue = "صنع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 56", CourseCode = "56", DepartmentCodeValue = "عمر" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 57", CourseCode = "57", DepartmentCodeValue = "ريض" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 58", CourseCode = "58", DepartmentCodeValue = "فيز" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 59", CourseCode = "59", DepartmentCodeValue = "ميك" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 60", CourseCode = "60", DepartmentCodeValue = "عام" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 61", CourseCode = "61", DepartmentCodeValue = "هند" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 62", CourseCode = "62", DepartmentCodeValue = "مدن" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 63", CourseCode = "63", DepartmentCodeValue = "كھع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 64", CourseCode = "64", DepartmentCodeValue = "كهق" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 65", CourseCode = "65", DepartmentCodeValue = "كهت" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 66", CourseCode = "66", DepartmentCodeValue = "كهح" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 67", CourseCode = "67", DepartmentCodeValue = "قوى" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 68", CourseCode = "68", DepartmentCodeValue = "تمج" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 69", CourseCode = "69", DepartmentCodeValue = "صنع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 70", CourseCode = "70", DepartmentCodeValue = "عمر" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 71", CourseCode = "71", DepartmentCodeValue = "ريض" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 72", CourseCode = "72", DepartmentCodeValue = "فيز" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 73", CourseCode = "73", DepartmentCodeValue = "ميك" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 74", CourseCode = "74", DepartmentCodeValue = "عام" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 75", CourseCode = "75", DepartmentCodeValue = "هند" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 76", CourseCode = "76", DepartmentCodeValue = "مدن" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 77", CourseCode = "77", DepartmentCodeValue = "كھع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 78", CourseCode = "78", DepartmentCodeValue = "كهق" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 79", CourseCode = "79", DepartmentCodeValue = "كهت" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 80", CourseCode = "80", DepartmentCodeValue = "كهح" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 81", CourseCode = "81", DepartmentCodeValue = "قوى" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 82", CourseCode = "82", DepartmentCodeValue = "تمج" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 83", CourseCode = "83", DepartmentCodeValue = "صنع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 84", CourseCode = "84", DepartmentCodeValue = "عمر" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 85", CourseCode = "85", DepartmentCodeValue = "ريض" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 86", CourseCode = "86", DepartmentCodeValue = "فيز" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 87", CourseCode = "87", DepartmentCodeValue = "ميك" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 88", CourseCode = "88", DepartmentCodeValue = "عام" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 89", CourseCode = "89", DepartmentCodeValue = "هند" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 90", CourseCode = "90", DepartmentCodeValue = "مدن" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 91", CourseCode = "91", DepartmentCodeValue = "كھع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 92", CourseCode = "92", DepartmentCodeValue = "كهق" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 93", CourseCode = "93", DepartmentCodeValue = "كهت" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 94", CourseCode = "94", DepartmentCodeValue = "كهح" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 95", CourseCode = "95", DepartmentCodeValue = "قوى" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 96", CourseCode = "96", DepartmentCodeValue = "تمج" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 97", CourseCode = "97", DepartmentCodeValue = "صنع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 98", CourseCode = "98", DepartmentCodeValue = "عمر" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 99", CourseCode = "99", DepartmentCodeValue = "ريض" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 100", CourseCode = "100", DepartmentCodeValue = "فيز" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 101", CourseCode = "101", DepartmentCodeValue = "ميك" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 102", CourseCode = "102", DepartmentCodeValue = "عام" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 103", CourseCode = "103", DepartmentCodeValue = "هند" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 104", CourseCode = "104", DepartmentCodeValue = "مدن" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 105", CourseCode = "105", DepartmentCodeValue = "كھع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 106", CourseCode = "106", DepartmentCodeValue = "كهق" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 107", CourseCode = "107", DepartmentCodeValue = "كهت" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 108", CourseCode = "108", DepartmentCodeValue = "كهح" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 109", CourseCode = "109", DepartmentCodeValue = "قوى" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 110", CourseCode = "110", DepartmentCodeValue = "تمج" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 111", CourseCode = "111", DepartmentCodeValue = "صنع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 112", CourseCode = "112", DepartmentCodeValue = "عمر" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 113", CourseCode = "113", DepartmentCodeValue = "ريض" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 114", CourseCode = "114", DepartmentCodeValue = "فيز" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 115", CourseCode = "115", DepartmentCodeValue = "ميك" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 116", CourseCode = "116", DepartmentCodeValue = "عام" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 117", CourseCode = "117", DepartmentCodeValue = "هند" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 118", CourseCode = "118", DepartmentCodeValue = "مدن" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 119", CourseCode = "119", DepartmentCodeValue = "كھع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 120", CourseCode = "120", DepartmentCodeValue = "كهق" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 121", CourseCode = "121", DepartmentCodeValue = "كهت" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 122", CourseCode = "122", DepartmentCodeValue = "كهح" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 123", CourseCode = "123", DepartmentCodeValue = "قوى" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 124", CourseCode = "124", DepartmentCodeValue = "تمج" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 125", CourseCode = "125", DepartmentCodeValue = "صنع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 126", CourseCode = "126", DepartmentCodeValue = "عمر" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 127", CourseCode = "127", DepartmentCodeValue = "ريض" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 128", CourseCode = "128", DepartmentCodeValue = "فيز" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 129", CourseCode = "129", DepartmentCodeValue = "ميك" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 130", CourseCode = "130", DepartmentCodeValue = "عام" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 131", CourseCode = "131", DepartmentCodeValue = "هند" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 132", CourseCode = "132", DepartmentCodeValue = "مدن" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 133", CourseCode = "133", DepartmentCodeValue = "كھع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 134", CourseCode = "134", DepartmentCodeValue = "كهق" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 135", CourseCode = "135", DepartmentCodeValue = "كهت" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 136", CourseCode = "136", DepartmentCodeValue = "كهح" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 137", CourseCode = "137", DepartmentCodeValue = "قوى" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 138", CourseCode = "138", DepartmentCodeValue = "تمج" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 139", CourseCode = "139", DepartmentCodeValue = "صنع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 140", CourseCode = "140", DepartmentCodeValue = "عمر" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 141", CourseCode = "141", DepartmentCodeValue = "ريض" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 142", CourseCode = "142", DepartmentCodeValue = "فيز" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 143", CourseCode = "143", DepartmentCodeValue = "ميك" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 144", CourseCode = "144", DepartmentCodeValue = "عام" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 145", CourseCode = "145", DepartmentCodeValue = "هند" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 146", CourseCode = "146", DepartmentCodeValue = "مدن" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 147", CourseCode = "147", DepartmentCodeValue = "كھع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 148", CourseCode = "148", DepartmentCodeValue = "كهق" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 149", CourseCode = "149", DepartmentCodeValue = "كهت" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 150", CourseCode = "150", DepartmentCodeValue = "كهح" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 151", CourseCode = "151", DepartmentCodeValue = "قوى" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 152", CourseCode = "152", DepartmentCodeValue = "تمج" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 153", CourseCode = "153", DepartmentCodeValue = "صنع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 154", CourseCode = "154", DepartmentCodeValue = "عمر" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 155", CourseCode = "155", DepartmentCodeValue = "ريض" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 156", CourseCode = "156", DepartmentCodeValue = "فيز" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 157", CourseCode = "157", DepartmentCodeValue = "ميك" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 158", CourseCode = "158", DepartmentCodeValue = "عام" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 159", CourseCode = "159", DepartmentCodeValue = "هند" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 160", CourseCode = "160", DepartmentCodeValue = "مدن" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 161", CourseCode = "161", DepartmentCodeValue = "كھع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 162", CourseCode = "162", DepartmentCodeValue = "كهق" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 163", CourseCode = "163", DepartmentCodeValue = "كهت" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 164", CourseCode = "164", DepartmentCodeValue = "كهح" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 165", CourseCode = "165", DepartmentCodeValue = "قوى" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 166", CourseCode = "166", DepartmentCodeValue = "تمج" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 167", CourseCode = "167", DepartmentCodeValue = "صنع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 168", CourseCode = "168", DepartmentCodeValue = "عمر" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 169", CourseCode = "169", DepartmentCodeValue = "ريض" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 170", CourseCode = "170", DepartmentCodeValue = "فيز" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 171", CourseCode = "171", DepartmentCodeValue = "ميك" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 172", CourseCode = "172", DepartmentCodeValue = "عام" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 173", CourseCode = "173", DepartmentCodeValue = "هند" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 174", CourseCode = "174", DepartmentCodeValue = "مدن" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 175", CourseCode = "175", DepartmentCodeValue = "كھع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 176", CourseCode = "176", DepartmentCodeValue = "كهق" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 177", CourseCode = "177", DepartmentCodeValue = "كهت" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 178", CourseCode = "178", DepartmentCodeValue = "كهح" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 179", CourseCode = "179", DepartmentCodeValue = "قوى" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 180", CourseCode = "180", DepartmentCodeValue = "تمج" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 181", CourseCode = "181", DepartmentCodeValue = "صنع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 182", CourseCode = "182", DepartmentCodeValue = "عمر" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 183", CourseCode = "183", DepartmentCodeValue = "ريض" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 184", CourseCode = "184", DepartmentCodeValue = "فيز" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 185", CourseCode = "185", DepartmentCodeValue = "ميك" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 186", CourseCode = "186", DepartmentCodeValue = "عام" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 187", CourseCode = "187", DepartmentCodeValue = "هند" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 188", CourseCode = "188", DepartmentCodeValue = "مدن" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 189", CourseCode = "189", DepartmentCodeValue = "كھع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 190", CourseCode = "190", DepartmentCodeValue = "كهق" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 191", CourseCode = "191", DepartmentCodeValue = "كهت" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 192", CourseCode = "192", DepartmentCodeValue = "كهح" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 193", CourseCode = "193", DepartmentCodeValue = "قوى" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 194", CourseCode = "194", DepartmentCodeValue = "تمج" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 195", CourseCode = "195", DepartmentCodeValue = "صنع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 196", CourseCode = "196", DepartmentCodeValue = "عمر" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 197", CourseCode = "197", DepartmentCodeValue = "ريض" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 198", CourseCode = "198", DepartmentCodeValue = "فيز" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 199", CourseCode = "199", DepartmentCodeValue = "ميك" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 200", CourseCode = "200", DepartmentCodeValue = "عام" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 201", CourseCode = "201", DepartmentCodeValue = "هند" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 202", CourseCode = "202", DepartmentCodeValue = "مدن" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 203", CourseCode = "203", DepartmentCodeValue = "كھع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 204", CourseCode = "204", DepartmentCodeValue = "كهق" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 205", CourseCode = "205", DepartmentCodeValue = "كهت" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 206", CourseCode = "206", DepartmentCodeValue = "كهح" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 207", CourseCode = "207", DepartmentCodeValue = "قوى" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 208", CourseCode = "208", DepartmentCodeValue = "تمج" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 209", CourseCode = "209", DepartmentCodeValue = "صنع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 210", CourseCode = "210", DepartmentCodeValue = "عمر" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 211", CourseCode = "211", DepartmentCodeValue = "ريض" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 212", CourseCode = "212", DepartmentCodeValue = "فيز" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 213", CourseCode = "213", DepartmentCodeValue = "ميك" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 214", CourseCode = "214", DepartmentCodeValue = "عام" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 215", CourseCode = "215", DepartmentCodeValue = "هند" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 216", CourseCode = "216", DepartmentCodeValue = "مدن" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 217", CourseCode = "217", DepartmentCodeValue = "كھع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 218", CourseCode = "218", DepartmentCodeValue = "كهق" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 219", CourseCode = "219", DepartmentCodeValue = "كهت" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 220", CourseCode = "220", DepartmentCodeValue = "كهح" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 221", CourseCode = "221", DepartmentCodeValue = "قوى" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 222", CourseCode = "222", DepartmentCodeValue = "تمج" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 223", CourseCode = "223", DepartmentCodeValue = "صنع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 224", CourseCode = "224", DepartmentCodeValue = "عمر" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 225", CourseCode = "225", DepartmentCodeValue = "ريض" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 226", CourseCode = "226", DepartmentCodeValue = "فيز" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 227", CourseCode = "227", DepartmentCodeValue = "ميك" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 228", CourseCode = "228", DepartmentCodeValue = "عام" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 229", CourseCode = "229", DepartmentCodeValue = "هند" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 230", CourseCode = "230", DepartmentCodeValue = "مدن" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 231", CourseCode = "231", DepartmentCodeValue = "كھع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 232", CourseCode = "232", DepartmentCodeValue = "كهق" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 233", CourseCode = "233", DepartmentCodeValue = "كهت" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 234", CourseCode = "234", DepartmentCodeValue = "كهح" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 235", CourseCode = "235", DepartmentCodeValue = "قوى" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 236", CourseCode = "236", DepartmentCodeValue = "تمج" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 237", CourseCode = "237", DepartmentCodeValue = "صنع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 238", CourseCode = "238", DepartmentCodeValue = "عمر" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 239", CourseCode = "239", DepartmentCodeValue = "ريض" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 240", CourseCode = "240", DepartmentCodeValue = "فيز" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 241", CourseCode = "241", DepartmentCodeValue = "ميك" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 242", CourseCode = "242", DepartmentCodeValue = "عام" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 243", CourseCode = "243", DepartmentCodeValue = "هند" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 244", CourseCode = "244", DepartmentCodeValue = "مدن" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 245", CourseCode = "245", DepartmentCodeValue = "كھع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 246", CourseCode = "246", DepartmentCodeValue = "كهق" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 247", CourseCode = "247", DepartmentCodeValue = "كهت" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 248", CourseCode = "248", DepartmentCodeValue = "كهح" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 249", CourseCode = "249", DepartmentCodeValue = "قوى" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 250", CourseCode = "250", DepartmentCodeValue = "تمج" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 251", CourseCode = "251", DepartmentCodeValue = "صنع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 252", CourseCode = "252", DepartmentCodeValue = "عمر" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 253", CourseCode = "253", DepartmentCodeValue = "ريض" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 254", CourseCode = "254", DepartmentCodeValue = "فيز" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 255", CourseCode = "255", DepartmentCodeValue = "ميك" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 256", CourseCode = "256", DepartmentCodeValue = "عام" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 257", CourseCode = "257", DepartmentCodeValue = "هند" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 258", CourseCode = "258", DepartmentCodeValue = "مدن" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 259", CourseCode = "259", DepartmentCodeValue = "كھع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 260", CourseCode = "260", DepartmentCodeValue = "كهق" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 261", CourseCode = "261", DepartmentCodeValue = "كهت" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 262", CourseCode = "262", DepartmentCodeValue = "كهح" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 263", CourseCode = "263", DepartmentCodeValue = "قوى" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 264", CourseCode = "264", DepartmentCodeValue = "تمج" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 265", CourseCode = "265", DepartmentCodeValue = "صنع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 266", CourseCode = "266", DepartmentCodeValue = "عمر" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 267", CourseCode = "267", DepartmentCodeValue = "ريض" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 268", CourseCode = "268", DepartmentCodeValue = "فيز" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 269", CourseCode = "269", DepartmentCodeValue = "ميك" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 270", CourseCode = "270", DepartmentCodeValue = "عام" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 271", CourseCode = "271", DepartmentCodeValue = "هند" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 272", CourseCode = "272", DepartmentCodeValue = "مدن" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 273", CourseCode = "273", DepartmentCodeValue = "كھع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 274", CourseCode = "274", DepartmentCodeValue = "كهق" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 275", CourseCode = "275", DepartmentCodeValue = "كهت" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 276", CourseCode = "276", DepartmentCodeValue = "كهح" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 277", CourseCode = "277", DepartmentCodeValue = "قوى" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 278", CourseCode = "278", DepartmentCodeValue = "تمج" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 279", CourseCode = "279", DepartmentCodeValue = "صنع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 280", CourseCode = "280", DepartmentCodeValue = "عمر" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 281", CourseCode = "281", DepartmentCodeValue = "ريض" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 282", CourseCode = "282", DepartmentCodeValue = "فيز" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 283", CourseCode = "283", DepartmentCodeValue = "ميك" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 284", CourseCode = "284", DepartmentCodeValue = "عام" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 285", CourseCode = "285", DepartmentCodeValue = "هند" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 286", CourseCode = "286", DepartmentCodeValue = "مدن" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 287", CourseCode = "287", DepartmentCodeValue = "كھع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 288", CourseCode = "288", DepartmentCodeValue = "كهق" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 289", CourseCode = "289", DepartmentCodeValue = "كهت" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 290", CourseCode = "290", DepartmentCodeValue = "كهح" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 291", CourseCode = "291", DepartmentCodeValue = "قوى" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 292", CourseCode = "292", DepartmentCodeValue = "تمج" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 293", CourseCode = "293", DepartmentCodeValue = "صنع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 294", CourseCode = "294", DepartmentCodeValue = "عمر" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 295", CourseCode = "295", DepartmentCodeValue = "ريض" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 296", CourseCode = "296", DepartmentCodeValue = "فيز" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 297", CourseCode = "297", DepartmentCodeValue = "ميك" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 298", CourseCode = "298", DepartmentCodeValue = "عام" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 299", CourseCode = "299", DepartmentCodeValue = "هند" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 300", CourseCode = "300", DepartmentCodeValue = "مدن" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 301", CourseCode = "301", DepartmentCodeValue = "كھع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 302", CourseCode = "302", DepartmentCodeValue = "كهق" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 303", CourseCode = "303", DepartmentCodeValue = "كهت" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 304", CourseCode = "304", DepartmentCodeValue = "كهح" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 305", CourseCode = "305", DepartmentCodeValue = "قوى" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 306", CourseCode = "306", DepartmentCodeValue = "تمج" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 307", CourseCode = "307", DepartmentCodeValue = "صنع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 308", CourseCode = "308", DepartmentCodeValue = "عمر" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 309", CourseCode = "309", DepartmentCodeValue = "ريض" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 310", CourseCode = "310", DepartmentCodeValue = "فيز" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 311", CourseCode = "311", DepartmentCodeValue = "ميك" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 312", CourseCode = "312", DepartmentCodeValue = "عام" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 313", CourseCode = "313", DepartmentCodeValue = "هند" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 314", CourseCode = "314", DepartmentCodeValue = "مدن" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 315", CourseCode = "315", DepartmentCodeValue = "كھع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 316", CourseCode = "316", DepartmentCodeValue = "كهق" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 317", CourseCode = "317", DepartmentCodeValue = "كهت" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 318", CourseCode = "318", DepartmentCodeValue = "كهح" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 319", CourseCode = "319", DepartmentCodeValue = "قوى" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 320", CourseCode = "320", DepartmentCodeValue = "تمج" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 321", CourseCode = "321", DepartmentCodeValue = "صنع" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 322", CourseCode = "322", DepartmentCodeValue = "عمر" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 323", CourseCode = "323", DepartmentCodeValue = "ريض" });
            modelBuilder.Entity<Course>().HasData(new Course { CourseName = "CourseName 324", CourseCode = "324", DepartmentCodeValue = "فيز" });

            //courseEnrollments:
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 1, CourseName = "CourseName 1", LevelName = "الإعدادية", BranchName = "الرياضيات والفيزيقا الهندسية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 2, CourseName = "CourseName 2", LevelName = "الإعدادية", BranchName = "الرياضيات والفيزيقا الهندسية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 3, CourseName = "CourseName 3", LevelName = "الإعدادية", BranchName = "الرياضيات والفيزيقا الهندسية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 4, CourseName = "CourseName 4", LevelName = "الإعدادية", BranchName = "الرياضيات والفيزيقا الهندسية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 5, CourseName = "CourseName 5", LevelName = "الإعدادية", BranchName = "الرياضيات والفيزيقا الهندسية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 6, CourseName = "CourseName 6", LevelName = "الإعدادية", BranchName = "الرياضيات والفيزيقا الهندسية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 7, CourseName = "CourseName 7", LevelName = "الإعدادية", BranchName = "الرياضيات والفيزيقا الهندسية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 8, CourseName = "CourseName 8", LevelName = "الإعدادية", BranchName = "الرياضيات والفيزيقا الهندسية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 9, CourseName = "CourseName 9", LevelName = "الإعدادية", BranchName = "الرياضيات والفيزيقا الهندسية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 10, CourseName = "CourseName 10", LevelName = "الإعدادية", BranchName = "الرياضيات والفيزيقا الهندسية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 11, CourseName = "CourseName 11", LevelName = "الإعدادية", BranchName = "الرياضيات والفيزيقا الهندسية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 12, CourseName = "CourseName 12", LevelName = "الإعدادية", BranchName = "الرياضيات والفيزيقا الهندسية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 13, CourseName = "CourseName 13", LevelName = "الأولى", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 14, CourseName = "CourseName 14", LevelName = "الأولى", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 15, CourseName = "CourseName 15", LevelName = "الأولى", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 16, CourseName = "CourseName 16", LevelName = "الأولى", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 17, CourseName = "CourseName 17", LevelName = "الأولى", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 18, CourseName = "CourseName 18", LevelName = "الأولى", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 19, CourseName = "CourseName 19", LevelName = "الأولى", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 20, CourseName = "CourseName 20", LevelName = "الأولى", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 21, CourseName = "CourseName 21", LevelName = "الأولى", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 22, CourseName = "CourseName 22", LevelName = "الأولى", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 23, CourseName = "CourseName 23", LevelName = "الأولى", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 24, CourseName = "CourseName 24", LevelName = "الأولى", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 25, CourseName = "CourseName 25", LevelName = "الثانية", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 26, CourseName = "CourseName 26", LevelName = "الثانية", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 27, CourseName = "CourseName 27", LevelName = "الثانية", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 28, CourseName = "CourseName 28", LevelName = "الثانية", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 29, CourseName = "CourseName 29", LevelName = "الثانية", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 30, CourseName = "CourseName 30", LevelName = "الثانية", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 31, CourseName = "CourseName 31", LevelName = "الثانية", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 32, CourseName = "CourseName 32", LevelName = "الثانية", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 33, CourseName = "CourseName 33", LevelName = "الثانية", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 34, CourseName = "CourseName 34", LevelName = "الثانية", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 35, CourseName = "CourseName 35", LevelName = "الثانية", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 36, CourseName = "CourseName 36", LevelName = "الثانية", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 37, CourseName = "CourseName 37", LevelName = "الثالثة", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 38, CourseName = "CourseName 38", LevelName = "الثالثة", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 39, CourseName = "CourseName 39", LevelName = "الثالثة", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 40, CourseName = "CourseName 40", LevelName = "الثالثة", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 41, CourseName = "CourseName 41", LevelName = "الثالثة", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 42, CourseName = "CourseName 42", LevelName = "الثالثة", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 43, CourseName = "CourseName 43", LevelName = "الثالثة", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 44, CourseName = "CourseName 44", LevelName = "الثالثة", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 45, CourseName = "CourseName 45", LevelName = "الثالثة", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 46, CourseName = "CourseName 46", LevelName = "الثالثة", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 47, CourseName = "CourseName 47", LevelName = "الثالثة", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 48, CourseName = "CourseName 48", LevelName = "الثالثة", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 49, CourseName = "CourseName 49", LevelName = "الأولى", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 50, CourseName = "CourseName 50", LevelName = "الأولى", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 51, CourseName = "CourseName 51", LevelName = "الأولى", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 52, CourseName = "CourseName 52", LevelName = "الأولى", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 53, CourseName = "CourseName 53", LevelName = "الأولى", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 54, CourseName = "CourseName 54", LevelName = "الأولى", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 55, CourseName = "CourseName 55", LevelName = "الأولى", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 56, CourseName = "CourseName 56", LevelName = "الأولى", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 57, CourseName = "CourseName 57", LevelName = "الأولى", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 58, CourseName = "CourseName 58", LevelName = "الأولى", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 59, CourseName = "CourseName 59", LevelName = "الأولى", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 60, CourseName = "CourseName 60", LevelName = "الأولى", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 61, CourseName = "CourseName 61", LevelName = "الثانية", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 62, CourseName = "CourseName 62", LevelName = "الثانية", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 63, CourseName = "CourseName 63", LevelName = "الثانية", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 64, CourseName = "CourseName 64", LevelName = "الثانية", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 65, CourseName = "CourseName 65", LevelName = "الثانية", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 66, CourseName = "CourseName 66", LevelName = "الثانية", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 67, CourseName = "CourseName 67", LevelName = "الثانية", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 68, CourseName = "CourseName 68", LevelName = "الثانية", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 69, CourseName = "CourseName 69", LevelName = "الثانية", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 70, CourseName = "CourseName 70", LevelName = "الثانية", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 71, CourseName = "CourseName 71", LevelName = "الثانية", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 72, CourseName = "CourseName 72", LevelName = "الثانية", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 73, CourseName = "CourseName 73", LevelName = "الثالثة", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 74, CourseName = "CourseName 74", LevelName = "الثالثة", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 75, CourseName = "CourseName 75", LevelName = "الثالثة", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 76, CourseName = "CourseName 76", LevelName = "الثالثة", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 77, CourseName = "CourseName 77", LevelName = "الثالثة", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 78, CourseName = "CourseName 78", LevelName = "الثالثة", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 79, CourseName = "CourseName 79", LevelName = "الثالثة", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 80, CourseName = "CourseName 80", LevelName = "الثالثة", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 81, CourseName = "CourseName 81", LevelName = "الثالثة", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 82, CourseName = "CourseName 82", LevelName = "الثالثة", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 83, CourseName = "CourseName 83", LevelName = "الثالثة", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 84, CourseName = "CourseName 84", LevelName = "الثالثة", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 85, CourseName = "CourseName 85", LevelName = "الأولى", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 86, CourseName = "CourseName 86", LevelName = "الأولى", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 87, CourseName = "CourseName 87", LevelName = "الأولى", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 88, CourseName = "CourseName 88", LevelName = "الأولى", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 89, CourseName = "CourseName 89", LevelName = "الأولى", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 90, CourseName = "CourseName 90", LevelName = "الأولى", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 91, CourseName = "CourseName 91", LevelName = "الأولى", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 92, CourseName = "CourseName 92", LevelName = "الأولى", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 93, CourseName = "CourseName 93", LevelName = "الأولى", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 94, CourseName = "CourseName 94", LevelName = "الأولى", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 95, CourseName = "CourseName 95", LevelName = "الأولى", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 96, CourseName = "CourseName 96", LevelName = "الأولى", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 97, CourseName = "CourseName 97", LevelName = "الثانية", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 98, CourseName = "CourseName 98", LevelName = "الثانية", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 99, CourseName = "CourseName 99", LevelName = "الثانية", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 100, CourseName = "CourseName 100", LevelName = "الثانية", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 101, CourseName = "CourseName 101", LevelName = "الثانية", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 102, CourseName = "CourseName 102", LevelName = "الثانية", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 103, CourseName = "CourseName 103", LevelName = "الثانية", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 104, CourseName = "CourseName 104", LevelName = "الثانية", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 105, CourseName = "CourseName 105", LevelName = "الثانية", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 106, CourseName = "CourseName 106", LevelName = "الثانية", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 107, CourseName = "CourseName 107", LevelName = "الثانية", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 108, CourseName = "CourseName 108", LevelName = "الثانية", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 109, CourseName = "CourseName 109", LevelName = "الثالثة", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 110, CourseName = "CourseName 110", LevelName = "الثالثة", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 111, CourseName = "CourseName 111", LevelName = "الثالثة", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 112, CourseName = "CourseName 112", LevelName = "الثالثة", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 113, CourseName = "CourseName 113", LevelName = "الثالثة", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 114, CourseName = "CourseName 114", LevelName = "الثالثة", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 115, CourseName = "CourseName 115", LevelName = "الثالثة", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 116, CourseName = "CourseName 116", LevelName = "الثالثة", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 117, CourseName = "CourseName 117", LevelName = "الثالثة", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 118, CourseName = "CourseName 118", LevelName = "الثالثة", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 119, CourseName = "CourseName 119", LevelName = "الثالثة", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 120, CourseName = "CourseName 120", LevelName = "الثالثة", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 121, CourseName = "CourseName 121", LevelName = "الأولى", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 122, CourseName = "CourseName 122", LevelName = "الأولى", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 123, CourseName = "CourseName 123", LevelName = "الأولى", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 124, CourseName = "CourseName 124", LevelName = "الأولى", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 125, CourseName = "CourseName 125", LevelName = "الأولى", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 126, CourseName = "CourseName 126", LevelName = "الأولى", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 127, CourseName = "CourseName 127", LevelName = "الأولى", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 128, CourseName = "CourseName 128", LevelName = "الأولى", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 129, CourseName = "CourseName 129", LevelName = "الأولى", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 130, CourseName = "CourseName 130", LevelName = "الأولى", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 131, CourseName = "CourseName 131", LevelName = "الأولى", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 132, CourseName = "CourseName 132", LevelName = "الأولى", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 133, CourseName = "CourseName 133", LevelName = "الثانية", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 134, CourseName = "CourseName 134", LevelName = "الثانية", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 135, CourseName = "CourseName 135", LevelName = "الثانية", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 136, CourseName = "CourseName 136", LevelName = "الثانية", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 137, CourseName = "CourseName 137", LevelName = "الثانية", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 138, CourseName = "CourseName 138", LevelName = "الثانية", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 139, CourseName = "CourseName 139", LevelName = "الثانية", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 140, CourseName = "CourseName 140", LevelName = "الثانية", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 141, CourseName = "CourseName 141", LevelName = "الثانية", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 142, CourseName = "CourseName 142", LevelName = "الثانية", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 143, CourseName = "CourseName 143", LevelName = "الثانية", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 144, CourseName = "CourseName 144", LevelName = "الثانية", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 145, CourseName = "CourseName 145", LevelName = "الثالثة", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 146, CourseName = "CourseName 146", LevelName = "الثالثة", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 147, CourseName = "CourseName 147", LevelName = "الثالثة", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 148, CourseName = "CourseName 148", LevelName = "الثالثة", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 149, CourseName = "CourseName 149", LevelName = "الثالثة", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 150, CourseName = "CourseName 150", LevelName = "الثالثة", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 151, CourseName = "CourseName 151", LevelName = "الثالثة", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 152, CourseName = "CourseName 152", LevelName = "الثالثة", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 153, CourseName = "CourseName 153", LevelName = "الثالثة", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 154, CourseName = "CourseName 154", LevelName = "الثالثة", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 155, CourseName = "CourseName 155", LevelName = "الثالثة", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 156, CourseName = "CourseName 156", LevelName = "الثالثة", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 157, CourseName = "CourseName 157", LevelName = "الأولى", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 158, CourseName = "CourseName 158", LevelName = "الأولى", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 159, CourseName = "CourseName 159", LevelName = "الأولى", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 160, CourseName = "CourseName 160", LevelName = "الأولى", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 161, CourseName = "CourseName 161", LevelName = "الأولى", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 162, CourseName = "CourseName 162", LevelName = "الأولى", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 163, CourseName = "CourseName 163", LevelName = "الأولى", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 164, CourseName = "CourseName 164", LevelName = "الأولى", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 165, CourseName = "CourseName 165", LevelName = "الأولى", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 166, CourseName = "CourseName 166", LevelName = "الأولى", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 167, CourseName = "CourseName 167", LevelName = "الأولى", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 168, CourseName = "CourseName 168", LevelName = "الأولى", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 169, CourseName = "CourseName 169", LevelName = "الثانية", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 170, CourseName = "CourseName 170", LevelName = "الثانية", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 171, CourseName = "CourseName 171", LevelName = "الثانية", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 172, CourseName = "CourseName 172", LevelName = "الثانية", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 173, CourseName = "CourseName 173", LevelName = "الثانية", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 174, CourseName = "CourseName 174", LevelName = "الثانية", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 175, CourseName = "CourseName 175", LevelName = "الثانية", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 176, CourseName = "CourseName 176", LevelName = "الثانية", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 177, CourseName = "CourseName 177", LevelName = "الثانية", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 178, CourseName = "CourseName 178", LevelName = "الثانية", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 179, CourseName = "CourseName 179", LevelName = "الثانية", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 180, CourseName = "CourseName 180", LevelName = "الثانية", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 181, CourseName = "CourseName 181", LevelName = "الثالثة", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 182, CourseName = "CourseName 182", LevelName = "الثالثة", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 183, CourseName = "CourseName 183", LevelName = "الثالثة", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 184, CourseName = "CourseName 184", LevelName = "الثالثة", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 185, CourseName = "CourseName 185", LevelName = "الثالثة", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 186, CourseName = "CourseName 186", LevelName = "الثالثة", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 187, CourseName = "CourseName 187", LevelName = "الثالثة", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 188, CourseName = "CourseName 188", LevelName = "الثالثة", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 189, CourseName = "CourseName 189", LevelName = "الثالثة", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 190, CourseName = "CourseName 190", LevelName = "الثالثة", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 191, CourseName = "CourseName 191", LevelName = "الثالثة", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 192, CourseName = "CourseName 192", LevelName = "الثالثة", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 193, CourseName = "CourseName 193", LevelName = "الأولى", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 194, CourseName = "CourseName 194", LevelName = "الأولى", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 195, CourseName = "CourseName 195", LevelName = "الأولى", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 196, CourseName = "CourseName 196", LevelName = "الأولى", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 197, CourseName = "CourseName 197", LevelName = "الأولى", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 198, CourseName = "CourseName 198", LevelName = "الأولى", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 199, CourseName = "CourseName 199", LevelName = "الأولى", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 200, CourseName = "CourseName 200", LevelName = "الأولى", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 201, CourseName = "CourseName 201", LevelName = "الأولى", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 202, CourseName = "CourseName 202", LevelName = "الأولى", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 203, CourseName = "CourseName 203", LevelName = "الأولى", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 204, CourseName = "CourseName 204", LevelName = "الأولى", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 205, CourseName = "CourseName 205", LevelName = "الثانية", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 206, CourseName = "CourseName 206", LevelName = "الثانية", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 207, CourseName = "CourseName 207", LevelName = "الثانية", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 208, CourseName = "CourseName 208", LevelName = "الثانية", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 209, CourseName = "CourseName 209", LevelName = "الثانية", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 210, CourseName = "CourseName 210", LevelName = "الثانية", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 211, CourseName = "CourseName 211", LevelName = "الثانية", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 212, CourseName = "CourseName 212", LevelName = "الثانية", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 213, CourseName = "CourseName 213", LevelName = "الثانية", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 214, CourseName = "CourseName 214", LevelName = "الثانية", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 215, CourseName = "CourseName 215", LevelName = "الثانية", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 216, CourseName = "CourseName 216", LevelName = "الثانية", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 217, CourseName = "CourseName 217", LevelName = "الثالثة", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 218, CourseName = "CourseName 218", LevelName = "الثالثة", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 219, CourseName = "CourseName 219", LevelName = "الثالثة", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 220, CourseName = "CourseName 220", LevelName = "الثالثة", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 221, CourseName = "CourseName 221", LevelName = "الثالثة", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 222, CourseName = "CourseName 222", LevelName = "الثالثة", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 223, CourseName = "CourseName 223", LevelName = "الثالثة", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 224, CourseName = "CourseName 224", LevelName = "الثالثة", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 225, CourseName = "CourseName 225", LevelName = "الثالثة", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 226, CourseName = "CourseName 226", LevelName = "الثالثة", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 227, CourseName = "CourseName 227", LevelName = "الثالثة", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 228, CourseName = "CourseName 228", LevelName = "الثالثة", BranchName = "الهندسة الميكانيكية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 229, CourseName = "CourseName 229", LevelName = "الرابعة", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 230, CourseName = "CourseName 230", LevelName = "الرابعة", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 231, CourseName = "CourseName 231", LevelName = "الرابعة", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 232, CourseName = "CourseName 232", LevelName = "الرابعة", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 233, CourseName = "CourseName 233", LevelName = "الرابعة", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 234, CourseName = "CourseName 234", LevelName = "الرابعة", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 235, CourseName = "CourseName 235", LevelName = "الرابعة", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 236, CourseName = "CourseName 236", LevelName = "الرابعة", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 237, CourseName = "CourseName 237", LevelName = "الرابعة", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 238, CourseName = "CourseName 238", LevelName = "الرابعة", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 239, CourseName = "CourseName 239", LevelName = "الرابعة", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 240, CourseName = "CourseName 240", LevelName = "الرابعة", BranchName = "الهندسة المدنية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 241, CourseName = "CourseName 241", LevelName = "الرابعة", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 242, CourseName = "CourseName 242", LevelName = "الرابعة", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 243, CourseName = "CourseName 243", LevelName = "الرابعة", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 244, CourseName = "CourseName 244", LevelName = "الرابعة", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 245, CourseName = "CourseName 245", LevelName = "الرابعة", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 246, CourseName = "CourseName 246", LevelName = "الرابعة", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 247, CourseName = "CourseName 247", LevelName = "الرابعة", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 248, CourseName = "CourseName 248", LevelName = "الرابعة", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 249, CourseName = "CourseName 249", LevelName = "الرابعة", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 250, CourseName = "CourseName 250", LevelName = "الرابعة", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 251, CourseName = "CourseName 251", LevelName = "الرابعة", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 252, CourseName = "CourseName 252", LevelName = "الرابعة", BranchName = "الهندسة المعمارية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 253, CourseName = "CourseName 253", LevelName = "الرابعة", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 254, CourseName = "CourseName 254", LevelName = "الرابعة", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 255, CourseName = "CourseName 255", LevelName = "الرابعة", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 256, CourseName = "CourseName 256", LevelName = "الرابعة", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 257, CourseName = "CourseName 257", LevelName = "الرابعة", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 258, CourseName = "CourseName 258", LevelName = "الرابعة", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 259, CourseName = "CourseName 259", LevelName = "الرابعة", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 260, CourseName = "CourseName 260", LevelName = "الرابعة", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 261, CourseName = "CourseName 261", LevelName = "الرابعة", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 262, CourseName = "CourseName 262", LevelName = "الرابعة", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 263, CourseName = "CourseName 263", LevelName = "الرابعة", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 264, CourseName = "CourseName 264", LevelName = "الرابعة", BranchName = "هندسة القوى والآلات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 265, CourseName = "CourseName 265", LevelName = "الرابعة", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 266, CourseName = "CourseName 266", LevelName = "الرابعة", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 267, CourseName = "CourseName 267", LevelName = "الرابعة", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 268, CourseName = "CourseName 268", LevelName = "الرابعة", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 269, CourseName = "CourseName 269", LevelName = "الرابعة", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 270, CourseName = "CourseName 270", LevelName = "الرابعة", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 271, CourseName = "CourseName 271", LevelName = "الرابعة", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 272, CourseName = "CourseName 272", LevelName = "الرابعة", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 273, CourseName = "CourseName 273", LevelName = "الرابعة", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 274, CourseName = "CourseName 274", LevelName = "الرابعة", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 275, CourseName = "CourseName 275", LevelName = "الرابعة", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 276, CourseName = "CourseName 276", LevelName = "الرابعة", BranchName = "هندسة الإلكترونيات والاتصالات الكهربية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 277, CourseName = "CourseName 277", LevelName = "الرابعة", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 278, CourseName = "CourseName 278", LevelName = "الرابعة", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 279, CourseName = "CourseName 279", LevelName = "الرابعة", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 280, CourseName = "CourseName 280", LevelName = "الرابعة", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 281, CourseName = "CourseName 281", LevelName = "الرابعة", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 282, CourseName = "CourseName 282", LevelName = "الرابعة", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 283, CourseName = "CourseName 283", LevelName = "الرابعة", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 284, CourseName = "CourseName 284", LevelName = "الرابعة", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 285, CourseName = "CourseName 285", LevelName = "الرابعة", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 286, CourseName = "CourseName 286", LevelName = "الرابعة", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 287, CourseName = "CourseName 287", LevelName = "الرابعة", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 288, CourseName = "CourseName 288", LevelName = "الرابعة", BranchName = "هندسة الحاسبات والنظم", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 289, CourseName = "CourseName 289", LevelName = "الرابعة", BranchName = "هندسة الإنتاج والتصميم الميكانيكي", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 290, CourseName = "CourseName 290", LevelName = "الرابعة", BranchName = "هندسة الإنتاج والتصميم الميكانيكي", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 291, CourseName = "CourseName 291", LevelName = "الرابعة", BranchName = "هندسة الإنتاج والتصميم الميكانيكي", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 292, CourseName = "CourseName 292", LevelName = "الرابعة", BranchName = "هندسة الإنتاج والتصميم الميكانيكي", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 293, CourseName = "CourseName 293", LevelName = "الرابعة", BranchName = "هندسة الإنتاج والتصميم الميكانيكي", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 294, CourseName = "CourseName 294", LevelName = "الرابعة", BranchName = "هندسة الإنتاج والتصميم الميكانيكي", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 295, CourseName = "CourseName 295", LevelName = "الرابعة", BranchName = "هندسة الإنتاج والتصميم الميكانيكي", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 296, CourseName = "CourseName 296", LevelName = "الرابعة", BranchName = "هندسة الإنتاج والتصميم الميكانيكي", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 297, CourseName = "CourseName 297", LevelName = "الرابعة", BranchName = "هندسة الإنتاج والتصميم الميكانيكي", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 298, CourseName = "CourseName 298", LevelName = "الرابعة", BranchName = "هندسة الإنتاج والتصميم الميكانيكي", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 299, CourseName = "CourseName 299", LevelName = "الرابعة", BranchName = "هندسة الإنتاج والتصميم الميكانيكي", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 300, CourseName = "CourseName 300", LevelName = "الرابعة", BranchName = "هندسة الإنتاج والتصميم الميكانيكي", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 301, CourseName = "CourseName 301", LevelName = "الرابعة", BranchName = "الهندسة الصناعية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 302, CourseName = "CourseName 302", LevelName = "الرابعة", BranchName = "الهندسة الصناعية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 303, CourseName = "CourseName 303", LevelName = "الرابعة", BranchName = "الهندسة الصناعية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 304, CourseName = "CourseName 304", LevelName = "الرابعة", BranchName = "الهندسة الصناعية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 305, CourseName = "CourseName 305", LevelName = "الرابعة", BranchName = "الهندسة الصناعية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 306, CourseName = "CourseName 306", LevelName = "الرابعة", BranchName = "الهندسة الصناعية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 307, CourseName = "CourseName 307", LevelName = "الرابعة", BranchName = "الهندسة الصناعية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 308, CourseName = "CourseName 308", LevelName = "الرابعة", BranchName = "الهندسة الصناعية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 309, CourseName = "CourseName 309", LevelName = "الرابعة", BranchName = "الهندسة الصناعية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 310, CourseName = "CourseName 310", LevelName = "الرابعة", BranchName = "الهندسة الصناعية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 311, CourseName = "CourseName 311", LevelName = "الرابعة", BranchName = "الهندسة الصناعية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 312, CourseName = "CourseName 312", LevelName = "الرابعة", BranchName = "الهندسة الصناعية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 313, CourseName = "CourseName 313", LevelName = "الرابعة", BranchName = "هندسة القوى الميكانيكية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 314, CourseName = "CourseName 314", LevelName = "الرابعة", BranchName = "هندسة القوى الميكانيكية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 315, CourseName = "CourseName 315", LevelName = "الرابعة", BranchName = "هندسة القوى الميكانيكية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 316, CourseName = "CourseName 316", LevelName = "الرابعة", BranchName = "هندسة القوى الميكانيكية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 317, CourseName = "CourseName 317", LevelName = "الرابعة", BranchName = "هندسة القوى الميكانيكية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 318, CourseName = "CourseName 318", LevelName = "الرابعة", BranchName = "هندسة القوى الميكانيكية", Term = grad2021.Models.Term.الأول });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 319, CourseName = "CourseName 319", LevelName = "الرابعة", BranchName = "هندسة القوى الميكانيكية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 320, CourseName = "CourseName 320", LevelName = "الرابعة", BranchName = "هندسة القوى الميكانيكية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 321, CourseName = "CourseName 321", LevelName = "الرابعة", BranchName = "هندسة القوى الميكانيكية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 322, CourseName = "CourseName 322", LevelName = "الرابعة", BranchName = "هندسة القوى الميكانيكية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 323, CourseName = "CourseName 323", LevelName = "الرابعة", BranchName = "هندسة القوى الميكانيكية", Term = grad2021.Models.Term.الثاني });
            modelBuilder.Entity<CourseEnrollment>().HasData(new CourseEnrollment { CourseEnrollmentID = 324, CourseName = "CourseName 324", LevelName = "الرابعة", BranchName = "هندسة القوى الميكانيكية", Term = grad2021.Models.Term.الثاني });

            //Calculated Column Entities

            modelBuilder.Entity<StudentCourse>()
            .Property(c => c.TotalMark)
            .HasComputedColumnSql("[MidTermMark] + [CourseWorkMark] + [OralExamMark] + [FinalExamMark] + [MerciMark]");
            modelBuilder.Entity<Course>()
            .Property(c => c.FullMark)
            .HasComputedColumnSql("[CourseWorkMaxScore] + [MidTermExamMaxScore] + [OralExamMaxScore] + [TermExamMaxScore]");
            
        }
    }
}
