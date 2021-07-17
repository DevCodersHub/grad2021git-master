using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace grad2021.Migrations
{
    public partial class NewMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicYears",
                columns: table => new
                {
                    AcademicYearID = table.Column<int>(type: "int", nullable: false),
                    FirstSemesterStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstSemesterExamsStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstSemesterControlStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstSemesterObjectionStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstSemesterObjectionEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecondSemesterStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecondSemesterExamsStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecondSemesterControlStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecondSemesterObjectionStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecondSemesterObjectionEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NovemberExamsStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NovemberControlStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NovemberObjectionStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NovemberObjectionEndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicYears", x => x.AcademicYearID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmentDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentName);
                });

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    LevelName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaxFailures = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.LevelName);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    BranchName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BranchDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullCapacity = table.Column<int>(type: "int", nullable: false),
                    CurrentCapacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.BranchName);
                    table.ForeignKey(
                        name: "FK_Branches_Departments_DepartmentName",
                        column: x => x.DepartmentName,
                        principalTable: "Departments",
                        principalColumn: "DepartmentName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentCodes",
                columns: table => new
                {
                    DepartmentCodeValue = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentCodes", x => x.DepartmentCodeValue);
                    table.ForeignKey(
                        name: "FK_DepartmentCodes_Departments_DepartmentName",
                        column: x => x.DepartmentName,
                        principalTable: "Departments",
                        principalColumn: "DepartmentName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NatId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BirthPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SeatNo = table.Column<int>(type: "int", nullable: true),
                    BranchName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AcademicYearID = table.Column<int>(type: "int", nullable: false),
                    FinalMark = table.Column<double>(type: "float", nullable: false),
                    FinalGrade = table.Column<int>(type: "int", nullable: false),
                    HonourDegree = table.Column<bool>(type: "bit", nullable: false),
                    AcademicYearID1 = table.Column<int>(type: "int", nullable: true),
                    BranchName1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AcademicYears_AcademicYearID",
                        column: x => x.AcademicYearID,
                        principalTable: "AcademicYears",
                        principalColumn: "AcademicYearID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AcademicYears_AcademicYearID1",
                        column: x => x.AcademicYearID1,
                        principalTable: "AcademicYears",
                        principalColumn: "AcademicYearID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Branches_BranchName",
                        column: x => x.BranchName,
                        principalTable: "Branches",
                        principalColumn: "BranchName",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Branches_BranchName1",
                        column: x => x.BranchName1,
                        principalTable: "Branches",
                        principalColumn: "BranchName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    DepartmentCodeValue = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CourseDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LectureWeeklyDuration = table.Column<int>(type: "int", nullable: true),
                    SectionWeeklyDuration = table.Column<int>(type: "int", nullable: true),
                    CourseWorkMaxScore = table.Column<double>(type: "float", nullable: false),
                    MidTermExamMaxScore = table.Column<double>(type: "float", nullable: false),
                    OralExamMaxScore = table.Column<double>(type: "float", nullable: false),
                    TermExamMaxScore = table.Column<double>(type: "float", nullable: false),
                    FullMark = table.Column<double>(type: "float", nullable: false, computedColumnSql: "[CourseWorkMaxScore] + [MidTermExamMaxScore] + [OralExamMaxScore] + [TermExamMaxScore]")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseName);
                    table.ForeignKey(
                        name: "FK_Courses_DepartmentCodes_DepartmentCodeValue",
                        column: x => x.DepartmentCodeValue,
                        principalTable: "DepartmentCodes",
                        principalColumn: "DepartmentCodeValue",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstructorProfessions",
                columns: table => new
                {
                    InstructorProfessionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProfessionDegree = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PromotionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorProfessions", x => x.InstructorProfessionID);
                    table.ForeignKey(
                        name: "FK_InstructorProfessions_AspNetUsers_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstructorProfessions_Levels_ProfessionDegree",
                        column: x => x.ProfessionDegree,
                        principalTable: "Levels",
                        principalColumn: "LevelName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentEnrollments",
                columns: table => new
                {
                    StudentEnrollmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BranchName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AcademicYearID = table.Column<int>(type: "int", nullable: false),
                    LevelName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CompleteLevelMark = table.Column<double>(type: "float", nullable: false),
                    StudentGrade = table.Column<int>(type: "int", nullable: false),
                    StudentStatus = table.Column<int>(type: "int", nullable: false),
                    FailureNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEnrollments", x => x.StudentEnrollmentID);
                    table.ForeignKey(
                        name: "FK_StudentEnrollments_AcademicYears_AcademicYearID",
                        column: x => x.AcademicYearID,
                        principalTable: "AcademicYears",
                        principalColumn: "AcademicYearID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentEnrollments_AspNetUsers_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentEnrollments_Branches_BranchName",
                        column: x => x.BranchName,
                        principalTable: "Branches",
                        principalColumn: "BranchName",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentEnrollments_Levels_LevelName",
                        column: x => x.LevelName,
                        principalTable: "Levels",
                        principalColumn: "LevelName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseEnrollments",
                columns: table => new
                {
                    CourseEnrollmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BranchName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LevelName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Term = table.Column<int>(type: "int", nullable: false),
                    IsEssential = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseEnrollments", x => x.CourseEnrollmentID);
                    table.ForeignKey(
                        name: "FK_CourseEnrollments_Branches_BranchName",
                        column: x => x.BranchName,
                        principalTable: "Branches",
                        principalColumn: "BranchName",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseEnrollments_Courses_CourseName",
                        column: x => x.CourseName,
                        principalTable: "Courses",
                        principalColumn: "CourseName",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseEnrollments_Levels_LevelName",
                        column: x => x.LevelName,
                        principalTable: "Levels",
                        principalColumn: "LevelName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Selections",
                columns: table => new
                {
                    SelectionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentEnrollmentID = table.Column<int>(type: "int", nullable: false),
                    SelectionNo = table.Column<int>(type: "int", nullable: false),
                    CurrentBranchName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SelectionBranchName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Selections", x => x.SelectionID);
                    table.ForeignKey(
                        name: "FK_Selections_Branches_CurrentBranchName",
                        column: x => x.CurrentBranchName,
                        principalTable: "Branches",
                        principalColumn: "BranchName",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Selections_Branches_SelectionBranchName",
                        column: x => x.SelectionBranchName,
                        principalTable: "Branches",
                        principalColumn: "BranchName",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Selections_StudentEnrollments_StudentEnrollmentID",
                        column: x => x.StudentEnrollmentID,
                        principalTable: "StudentEnrollments",
                        principalColumn: "StudentEnrollmentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstructorEnrollments",
                columns: table => new
                {
                    InstructorEnrollmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CourseEnrollmentID = table.Column<int>(type: "int", nullable: false),
                    AcademicYearID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorEnrollments", x => x.InstructorEnrollmentID);
                    table.ForeignKey(
                        name: "FK_InstructorEnrollments_AcademicYears_AcademicYearID",
                        column: x => x.AcademicYearID,
                        principalTable: "AcademicYears",
                        principalColumn: "AcademicYearID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InstructorEnrollments_AspNetUsers_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InstructorEnrollments_CourseEnrollments_CourseEnrollmentID",
                        column: x => x.CourseEnrollmentID,
                        principalTable: "CourseEnrollments",
                        principalColumn: "CourseEnrollmentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourses",
                columns: table => new
                {
                    StudentCourseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentEnrollmentID = table.Column<int>(type: "int", nullable: false),
                    CourseEnrollmentID = table.Column<int>(type: "int", nullable: false),
                    AcademicYearID = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MidTermMark = table.Column<double>(type: "float", nullable: false),
                    CourseWorkMark = table.Column<double>(type: "float", nullable: false),
                    OralExamMark = table.Column<double>(type: "float", nullable: false),
                    FinalExamMark = table.Column<double>(type: "float", nullable: false),
                    MerciMark = table.Column<double>(type: "float", nullable: false),
                    CourseGrade = table.Column<int>(type: "int", nullable: false),
                    TotalMark = table.Column<double>(type: "float", nullable: false, computedColumnSql: "[MidTermMark] + [CourseWorkMark] + [OralExamMark] + [FinalExamMark] + [MerciMark]")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourses", x => x.StudentCourseID);
                    table.ForeignKey(
                        name: "FK_StudentCourses_AcademicYears_AcademicYearID",
                        column: x => x.AcademicYearID,
                        principalTable: "AcademicYears",
                        principalColumn: "AcademicYearID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourses_AspNetUsers_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourses_CourseEnrollments_CourseEnrollmentID",
                        column: x => x.CourseEnrollmentID,
                        principalTable: "CourseEnrollments",
                        principalColumn: "CourseEnrollmentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentCourses_StudentEnrollments_StudentEnrollmentID",
                        column: x => x.StudentEnrollmentID,
                        principalTable: "StudentEnrollments",
                        principalColumn: "StudentEnrollmentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AcademicYears",
                columns: new[] { "AcademicYearID", "FirstSemesterControlStartDate", "FirstSemesterExamsStartDate", "FirstSemesterObjectionEndDate", "FirstSemesterObjectionStartDate", "FirstSemesterStartDate", "NovemberControlStartDate", "NovemberExamsStartDate", "NovemberObjectionEndDate", "NovemberObjectionStartDate", "SecondSemesterControlStartDate", "SecondSemesterExamsStartDate", "SecondSemesterObjectionEndDate", "SecondSemesterObjectionStartDate", "SecondSemesterStartDate" },
                values: new object[] { 2021, null, null, null, null, null, null, null, null, null, null, null, null, null, null });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentName", "DepartmentDescription" },
                values: new object[,]
                {
                    { "الرياضيات والفيزيقا الهندسية", "وصف قسم الرياضيات والفيزيقا الهندسية" },
                    { "الهندسة المدنية", "وصف قسم الهندسة المدنية" },
                    { "الهندسة الكهربية", "وصف قسم الهندسة الكهربية" },
                    { "الهندسة المعمارية", "وصف قسم الهندسة المعمارية" },
                    { "الهندسة الميكانيكية", "وصف قسم الهندسة الميكانيكية" }
                });

            migrationBuilder.InsertData(
                table: "Levels",
                columns: new[] { "LevelName", "MaxFailures" },
                values: new object[,]
                {
                    { "أستاذ", 0 },
                    { "أستاذ مساعد", 0 },
                    { "مدرس", 0 },
                    { "مدرس مساعد", 0 },
                    { "معيد", 0 },
                    { "الثانية", 2 },
                    { "الثالثة", 2 },
                    { "أستاذ متفرغ", 0 },
                    { "الأولى", 2 },
                    { "الإعدادية", 2 },
                    { "الرابعة", 100 },
                    { "إداري", 0 }
                });

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "BranchName", "BranchDescription", "CurrentCapacity", "DepartmentName", "FullCapacity" },
                values: new object[,]
                {
                    { "الرياضيات والفيزيقا الهندسية", "وصف قسم الرياضيات والفيزيقا الهندسية", 0, "الرياضيات والفيزيقا الهندسية", 2 },
                    { "هندسة القوى الميكانيكية", "وصف شعبة هندسة القوى الميكانيكية", 0, "الهندسة الميكانيكية", 2 },
                    { "الهندسة الصناعية", "وصف شعبة الهندسة الصناعية", 0, "الهندسة الميكانيكية", 2 },
                    { "هندسة الإنتاج والتصميم الميكانيكي", "وصف شعبة هندسة الإنتاج والتصميم الميكانيكي", 0, "الهندسة الميكانيكية", 2 },
                    { "الهندسة الميكانيكية", "وصف قسم الهندسة الميكانيكية", 0, "الهندسة الميكانيكية", 2 },
                    { "الهندسة المدنية", "وصف قسم الهندسة المدنية", 0, "الهندسة المدنية", 2 },
                    { "هندسة القوى والآلات الكهربية", "وصف شعبة هندسة القوى والآلات الكهربية", 0, "الهندسة الكهربية", 2 },
                    { "هندسة الإلكترونيات والاتصالات الكهربية", "وصف شعبة هندسة الإلكترونيات والاتصالات الكهربية", 0, "الهندسة الكهربية", 2 },
                    { "هندسة الحاسبات والنظم", "وصف شعبة هندسة الحاسبات والنظم", 0, "الهندسة الكهربية", 2 },
                    { "الهندسة المعمارية", "وصف قسم الهندسة المعمارية", 0, "الهندسة المعمارية", 2 }
                });

            migrationBuilder.InsertData(
                table: "DepartmentCodes",
                columns: new[] { "DepartmentCodeValue", "DepartmentName" },
                values: new object[,]
                {
                    { "تمج", "الهندسة الميكانيكية" },
                    { "عمر", "الهندسة المعمارية" },
                    { "كهح", "الهندسة الكهربية" },
                    { "كھع", "الهندسة الكهربية" },
                    { "كهق", "الهندسة الكهربية" },
                    { "صنع", "الهندسة الميكانيكية" },
                    { "مدن", "الهندسة المدنية" },
                    { "هند", "الرياضيات والفيزيقا الهندسية" },
                    { "عام", "الرياضيات والفيزيقا الهندسية" },
                    { "ميك", "الرياضيات والفيزيقا الهندسية" },
                    { "فيز", "الرياضيات والفيزيقا الهندسية" },
                    { "ريض", "الرياضيات والفيزيقا الهندسية" },
                    { "كهت", "الهندسة الكهربية" },
                    { "قوى", "الهندسة الميكانيكية" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseName", "CourseCode", "CourseDescription", "CourseWorkMaxScore", "DepartmentCodeValue", "LectureWeeklyDuration", "MidTermExamMaxScore", "OralExamMaxScore", "SectionWeeklyDuration", "TermExamMaxScore" },
                values: new object[,]
                {
                    { "CourseName 1", "1", null, 15.0, "ريض", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 164", "164", null, 15.0, "كهح", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 150", "150", null, 15.0, "كهح", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 136", "136", null, 15.0, "كهح", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 122", "122", null, 15.0, "كهح", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 108", "108", null, 15.0, "كهح", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 94", "94", null, 15.0, "كهح", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 80", "80", null, 15.0, "كهح", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 178", "178", null, 15.0, "كهح", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 66", "66", null, 15.0, "كهح", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 38", "38", null, 15.0, "كهح", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 24", "24", null, 15.0, "كهح", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 10", "10", null, 15.0, "كهح", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 317", "317", null, 15.0, "كهت", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 303", "303", null, 15.0, "كهت", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 289", "289", null, 15.0, "كهت", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 275", "275", null, 15.0, "كهت", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 52", "52", null, 15.0, "كهح", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 261", "261", null, 15.0, "كهت", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 192", "192", null, 15.0, "كهح", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 220", "220", null, 15.0, "كهح", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 126", "126", null, 15.0, "عمر", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 112", "112", null, 15.0, "عمر", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 98", "98", null, 15.0, "عمر", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 84", "84", null, 15.0, "عمر", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 70", "70", null, 15.0, "عمر", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 56", "56", null, 15.0, "عمر", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 42", "42", null, 15.0, "عمر", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 206", "206", null, 15.0, "كهح", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 28", "28", null, 15.0, "عمر", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 318", "318", null, 15.0, "كهح", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 304", "304", null, 15.0, "كهح", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 290", "290", null, 15.0, "كهح", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 276", "276", null, 15.0, "كهح", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 262", "262", null, 15.0, "كهح", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 248", "248", null, 15.0, "كهح", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 234", "234", null, 15.0, "كهح", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 14", "14", null, 15.0, "عمر", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 247", "247", null, 15.0, "كهت", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 233", "233", null, 15.0, "كهت", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 219", "219", null, 15.0, "كهت", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 232", "232", null, 15.0, "كهق", null, 15.0, 15.0, null, 80.0 }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseName", "CourseCode", "CourseDescription", "CourseWorkMaxScore", "DepartmentCodeValue", "LectureWeeklyDuration", "MidTermExamMaxScore", "OralExamMaxScore", "SectionWeeklyDuration", "TermExamMaxScore" },
                values: new object[,]
                {
                    { "CourseName 218", "218", null, 15.0, "كهق", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 204", "204", null, 15.0, "كهق", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 190", "190", null, 15.0, "كهق", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 176", "176", null, 15.0, "كهق", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 162", "162", null, 15.0, "كهق", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 148", "148", null, 15.0, "كهق", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 246", "246", null, 15.0, "كهق", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 134", "134", null, 15.0, "كهق", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 106", "106", null, 15.0, "كهق", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 92", "92", null, 15.0, "كهق", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 78", "78", null, 15.0, "كهق", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 64", "64", null, 15.0, "كهق", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 50", "50", null, 15.0, "كهق", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 36", "36", null, 15.0, "كهق", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 22", "22", null, 15.0, "كهق", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 120", "120", null, 15.0, "كهق", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 260", "260", null, 15.0, "كهق", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 274", "274", null, 15.0, "كهق", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 288", "288", null, 15.0, "كهق", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 205", "205", null, 15.0, "كهت", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 191", "191", null, 15.0, "كهت", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 177", "177", null, 15.0, "كهت", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 163", "163", null, 15.0, "كهت", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 149", "149", null, 15.0, "كهت", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 135", "135", null, 15.0, "كهت", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 121", "121", null, 15.0, "كهت", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 107", "107", null, 15.0, "كهت", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 93", "93", null, 15.0, "كهت", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 79", "79", null, 15.0, "كهت", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 65", "65", null, 15.0, "كهت", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 51", "51", null, 15.0, "كهت", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 37", "37", null, 15.0, "كهت", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 23", "23", null, 15.0, "كهت", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 9", "9", null, 15.0, "كهت", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 316", "316", null, 15.0, "كهق", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 302", "302", null, 15.0, "كهق", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 140", "140", null, 15.0, "عمر", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 8", "8", null, 15.0, "كهق", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 154", "154", null, 15.0, "عمر", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 182", "182", null, 15.0, "عمر", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 11", "11", null, 15.0, "قوى", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 321", "321", null, 15.0, "صنع", null, 15.0, 15.0, null, 80.0 }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseName", "CourseCode", "CourseDescription", "CourseWorkMaxScore", "DepartmentCodeValue", "LectureWeeklyDuration", "MidTermExamMaxScore", "OralExamMaxScore", "SectionWeeklyDuration", "TermExamMaxScore" },
                values: new object[,]
                {
                    { "CourseName 307", "307", null, 15.0, "صنع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 293", "293", null, 15.0, "صنع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 279", "279", null, 15.0, "صنع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 265", "265", null, 15.0, "صنع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 251", "251", null, 15.0, "صنع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 25", "25", null, 15.0, "قوى", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 237", "237", null, 15.0, "صنع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 209", "209", null, 15.0, "صنع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 195", "195", null, 15.0, "صنع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 181", "181", null, 15.0, "صنع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 167", "167", null, 15.0, "صنع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 153", "153", null, 15.0, "صنع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 139", "139", null, 15.0, "صنع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 125", "125", null, 15.0, "صنع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 223", "223", null, 15.0, "صنع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 111", "111", null, 15.0, "صنع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 39", "39", null, 15.0, "قوى", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 67", "67", null, 15.0, "قوى", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 291", "291", null, 15.0, "قوى", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 277", "277", null, 15.0, "قوى", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 263", "263", null, 15.0, "قوى", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 249", "249", null, 15.0, "قوى", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 235", "235", null, 15.0, "قوى", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 221", "221", null, 15.0, "قوى", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 207", "207", null, 15.0, "قوى", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 53", "53", null, 15.0, "قوى", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 193", "193", null, 15.0, "قوى", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 165", "165", null, 15.0, "قوى", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 151", "151", null, 15.0, "قوى", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 137", "137", null, 15.0, "قوى", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 123", "123", null, 15.0, "قوى", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 109", "109", null, 15.0, "قوى", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 95", "95", null, 15.0, "قوى", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 81", "81", null, 15.0, "قوى", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 179", "179", null, 15.0, "قوى", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 97", "97", null, 15.0, "صنع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 83", "83", null, 15.0, "صنع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 69", "69", null, 15.0, "صنع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 82", "82", null, 15.0, "تمج", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 68", "68", null, 15.0, "تمج", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 54", "54", null, 15.0, "تمج", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 40", "40", null, 15.0, "تمج", null, 15.0, 15.0, null, 80.0 }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseName", "CourseCode", "CourseDescription", "CourseWorkMaxScore", "DepartmentCodeValue", "LectureWeeklyDuration", "MidTermExamMaxScore", "OralExamMaxScore", "SectionWeeklyDuration", "TermExamMaxScore" },
                values: new object[,]
                {
                    { "CourseName 26", "26", null, 15.0, "تمج", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 12", "12", null, 15.0, "تمج", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 322", "322", null, 15.0, "عمر", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 96", "96", null, 15.0, "تمج", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 308", "308", null, 15.0, "عمر", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 280", "280", null, 15.0, "عمر", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 266", "266", null, 15.0, "عمر", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 252", "252", null, 15.0, "عمر", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 238", "238", null, 15.0, "عمر", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 224", "224", null, 15.0, "عمر", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 210", "210", null, 15.0, "عمر", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 196", "196", null, 15.0, "عمر", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 294", "294", null, 15.0, "عمر", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 110", "110", null, 15.0, "تمج", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 124", "124", null, 15.0, "تمج", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 138", "138", null, 15.0, "تمج", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 55", "55", null, 15.0, "صنع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 41", "41", null, 15.0, "صنع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 27", "27", null, 15.0, "صنع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 13", "13", null, 15.0, "صنع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 320", "320", null, 15.0, "تمج", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 306", "306", null, 15.0, "تمج", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 292", "292", null, 15.0, "تمج", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 278", "278", null, 15.0, "تمج", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 264", "264", null, 15.0, "تمج", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 250", "250", null, 15.0, "تمج", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 236", "236", null, 15.0, "تمج", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 222", "222", null, 15.0, "تمج", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 208", "208", null, 15.0, "تمج", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 194", "194", null, 15.0, "تمج", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 180", "180", null, 15.0, "تمج", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 166", "166", null, 15.0, "تمج", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 152", "152", null, 15.0, "تمج", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 168", "168", null, 15.0, "عمر", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 315", "315", null, 15.0, "كھع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 301", "301", null, 15.0, "كھع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 287", "287", null, 15.0, "كھع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 129", "129", null, 15.0, "ميك", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 115", "115", null, 15.0, "ميك", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 101", "101", null, 15.0, "ميك", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 87", "87", null, 15.0, "ميك", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 73", "73", null, 15.0, "ميك", null, 15.0, 15.0, null, 80.0 }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseName", "CourseCode", "CourseDescription", "CourseWorkMaxScore", "DepartmentCodeValue", "LectureWeeklyDuration", "MidTermExamMaxScore", "OralExamMaxScore", "SectionWeeklyDuration", "TermExamMaxScore" },
                values: new object[,]
                {
                    { "CourseName 59", "59", null, 15.0, "ميك", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 45", "45", null, 15.0, "ميك", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 143", "143", null, 15.0, "ميك", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 31", "31", null, 15.0, "ميك", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 3", "3", null, 15.0, "ميك", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 324", "324", null, 15.0, "فيز", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 310", "310", null, 15.0, "فيز", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 296", "296", null, 15.0, "فيز", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 282", "282", null, 15.0, "فيز", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 268", "268", null, 15.0, "فيز", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 254", "254", null, 15.0, "فيز", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 17", "17", null, 15.0, "ميك", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 240", "240", null, 15.0, "فيز", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 157", "157", null, 15.0, "ميك", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 185", "185", null, 15.0, "ميك", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 88", "88", null, 15.0, "عام", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 74", "74", null, 15.0, "عام", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 60", "60", null, 15.0, "عام", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 46", "46", null, 15.0, "عام", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 32", "32", null, 15.0, "عام", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 18", "18", null, 15.0, "عام", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 4", "4", null, 15.0, "عام", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 171", "171", null, 15.0, "ميك", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 311", "311", null, 15.0, "ميك", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 283", "283", null, 15.0, "ميك", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 269", "269", null, 15.0, "ميك", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 255", "255", null, 15.0, "ميك", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 241", "241", null, 15.0, "ميك", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 227", "227", null, 15.0, "ميك", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 213", "213", null, 15.0, "ميك", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 199", "199", null, 15.0, "ميك", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 297", "297", null, 15.0, "ميك", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 226", "226", null, 15.0, "فيز", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 212", "212", null, 15.0, "فيز", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 198", "198", null, 15.0, "فيز", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 225", "225", null, 15.0, "ريض", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 211", "211", null, 15.0, "ريض", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 197", "197", null, 15.0, "ريض", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 183", "183", null, 15.0, "ريض", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 169", "169", null, 15.0, "ريض", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 155", "155", null, 15.0, "ريض", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 141", "141", null, 15.0, "ريض", null, 15.0, 15.0, null, 80.0 }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseName", "CourseCode", "CourseDescription", "CourseWorkMaxScore", "DepartmentCodeValue", "LectureWeeklyDuration", "MidTermExamMaxScore", "OralExamMaxScore", "SectionWeeklyDuration", "TermExamMaxScore" },
                values: new object[,]
                {
                    { "CourseName 239", "239", null, 15.0, "ريض", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 127", "127", null, 15.0, "ريض", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 99", "99", null, 15.0, "ريض", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 85", "85", null, 15.0, "ريض", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 71", "71", null, 15.0, "ريض", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 57", "57", null, 15.0, "ريض", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 43", "43", null, 15.0, "ريض", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 29", "29", null, 15.0, "ريض", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 15", "15", null, 15.0, "ريض", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 113", "113", null, 15.0, "ريض", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 253", "253", null, 15.0, "ريض", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 267", "267", null, 15.0, "ريض", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 281", "281", null, 15.0, "ريض", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 184", "184", null, 15.0, "فيز", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 170", "170", null, 15.0, "فيز", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 156", "156", null, 15.0, "فيز", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 142", "142", null, 15.0, "فيز", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 128", "128", null, 15.0, "فيز", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 114", "114", null, 15.0, "فيز", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 100", "100", null, 15.0, "فيز", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 86", "86", null, 15.0, "فيز", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 72", "72", null, 15.0, "فيز", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 58", "58", null, 15.0, "فيز", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 44", "44", null, 15.0, "فيز", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 30", "30", null, 15.0, "فيز", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 16", "16", null, 15.0, "فيز", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 2", "2", null, 15.0, "فيز", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 323", "323", null, 15.0, "ريض", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 309", "309", null, 15.0, "ريض", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 295", "295", null, 15.0, "ريض", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 102", "102", null, 15.0, "عام", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 116", "116", null, 15.0, "عام", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 130", "130", null, 15.0, "عام", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 144", "144", null, 15.0, "عام", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 300", "300", null, 15.0, "مدن", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 286", "286", null, 15.0, "مدن", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 272", "272", null, 15.0, "مدن", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 258", "258", null, 15.0, "مدن", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 244", "244", null, 15.0, "مدن", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 230", "230", null, 15.0, "مدن", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 216", "216", null, 15.0, "مدن", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 314", "314", null, 15.0, "مدن", null, 15.0, 15.0, null, 80.0 }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseName", "CourseCode", "CourseDescription", "CourseWorkMaxScore", "DepartmentCodeValue", "LectureWeeklyDuration", "MidTermExamMaxScore", "OralExamMaxScore", "SectionWeeklyDuration", "TermExamMaxScore" },
                values: new object[,]
                {
                    { "CourseName 202", "202", null, 15.0, "مدن", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 174", "174", null, 15.0, "مدن", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 160", "160", null, 15.0, "مدن", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 146", "146", null, 15.0, "مدن", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 132", "132", null, 15.0, "مدن", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 118", "118", null, 15.0, "مدن", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 104", "104", null, 15.0, "مدن", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 90", "90", null, 15.0, "مدن", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 188", "188", null, 15.0, "مدن", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 7", "7", null, 15.0, "كھع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 21", "21", null, 15.0, "كھع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 35", "35", null, 15.0, "كھع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 273", "273", null, 15.0, "كھع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 259", "259", null, 15.0, "كھع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 245", "245", null, 15.0, "كھع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 231", "231", null, 15.0, "كھع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 217", "217", null, 15.0, "كھع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 203", "203", null, 15.0, "كھع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 189", "189", null, 15.0, "كھع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 175", "175", null, 15.0, "كھع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 161", "161", null, 15.0, "كھع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 147", "147", null, 15.0, "كھع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 133", "133", null, 15.0, "كھع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 119", "119", null, 15.0, "كھع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 105", "105", null, 15.0, "كھع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 91", "91", null, 15.0, "كھع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 77", "77", null, 15.0, "كھع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 63", "63", null, 15.0, "كھع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 49", "49", null, 15.0, "كھع", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 76", "76", null, 15.0, "مدن", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 305", "305", null, 15.0, "قوى", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 62", "62", null, 15.0, "مدن", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 34", "34", null, 15.0, "مدن", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 47", "47", null, 15.0, "هند", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 33", "33", null, 15.0, "هند", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 19", "19", null, 15.0, "هند", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 5", "5", null, 15.0, "هند", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 312", "312", null, 15.0, "عام", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 298", "298", null, 15.0, "عام", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 284", "284", null, 15.0, "عام", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 61", "61", null, 15.0, "هند", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 270", "270", null, 15.0, "عام", null, 15.0, 15.0, null, 80.0 }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseName", "CourseCode", "CourseDescription", "CourseWorkMaxScore", "DepartmentCodeValue", "LectureWeeklyDuration", "MidTermExamMaxScore", "OralExamMaxScore", "SectionWeeklyDuration", "TermExamMaxScore" },
                values: new object[,]
                {
                    { "CourseName 242", "242", null, 15.0, "عام", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 228", "228", null, 15.0, "عام", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 214", "214", null, 15.0, "عام", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 200", "200", null, 15.0, "عام", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 186", "186", null, 15.0, "عام", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 172", "172", null, 15.0, "عام", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 158", "158", null, 15.0, "عام", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 256", "256", null, 15.0, "عام", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 75", "75", null, 15.0, "هند", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 89", "89", null, 15.0, "هند", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 103", "103", null, 15.0, "هند", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 20", "20", null, 15.0, "مدن", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 6", "6", null, 15.0, "مدن", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 313", "313", null, 15.0, "هند", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 299", "299", null, 15.0, "هند", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 285", "285", null, 15.0, "هند", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 271", "271", null, 15.0, "هند", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 257", "257", null, 15.0, "هند", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 243", "243", null, 15.0, "هند", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 229", "229", null, 15.0, "هند", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 215", "215", null, 15.0, "هند", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 201", "201", null, 15.0, "هند", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 187", "187", null, 15.0, "هند", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 173", "173", null, 15.0, "هند", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 159", "159", null, 15.0, "هند", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 145", "145", null, 15.0, "هند", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 131", "131", null, 15.0, "هند", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 117", "117", null, 15.0, "هند", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 48", "48", null, 15.0, "مدن", null, 15.0, 15.0, null, 80.0 },
                    { "CourseName 319", "319", null, 15.0, "قوى", null, 15.0, 15.0, null, 80.0 }
                });

            migrationBuilder.InsertData(
                table: "CourseEnrollments",
                columns: new[] { "CourseEnrollmentID", "BranchName", "CourseName", "IsEssential", "LevelName", "Term" },
                values: new object[,]
                {
                    { 1, "الرياضيات والفيزيقا الهندسية", "CourseName 1", false, "الإعدادية", 0 },
                    { 164, "هندسة الحاسبات والنظم", "CourseName 164", false, "الأولى", 1 },
                    { 150, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 150", false, "الثالثة", 0 },
                    { 136, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 136", false, "الثانية", 0 },
                    { 122, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 122", false, "الأولى", 0 },
                    { 108, "هندسة القوى والآلات الكهربية", "CourseName 108", false, "الثانية", 1 },
                    { 94, "هندسة القوى والآلات الكهربية", "CourseName 94", false, "الأولى", 1 },
                    { 80, "الهندسة المعمارية", "CourseName 80", false, "الثالثة", 1 },
                    { 178, "هندسة الحاسبات والنظم", "CourseName 178", false, "الثانية", 1 },
                    { 66, "الهندسة المعمارية", "CourseName 66", false, "الثانية", 0 },
                    { 38, "الهندسة المدنية", "CourseName 38", false, "الثالثة", 0 },
                    { 24, "الهندسة المدنية", "CourseName 24", false, "الأولى", 1 },
                    { 10, "الرياضيات والفيزيقا الهندسية", "CourseName 10", false, "الإعدادية", 1 },
                    { 317, "هندسة القوى الميكانيكية", "CourseName 317", false, "الرابعة", 0 },
                    { 303, "الهندسة الصناعية", "CourseName 303", false, "الرابعة", 0 },
                    { 289, "هندسة الإنتاج والتصميم الميكانيكي", "CourseName 289", false, "الرابعة", 0 },
                    { 275, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 275", false, "الرابعة", 1 },
                    { 52, "الهندسة المعمارية", "CourseName 52", false, "الأولى", 0 },
                    { 261, "هندسة القوى والآلات الكهربية", "CourseName 261", false, "الرابعة", 1 },
                    { 192, "هندسة الحاسبات والنظم", "CourseName 192", false, "الثالثة", 1 },
                    { 220, "الهندسة الميكانيكية", "CourseName 220", false, "الثالثة", 0 },
                    { 126, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 126", false, "الأولى", 0 },
                    { 112, "هندسة القوى والآلات الكهربية", "CourseName 112", false, "الثالثة", 0 },
                    { 98, "هندسة القوى والآلات الكهربية", "CourseName 98", false, "الثانية", 0 },
                    { 84, "الهندسة المعمارية", "CourseName 84", false, "الثالثة", 1 },
                    { 70, "الهندسة المعمارية", "CourseName 70", false, "الثانية", 1 },
                    { 56, "الهندسة المعمارية", "CourseName 56", false, "الأولى", 1 },
                    { 42, "الهندسة المدنية", "CourseName 42", false, "الثالثة", 0 },
                    { 206, "الهندسة الميكانيكية", "CourseName 206", false, "الثانية", 0 },
                    { 28, "الهندسة المدنية", "CourseName 28", false, "الثانية", 0 },
                    { 318, "هندسة القوى الميكانيكية", "CourseName 318", false, "الرابعة", 0 },
                    { 304, "الهندسة الصناعية", "CourseName 304", false, "الرابعة", 0 },
                    { 290, "هندسة الإنتاج والتصميم الميكانيكي", "CourseName 290", false, "الرابعة", 0 },
                    { 276, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 276", false, "الرابعة", 1 },
                    { 262, "هندسة القوى والآلات الكهربية", "CourseName 262", false, "الرابعة", 1 },
                    { 248, "الهندسة المعمارية", "CourseName 248", false, "الرابعة", 1 },
                    { 234, "الهندسة المدنية", "CourseName 234", false, "الرابعة", 0 },
                    { 14, "الهندسة المدنية", "CourseName 14", false, "الأولى", 0 },
                    { 247, "الهندسة المعمارية", "CourseName 247", false, "الرابعة", 1 },
                    { 233, "الهندسة المدنية", "CourseName 233", false, "الرابعة", 0 },
                    { 219, "الهندسة الميكانيكية", "CourseName 219", false, "الثالثة", 0 },
                    { 232, "الهندسة المدنية", "CourseName 232", false, "الرابعة", 0 }
                });

            migrationBuilder.InsertData(
                table: "CourseEnrollments",
                columns: new[] { "CourseEnrollmentID", "BranchName", "CourseName", "IsEssential", "LevelName", "Term" },
                values: new object[,]
                {
                    { 218, "الهندسة الميكانيكية", "CourseName 218", false, "الثالثة", 0 },
                    { 204, "الهندسة الميكانيكية", "CourseName 204", false, "الأولى", 1 },
                    { 190, "هندسة الحاسبات والنظم", "CourseName 190", false, "الثالثة", 1 },
                    { 176, "هندسة الحاسبات والنظم", "CourseName 176", false, "الثانية", 1 },
                    { 162, "هندسة الحاسبات والنظم", "CourseName 162", false, "الأولى", 0 },
                    { 148, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 148", false, "الثالثة", 0 },
                    { 246, "الهندسة المعمارية", "CourseName 246", false, "الرابعة", 0 },
                    { 134, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 134", false, "الثانية", 0 },
                    { 106, "هندسة القوى والآلات الكهربية", "CourseName 106", false, "الثانية", 1 },
                    { 92, "هندسة القوى والآلات الكهربية", "CourseName 92", false, "الأولى", 1 },
                    { 78, "الهندسة المعمارية", "CourseName 78", false, "الثالثة", 0 },
                    { 64, "الهندسة المعمارية", "CourseName 64", false, "الثانية", 0 },
                    { 50, "الهندسة المعمارية", "CourseName 50", false, "الأولى", 0 },
                    { 36, "الهندسة المدنية", "CourseName 36", false, "الثانية", 1 },
                    { 22, "الهندسة المدنية", "CourseName 22", false, "الأولى", 1 },
                    { 120, "هندسة القوى والآلات الكهربية", "CourseName 120", false, "الثالثة", 1 },
                    { 260, "هندسة القوى والآلات الكهربية", "CourseName 260", false, "الرابعة", 1 },
                    { 274, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 274", false, "الرابعة", 1 },
                    { 288, "هندسة الحاسبات والنظم", "CourseName 288", false, "الرابعة", 1 },
                    { 205, "الهندسة الميكانيكية", "CourseName 205", false, "الثانية", 0 },
                    { 191, "هندسة الحاسبات والنظم", "CourseName 191", false, "الثالثة", 1 },
                    { 177, "هندسة الحاسبات والنظم", "CourseName 177", false, "الثانية", 1 },
                    { 163, "هندسة الحاسبات والنظم", "CourseName 163", false, "الأولى", 1 },
                    { 149, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 149", false, "الثالثة", 0 },
                    { 135, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 135", false, "الثانية", 0 },
                    { 121, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 121", false, "الأولى", 0 },
                    { 107, "هندسة القوى والآلات الكهربية", "CourseName 107", false, "الثانية", 1 },
                    { 93, "هندسة القوى والآلات الكهربية", "CourseName 93", false, "الأولى", 1 },
                    { 79, "الهندسة المعمارية", "CourseName 79", false, "الثالثة", 1 },
                    { 65, "الهندسة المعمارية", "CourseName 65", false, "الثانية", 0 },
                    { 51, "الهندسة المعمارية", "CourseName 51", false, "الأولى", 0 },
                    { 37, "الهندسة المدنية", "CourseName 37", false, "الثالثة", 0 },
                    { 23, "الهندسة المدنية", "CourseName 23", false, "الأولى", 1 },
                    { 9, "الرياضيات والفيزيقا الهندسية", "CourseName 9", false, "الإعدادية", 0 },
                    { 316, "هندسة القوى الميكانيكية", "CourseName 316", false, "الرابعة", 0 },
                    { 302, "الهندسة الصناعية", "CourseName 302", false, "الرابعة", 0 },
                    { 140, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 140", false, "الثانية", 1 },
                    { 8, "الرياضيات والفيزيقا الهندسية", "CourseName 8", false, "الإعدادية", 1 },
                    { 154, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 154", false, "الثالثة", 1 },
                    { 182, "هندسة الحاسبات والنظم", "CourseName 182", false, "الثالثة", 0 },
                    { 11, "الرياضيات والفيزيقا الهندسية", "CourseName 11", false, "الإعدادية", 0 },
                    { 321, "هندسة القوى الميكانيكية", "CourseName 321", false, "الرابعة", 1 }
                });

            migrationBuilder.InsertData(
                table: "CourseEnrollments",
                columns: new[] { "CourseEnrollmentID", "BranchName", "CourseName", "IsEssential", "LevelName", "Term" },
                values: new object[,]
                {
                    { 307, "الهندسة الصناعية", "CourseName 307", false, "الرابعة", 1 },
                    { 293, "هندسة الإنتاج والتصميم الميكانيكي", "CourseName 293", false, "الرابعة", 0 },
                    { 279, "هندسة الحاسبات والنظم", "CourseName 279", false, "الرابعة", 0 },
                    { 265, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 265", false, "الرابعة", 0 },
                    { 251, "الهندسة المعمارية", "CourseName 251", false, "الرابعة", 1 },
                    { 25, "الهندسة المدنية", "CourseName 25", false, "الثانية", 0 },
                    { 237, "الهندسة المدنية", "CourseName 237", false, "الرابعة", 1 },
                    { 209, "الهندسة الميكانيكية", "CourseName 209", false, "الثانية", 0 },
                    { 195, "الهندسة الميكانيكية", "CourseName 195", false, "الأولى", 0 },
                    { 181, "هندسة الحاسبات والنظم", "CourseName 181", false, "الثالثة", 0 },
                    { 167, "هندسة الحاسبات والنظم", "CourseName 167", false, "الأولى", 1 },
                    { 153, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 153", false, "الثالثة", 1 },
                    { 139, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 139", false, "الثانية", 1 },
                    { 125, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 125", false, "الأولى", 0 },
                    { 223, "الهندسة الميكانيكية", "CourseName 223", false, "الثالثة", 1 },
                    { 111, "هندسة القوى والآلات الكهربية", "CourseName 111", false, "الثالثة", 0 },
                    { 39, "الهندسة المدنية", "CourseName 39", false, "الثالثة", 0 },
                    { 67, "الهندسة المعمارية", "CourseName 67", false, "الثانية", 1 },
                    { 291, "هندسة الإنتاج والتصميم الميكانيكي", "CourseName 291", false, "الرابعة", 0 },
                    { 277, "هندسة الحاسبات والنظم", "CourseName 277", false, "الرابعة", 0 },
                    { 263, "هندسة القوى والآلات الكهربية", "CourseName 263", false, "الرابعة", 1 },
                    { 249, "الهندسة المعمارية", "CourseName 249", false, "الرابعة", 1 },
                    { 235, "الهندسة المدنية", "CourseName 235", false, "الرابعة", 1 },
                    { 221, "الهندسة الميكانيكية", "CourseName 221", false, "الثالثة", 0 },
                    { 207, "الهندسة الميكانيكية", "CourseName 207", false, "الثانية", 0 },
                    { 53, "الهندسة المعمارية", "CourseName 53", false, "الأولى", 0 },
                    { 193, "الهندسة الميكانيكية", "CourseName 193", false, "الأولى", 0 },
                    { 165, "هندسة الحاسبات والنظم", "CourseName 165", false, "الأولى", 1 },
                    { 151, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 151", false, "الثالثة", 1 },
                    { 137, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 137", false, "الثانية", 0 },
                    { 123, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 123", false, "الأولى", 0 },
                    { 109, "هندسة القوى والآلات الكهربية", "CourseName 109", false, "الثالثة", 0 },
                    { 95, "هندسة القوى والآلات الكهربية", "CourseName 95", false, "الأولى", 1 },
                    { 81, "الهندسة المعمارية", "CourseName 81", false, "الثالثة", 1 },
                    { 179, "هندسة الحاسبات والنظم", "CourseName 179", false, "الثانية", 1 },
                    { 97, "هندسة القوى والآلات الكهربية", "CourseName 97", false, "الثانية", 0 },
                    { 83, "الهندسة المعمارية", "CourseName 83", false, "الثالثة", 1 },
                    { 69, "الهندسة المعمارية", "CourseName 69", false, "الثانية", 1 },
                    { 82, "الهندسة المعمارية", "CourseName 82", false, "الثالثة", 1 },
                    { 68, "الهندسة المعمارية", "CourseName 68", false, "الثانية", 1 },
                    { 54, "الهندسة المعمارية", "CourseName 54", false, "الأولى", 0 },
                    { 40, "الهندسة المدنية", "CourseName 40", false, "الثالثة", 0 }
                });

            migrationBuilder.InsertData(
                table: "CourseEnrollments",
                columns: new[] { "CourseEnrollmentID", "BranchName", "CourseName", "IsEssential", "LevelName", "Term" },
                values: new object[,]
                {
                    { 26, "الهندسة المدنية", "CourseName 26", false, "الثانية", 0 },
                    { 12, "الرياضيات والفيزيقا الهندسية", "CourseName 12", false, "الإعدادية", 1 },
                    { 322, "هندسة القوى الميكانيكية", "CourseName 322", false, "الرابعة", 1 },
                    { 96, "هندسة القوى والآلات الكهربية", "CourseName 96", false, "الأولى", 1 },
                    { 308, "الهندسة الصناعية", "CourseName 308", false, "الرابعة", 1 },
                    { 280, "هندسة الحاسبات والنظم", "CourseName 280", false, "الرابعة", 0 },
                    { 266, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 266", false, "الرابعة", 0 },
                    { 252, "الهندسة المعمارية", "CourseName 252", false, "الرابعة", 1 },
                    { 238, "الهندسة المدنية", "CourseName 238", false, "الرابعة", 1 },
                    { 224, "الهندسة الميكانيكية", "CourseName 224", false, "الثالثة", 1 },
                    { 210, "الهندسة الميكانيكية", "CourseName 210", false, "الثانية", 0 },
                    { 196, "الهندسة الميكانيكية", "CourseName 196", false, "الأولى", 0 },
                    { 294, "هندسة الإنتاج والتصميم الميكانيكي", "CourseName 294", false, "الرابعة", 0 },
                    { 110, "هندسة القوى والآلات الكهربية", "CourseName 110", false, "الثالثة", 0 },
                    { 124, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 124", false, "الأولى", 0 },
                    { 138, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 138", false, "الثانية", 0 },
                    { 55, "الهندسة المعمارية", "CourseName 55", false, "الأولى", 1 },
                    { 41, "الهندسة المدنية", "CourseName 41", false, "الثالثة", 0 },
                    { 27, "الهندسة المدنية", "CourseName 27", false, "الثانية", 0 },
                    { 13, "الهندسة المدنية", "CourseName 13", false, "الأولى", 0 },
                    { 320, "هندسة القوى الميكانيكية", "CourseName 320", false, "الرابعة", 1 },
                    { 306, "الهندسة الصناعية", "CourseName 306", false, "الرابعة", 0 },
                    { 292, "هندسة الإنتاج والتصميم الميكانيكي", "CourseName 292", false, "الرابعة", 0 },
                    { 278, "هندسة الحاسبات والنظم", "CourseName 278", false, "الرابعة", 0 },
                    { 264, "هندسة القوى والآلات الكهربية", "CourseName 264", false, "الرابعة", 1 },
                    { 250, "الهندسة المعمارية", "CourseName 250", false, "الرابعة", 1 },
                    { 236, "الهندسة المدنية", "CourseName 236", false, "الرابعة", 1 },
                    { 222, "الهندسة الميكانيكية", "CourseName 222", false, "الثالثة", 0 },
                    { 208, "الهندسة الميكانيكية", "CourseName 208", false, "الثانية", 0 },
                    { 194, "الهندسة الميكانيكية", "CourseName 194", false, "الأولى", 0 },
                    { 180, "هندسة الحاسبات والنظم", "CourseName 180", false, "الثانية", 1 },
                    { 166, "هندسة الحاسبات والنظم", "CourseName 166", false, "الأولى", 1 },
                    { 152, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 152", false, "الثالثة", 1 },
                    { 168, "هندسة الحاسبات والنظم", "CourseName 168", false, "الأولى", 1 },
                    { 315, "هندسة القوى الميكانيكية", "CourseName 315", false, "الرابعة", 0 },
                    { 301, "الهندسة الصناعية", "CourseName 301", false, "الرابعة", 0 },
                    { 287, "هندسة الحاسبات والنظم", "CourseName 287", false, "الرابعة", 1 },
                    { 129, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 129", false, "الأولى", 1 },
                    { 115, "هندسة القوى والآلات الكهربية", "CourseName 115", false, "الثالثة", 1 },
                    { 101, "هندسة القوى والآلات الكهربية", "CourseName 101", false, "الثانية", 0 },
                    { 87, "هندسة القوى والآلات الكهربية", "CourseName 87", false, "الأولى", 0 },
                    { 73, "الهندسة المعمارية", "CourseName 73", false, "الثالثة", 0 }
                });

            migrationBuilder.InsertData(
                table: "CourseEnrollments",
                columns: new[] { "CourseEnrollmentID", "BranchName", "CourseName", "IsEssential", "LevelName", "Term" },
                values: new object[,]
                {
                    { 59, "الهندسة المعمارية", "CourseName 59", false, "الأولى", 1 },
                    { 45, "الهندسة المدنية", "CourseName 45", false, "الثالثة", 1 },
                    { 143, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 143", false, "الثانية", 1 },
                    { 31, "الهندسة المدنية", "CourseName 31", false, "الثانية", 1 },
                    { 3, "الرياضيات والفيزيقا الهندسية", "CourseName 3", false, "الإعدادية", 0 },
                    { 324, "هندسة القوى الميكانيكية", "CourseName 324", false, "الرابعة", 1 },
                    { 310, "الهندسة الصناعية", "CourseName 310", false, "الرابعة", 1 },
                    { 296, "هندسة الإنتاج والتصميم الميكانيكي", "CourseName 296", false, "الرابعة", 1 },
                    { 282, "هندسة الحاسبات والنظم", "CourseName 282", false, "الرابعة", 0 },
                    { 268, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 268", false, "الرابعة", 0 },
                    { 254, "هندسة القوى والآلات الكهربية", "CourseName 254", false, "الرابعة", 0 },
                    { 17, "الهندسة المدنية", "CourseName 17", false, "الأولى", 0 },
                    { 240, "الهندسة المدنية", "CourseName 240", false, "الرابعة", 1 },
                    { 157, "هندسة الحاسبات والنظم", "CourseName 157", false, "الأولى", 0 },
                    { 185, "هندسة الحاسبات والنظم", "CourseName 185", false, "الثالثة", 0 },
                    { 88, "هندسة القوى والآلات الكهربية", "CourseName 88", false, "الأولى", 0 },
                    { 74, "الهندسة المعمارية", "CourseName 74", false, "الثالثة", 0 },
                    { 60, "الهندسة المعمارية", "CourseName 60", false, "الأولى", 1 },
                    { 46, "الهندسة المدنية", "CourseName 46", false, "الثالثة", 1 },
                    { 32, "الهندسة المدنية", "CourseName 32", false, "الثانية", 1 },
                    { 18, "الهندسة المدنية", "CourseName 18", false, "الأولى", 0 },
                    { 4, "الرياضيات والفيزيقا الهندسية", "CourseName 4", false, "الإعدادية", 1 },
                    { 171, "هندسة الحاسبات والنظم", "CourseName 171", false, "الثانية", 0 },
                    { 311, "الهندسة الصناعية", "CourseName 311", false, "الرابعة", 1 },
                    { 283, "هندسة الحاسبات والنظم", "CourseName 283", false, "الرابعة", 1 },
                    { 269, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 269", false, "الرابعة", 0 },
                    { 255, "هندسة القوى والآلات الكهربية", "CourseName 255", false, "الرابعة", 0 },
                    { 241, "الهندسة المعمارية", "CourseName 241", false, "الرابعة", 0 },
                    { 227, "الهندسة الميكانيكية", "CourseName 227", false, "الثالثة", 1 },
                    { 213, "الهندسة الميكانيكية", "CourseName 213", false, "الثانية", 1 },
                    { 199, "الهندسة الميكانيكية", "CourseName 199", false, "الأولى", 1 },
                    { 297, "هندسة الإنتاج والتصميم الميكانيكي", "CourseName 297", false, "الرابعة", 1 },
                    { 226, "الهندسة الميكانيكية", "CourseName 226", false, "الثالثة", 1 },
                    { 212, "الهندسة الميكانيكية", "CourseName 212", false, "الثانية", 1 },
                    { 198, "الهندسة الميكانيكية", "CourseName 198", false, "الأولى", 0 },
                    { 225, "الهندسة الميكانيكية", "CourseName 225", false, "الثالثة", 1 },
                    { 211, "الهندسة الميكانيكية", "CourseName 211", false, "الثانية", 1 },
                    { 197, "الهندسة الميكانيكية", "CourseName 197", false, "الأولى", 0 },
                    { 183, "هندسة الحاسبات والنظم", "CourseName 183", false, "الثالثة", 0 },
                    { 169, "هندسة الحاسبات والنظم", "CourseName 169", false, "الثانية", 0 },
                    { 155, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 155", false, "الثالثة", 1 },
                    { 141, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 141", false, "الثانية", 1 }
                });

            migrationBuilder.InsertData(
                table: "CourseEnrollments",
                columns: new[] { "CourseEnrollmentID", "BranchName", "CourseName", "IsEssential", "LevelName", "Term" },
                values: new object[,]
                {
                    { 239, "الهندسة المدنية", "CourseName 239", false, "الرابعة", 1 },
                    { 127, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 127", false, "الأولى", 1 },
                    { 99, "هندسة القوى والآلات الكهربية", "CourseName 99", false, "الثانية", 0 },
                    { 85, "هندسة القوى والآلات الكهربية", "CourseName 85", false, "الأولى", 0 },
                    { 71, "الهندسة المعمارية", "CourseName 71", false, "الثانية", 1 },
                    { 57, "الهندسة المعمارية", "CourseName 57", false, "الأولى", 1 },
                    { 43, "الهندسة المدنية", "CourseName 43", false, "الثالثة", 1 },
                    { 29, "الهندسة المدنية", "CourseName 29", false, "الثانية", 0 },
                    { 15, "الهندسة المدنية", "CourseName 15", false, "الأولى", 0 },
                    { 113, "هندسة القوى والآلات الكهربية", "CourseName 113", false, "الثالثة", 0 },
                    { 253, "هندسة القوى والآلات الكهربية", "CourseName 253", false, "الرابعة", 0 },
                    { 267, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 267", false, "الرابعة", 0 },
                    { 281, "هندسة الحاسبات والنظم", "CourseName 281", false, "الرابعة", 0 },
                    { 184, "هندسة الحاسبات والنظم", "CourseName 184", false, "الثالثة", 0 },
                    { 170, "هندسة الحاسبات والنظم", "CourseName 170", false, "الثانية", 0 },
                    { 156, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 156", false, "الثالثة", 1 },
                    { 142, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 142", false, "الثانية", 1 },
                    { 128, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 128", false, "الأولى", 1 },
                    { 114, "هندسة القوى والآلات الكهربية", "CourseName 114", false, "الثالثة", 0 },
                    { 100, "هندسة القوى والآلات الكهربية", "CourseName 100", false, "الثانية", 0 },
                    { 86, "هندسة القوى والآلات الكهربية", "CourseName 86", false, "الأولى", 0 },
                    { 72, "الهندسة المعمارية", "CourseName 72", false, "الثانية", 1 },
                    { 58, "الهندسة المعمارية", "CourseName 58", false, "الأولى", 1 },
                    { 44, "الهندسة المدنية", "CourseName 44", false, "الثالثة", 1 },
                    { 30, "الهندسة المدنية", "CourseName 30", false, "الثانية", 0 },
                    { 16, "الهندسة المدنية", "CourseName 16", false, "الأولى", 0 },
                    { 2, "الرياضيات والفيزيقا الهندسية", "CourseName 2", false, "الإعدادية", 1 },
                    { 323, "هندسة القوى الميكانيكية", "CourseName 323", false, "الرابعة", 1 },
                    { 309, "الهندسة الصناعية", "CourseName 309", false, "الرابعة", 1 },
                    { 295, "هندسة الإنتاج والتصميم الميكانيكي", "CourseName 295", false, "الرابعة", 1 },
                    { 102, "هندسة القوى والآلات الكهربية", "CourseName 102", false, "الثانية", 0 },
                    { 116, "هندسة القوى والآلات الكهربية", "CourseName 116", false, "الثالثة", 1 },
                    { 130, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 130", false, "الأولى", 1 },
                    { 144, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 144", false, "الثانية", 1 },
                    { 300, "هندسة الإنتاج والتصميم الميكانيكي", "CourseName 300", false, "الرابعة", 1 },
                    { 286, "هندسة الحاسبات والنظم", "CourseName 286", false, "الرابعة", 1 },
                    { 272, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 272", false, "الرابعة", 1 },
                    { 258, "هندسة القوى والآلات الكهربية", "CourseName 258", false, "الرابعة", 0 },
                    { 244, "الهندسة المعمارية", "CourseName 244", false, "الرابعة", 0 },
                    { 230, "الهندسة المدنية", "CourseName 230", false, "الرابعة", 0 },
                    { 216, "الهندسة الميكانيكية", "CourseName 216", false, "الثانية", 1 },
                    { 314, "هندسة القوى الميكانيكية", "CourseName 314", false, "الرابعة", 0 }
                });

            migrationBuilder.InsertData(
                table: "CourseEnrollments",
                columns: new[] { "CourseEnrollmentID", "BranchName", "CourseName", "IsEssential", "LevelName", "Term" },
                values: new object[,]
                {
                    { 202, "الهندسة الميكانيكية", "CourseName 202", false, "الأولى", 1 },
                    { 174, "هندسة الحاسبات والنظم", "CourseName 174", false, "الثانية", 0 },
                    { 160, "هندسة الحاسبات والنظم", "CourseName 160", false, "الأولى", 0 },
                    { 146, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 146", false, "الثالثة", 0 },
                    { 132, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 132", false, "الأولى", 1 },
                    { 118, "هندسة القوى والآلات الكهربية", "CourseName 118", false, "الثالثة", 1 },
                    { 104, "هندسة القوى والآلات الكهربية", "CourseName 104", false, "الثانية", 1 },
                    { 90, "هندسة القوى والآلات الكهربية", "CourseName 90", false, "الأولى", 0 },
                    { 188, "هندسة الحاسبات والنظم", "CourseName 188", false, "الثالثة", 1 },
                    { 7, "الرياضيات والفيزيقا الهندسية", "CourseName 7", false, "الإعدادية", 0 },
                    { 21, "الهندسة المدنية", "CourseName 21", false, "الأولى", 1 },
                    { 35, "الهندسة المدنية", "CourseName 35", false, "الثانية", 1 },
                    { 273, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 273", false, "الرابعة", 1 },
                    { 259, "هندسة القوى والآلات الكهربية", "CourseName 259", false, "الرابعة", 1 },
                    { 245, "الهندسة المعمارية", "CourseName 245", false, "الرابعة", 0 },
                    { 231, "الهندسة المدنية", "CourseName 231", false, "الرابعة", 0 },
                    { 217, "الهندسة الميكانيكية", "CourseName 217", false, "الثالثة", 0 },
                    { 203, "الهندسة الميكانيكية", "CourseName 203", false, "الأولى", 1 },
                    { 189, "هندسة الحاسبات والنظم", "CourseName 189", false, "الثالثة", 1 },
                    { 175, "هندسة الحاسبات والنظم", "CourseName 175", false, "الثانية", 1 },
                    { 161, "هندسة الحاسبات والنظم", "CourseName 161", false, "الأولى", 0 },
                    { 147, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 147", false, "الثالثة", 0 },
                    { 133, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 133", false, "الثانية", 0 },
                    { 119, "هندسة القوى والآلات الكهربية", "CourseName 119", false, "الثالثة", 1 },
                    { 105, "هندسة القوى والآلات الكهربية", "CourseName 105", false, "الثانية", 1 },
                    { 91, "هندسة القوى والآلات الكهربية", "CourseName 91", false, "الأولى", 1 },
                    { 77, "الهندسة المعمارية", "CourseName 77", false, "الثالثة", 0 },
                    { 63, "الهندسة المعمارية", "CourseName 63", false, "الثانية", 0 },
                    { 49, "الهندسة المعمارية", "CourseName 49", false, "الأولى", 0 },
                    { 76, "الهندسة المعمارية", "CourseName 76", false, "الثالثة", 0 },
                    { 305, "الهندسة الصناعية", "CourseName 305", false, "الرابعة", 0 },
                    { 62, "الهندسة المعمارية", "CourseName 62", false, "الثانية", 0 },
                    { 34, "الهندسة المدنية", "CourseName 34", false, "الثانية", 1 },
                    { 47, "الهندسة المدنية", "CourseName 47", false, "الثالثة", 1 },
                    { 33, "الهندسة المدنية", "CourseName 33", false, "الثانية", 1 },
                    { 19, "الهندسة المدنية", "CourseName 19", false, "الأولى", 1 },
                    { 5, "الرياضيات والفيزيقا الهندسية", "CourseName 5", false, "الإعدادية", 0 },
                    { 312, "الهندسة الصناعية", "CourseName 312", false, "الرابعة", 1 },
                    { 298, "هندسة الإنتاج والتصميم الميكانيكي", "CourseName 298", false, "الرابعة", 1 },
                    { 284, "هندسة الحاسبات والنظم", "CourseName 284", false, "الرابعة", 1 },
                    { 61, "الهندسة المعمارية", "CourseName 61", false, "الثانية", 0 },
                    { 270, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 270", false, "الرابعة", 0 }
                });

            migrationBuilder.InsertData(
                table: "CourseEnrollments",
                columns: new[] { "CourseEnrollmentID", "BranchName", "CourseName", "IsEssential", "LevelName", "Term" },
                values: new object[,]
                {
                    { 242, "الهندسة المعمارية", "CourseName 242", false, "الرابعة", 0 },
                    { 228, "الهندسة الميكانيكية", "CourseName 228", false, "الثالثة", 1 },
                    { 214, "الهندسة الميكانيكية", "CourseName 214", false, "الثانية", 1 },
                    { 200, "الهندسة الميكانيكية", "CourseName 200", false, "الأولى", 1 },
                    { 186, "هندسة الحاسبات والنظم", "CourseName 186", false, "الثالثة", 0 },
                    { 172, "هندسة الحاسبات والنظم", "CourseName 172", false, "الثانية", 0 },
                    { 158, "هندسة الحاسبات والنظم", "CourseName 158", false, "الأولى", 0 },
                    { 256, "هندسة القوى والآلات الكهربية", "CourseName 256", false, "الرابعة", 0 },
                    { 75, "الهندسة المعمارية", "CourseName 75", false, "الثالثة", 0 },
                    { 89, "هندسة القوى والآلات الكهربية", "CourseName 89", false, "الأولى", 0 },
                    { 103, "هندسة القوى والآلات الكهربية", "CourseName 103", false, "الثانية", 1 },
                    { 20, "الهندسة المدنية", "CourseName 20", false, "الأولى", 1 },
                    { 6, "الرياضيات والفيزيقا الهندسية", "CourseName 6", false, "الإعدادية", 1 },
                    { 313, "هندسة القوى الميكانيكية", "CourseName 313", false, "الرابعة", 0 },
                    { 299, "هندسة الإنتاج والتصميم الميكانيكي", "CourseName 299", false, "الرابعة", 1 },
                    { 285, "هندسة الحاسبات والنظم", "CourseName 285", false, "الرابعة", 1 },
                    { 271, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 271", false, "الرابعة", 1 },
                    { 257, "هندسة القوى والآلات الكهربية", "CourseName 257", false, "الرابعة", 0 },
                    { 243, "الهندسة المعمارية", "CourseName 243", false, "الرابعة", 0 },
                    { 229, "الهندسة المدنية", "CourseName 229", false, "الرابعة", 0 },
                    { 215, "الهندسة الميكانيكية", "CourseName 215", false, "الثانية", 1 },
                    { 201, "الهندسة الميكانيكية", "CourseName 201", false, "الأولى", 1 },
                    { 187, "هندسة الحاسبات والنظم", "CourseName 187", false, "الثالثة", 1 },
                    { 173, "هندسة الحاسبات والنظم", "CourseName 173", false, "الثانية", 0 },
                    { 159, "هندسة الحاسبات والنظم", "CourseName 159", false, "الأولى", 0 },
                    { 145, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 145", false, "الثالثة", 0 },
                    { 131, "هندسة الإلكترونيات والاتصالات الكهربية", "CourseName 131", false, "الأولى", 1 },
                    { 117, "هندسة القوى والآلات الكهربية", "CourseName 117", false, "الثالثة", 1 },
                    { 48, "الهندسة المدنية", "CourseName 48", false, "الثالثة", 1 },
                    { 319, "هندسة القوى الميكانيكية", "CourseName 319", false, "الرابعة", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AcademicYearID",
                table: "AspNetUsers",
                column: "AcademicYearID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AcademicYearID1",
                table: "AspNetUsers",
                column: "AcademicYearID1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BranchName",
                table: "AspNetUsers",
                column: "BranchName");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BranchName1",
                table: "AspNetUsers",
                column: "BranchName1");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_DepartmentName",
                table: "Branches",
                column: "DepartmentName");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEnrollments_BranchName",
                table: "CourseEnrollments",
                column: "BranchName");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEnrollments_CourseName",
                table: "CourseEnrollments",
                column: "CourseName");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEnrollments_LevelName",
                table: "CourseEnrollments",
                column: "LevelName");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_DepartmentCodeValue",
                table: "Courses",
                column: "DepartmentCodeValue");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentCodes_DepartmentName",
                table: "DepartmentCodes",
                column: "DepartmentName");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorEnrollments_AcademicYearID",
                table: "InstructorEnrollments",
                column: "AcademicYearID");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorEnrollments_ApplicationUserID",
                table: "InstructorEnrollments",
                column: "ApplicationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorEnrollments_CourseEnrollmentID",
                table: "InstructorEnrollments",
                column: "CourseEnrollmentID");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorProfessions_ApplicationUserID",
                table: "InstructorProfessions",
                column: "ApplicationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorProfessions_ProfessionDegree",
                table: "InstructorProfessions",
                column: "ProfessionDegree");

            migrationBuilder.CreateIndex(
                name: "IX_Selections_CurrentBranchName",
                table: "Selections",
                column: "CurrentBranchName");

            migrationBuilder.CreateIndex(
                name: "IX_Selections_SelectionBranchName",
                table: "Selections",
                column: "SelectionBranchName");

            migrationBuilder.CreateIndex(
                name: "IX_Selections_StudentEnrollmentID",
                table: "Selections",
                column: "StudentEnrollmentID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_AcademicYearID",
                table: "StudentCourses",
                column: "AcademicYearID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_ApplicationUserID",
                table: "StudentCourses",
                column: "ApplicationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_CourseEnrollmentID",
                table: "StudentCourses",
                column: "CourseEnrollmentID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_StudentEnrollmentID",
                table: "StudentCourses",
                column: "StudentEnrollmentID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEnrollments_AcademicYearID",
                table: "StudentEnrollments",
                column: "AcademicYearID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEnrollments_ApplicationUserID",
                table: "StudentEnrollments",
                column: "ApplicationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEnrollments_BranchName",
                table: "StudentEnrollments",
                column: "BranchName");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEnrollments_LevelName",
                table: "StudentEnrollments",
                column: "LevelName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "InstructorEnrollments");

            migrationBuilder.DropTable(
                name: "InstructorProfessions");

            migrationBuilder.DropTable(
                name: "Selections");

            migrationBuilder.DropTable(
                name: "StudentCourses");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "CourseEnrollments");

            migrationBuilder.DropTable(
                name: "StudentEnrollments");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropTable(
                name: "DepartmentCodes");

            migrationBuilder.DropTable(
                name: "AcademicYears");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
