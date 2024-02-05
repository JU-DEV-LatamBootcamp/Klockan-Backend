﻿// <auto-generated />
using System;
using KlockanAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KlockanAPI.Infrastructure.Data.Migrations
{
    [DbContext(typeof(KlockanContext))]
    partial class KlockanContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("KlockanAPI.Domain.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<int>("CountryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.Classroom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ProgramId")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("ProgramId");

                    b.ToTable("Classrooms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CourseId = 1,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProgramId = 1,
                            StartDate = new DateOnly(2024, 1, 23)
                        });
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.ClassroomUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClassroomId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClassroomId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("ClassroomUsers");
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("SessionDuration")
                        .HasColumnType("integer");

                    b.Property<int?>("Sessions")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc),
                            Description = "Course to develop Web Applications focusing on HTML, CSS, JavaScript, and popular frameworks.",
                            Name = "Frontend Development",
                            SessionDuration = 60,
                            Sessions = 10
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc),
                            Description = "Course on server side programming, databases, and API construction.",
                            Name = "Backend Development",
                            SessionDuration = 75,
                            Sessions = 12
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc),
                            Description = "Comprehensive course covering both frontend and backend development to build complete applications.",
                            Name = "Full Stack Development",
                            SessionDuration = 90,
                            Sessions = 15
                        });
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.Holiday", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Holidays");
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.Meeting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClassroomId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SessionNumber")
                        .HasColumnType("integer");

                    b.Property<TimeOnly>("Time")
                        .HasColumnType("time without time zone");

                    b.Property<int?>("TrainerId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ClassroomId");

                    b.HasIndex("TrainerId");

                    b.ToTable("Meetings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClassroomId = 1,
                            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Date = new DateOnly(2024, 1, 23),
                            SessionNumber = 3,
                            Time = new TimeOnly(15, 30, 0)
                        },
                        new
                        {
                            Id = 2,
                            ClassroomId = 1,
                            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Date = new DateOnly(2024, 1, 23),
                            SessionNumber = 3,
                            Time = new TimeOnly(15, 30, 0)
                        },
                        new
                        {
                            Id = 3,
                            ClassroomId = 1,
                            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Date = new DateOnly(2024, 1, 23),
                            SessionNumber = 3,
                            Time = new TimeOnly(15, 30, 0)
                        },
                        new
                        {
                            Id = 4,
                            ClassroomId = 1,
                            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Date = new DateOnly(2024, 1, 23),
                            SessionNumber = 3,
                            Time = new TimeOnly(15, 30, 0)
                        });
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.MeetingAttendance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClassroomUserId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("MeetingAttendanceStatusId")
                        .HasColumnType("integer");

                    b.Property<int>("MeetingId")
                        .HasColumnType("integer");

                    b.Property<int>("MinutesAttended")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ClassroomUserId");

                    b.HasIndex("MeetingAttendanceStatusId");

                    b.HasIndex("MeetingId");

                    b.ToTable("MeetingAttendances");
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.MeetingAttendanceStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("MeetingAttendanceStatuses");
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.Program", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Programs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc),
                            Description = "Program covering concepts in software development.",
                            Name = "Bootcamp Developers 01"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc),
                            Description = "Program focused on advanced software design and development techniques.",
                            Name = "Advanced Bootcamp Developers 01"
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc),
                            Description = "Program designed to teach the fundamentals of data analysis, machine learning, and statistical modeling.",
                            Name = "Bootcamp Data Science and Analytics 01"
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc),
                            Description = "Program covering concepts in software development.",
                            Name = "Bootcamp Developers 02"
                        });
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClassroomId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time without time zone");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("WeekdayId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClassroomId");

                    b.HasIndex("WeekdayId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Birthdate")
                        .HasColumnType("date");

                    b.Property<int>("CityId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.Weekday", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Weekdays");
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.City", b =>
                {
                    b.HasOne("KlockanAPI.Domain.Models.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.Classroom", b =>
                {
                    b.HasOne("KlockanAPI.Domain.Models.Course", "Course")
                        .WithMany("Classrooms")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KlockanAPI.Domain.Models.Program", "Program")
                        .WithMany("Classrooms")
                        .HasForeignKey("ProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Program");
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.ClassroomUser", b =>
                {
                    b.HasOne("KlockanAPI.Domain.Models.Classroom", "Classroom")
                        .WithMany("ClassroomUsers")
                        .HasForeignKey("ClassroomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KlockanAPI.Domain.Models.Role", "Role")
                        .WithMany("ClassroomUsers")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KlockanAPI.Domain.Models.User", "User")
                        .WithMany("ClassroomUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Classroom");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.Meeting", b =>
                {
                    b.HasOne("KlockanAPI.Domain.Models.Classroom", "Classroom")
                        .WithMany("Meetings")
                        .HasForeignKey("ClassroomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KlockanAPI.Domain.Models.ClassroomUser", "Trainer")
                        .WithMany("Meetings")
                        .HasForeignKey("TrainerId");

                    b.Navigation("Classroom");

                    b.Navigation("Trainer");
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.MeetingAttendance", b =>
                {
                    b.HasOne("KlockanAPI.Domain.Models.ClassroomUser", "User")
                        .WithMany("MeetingAttendances")
                        .HasForeignKey("ClassroomUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KlockanAPI.Domain.Models.MeetingAttendanceStatus", "Status")
                        .WithMany("MeetingAttendances")
                        .HasForeignKey("MeetingAttendanceStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KlockanAPI.Domain.Models.Meeting", "Meeting")
                        .WithMany("MeetingAttendances")
                        .HasForeignKey("MeetingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meeting");

                    b.Navigation("Status");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.Schedule", b =>
                {
                    b.HasOne("KlockanAPI.Domain.Models.Classroom", "Classroom")
                        .WithMany("Schedule")
                        .HasForeignKey("ClassroomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KlockanAPI.Domain.Models.Weekday", "Weekday")
                        .WithMany("Schedules")
                        .HasForeignKey("WeekdayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Classroom");

                    b.Navigation("Weekday");
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.User", b =>
                {
                    b.HasOne("KlockanAPI.Domain.Models.City", "City")
                        .WithMany("Users")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KlockanAPI.Domain.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.City", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.Classroom", b =>
                {
                    b.Navigation("ClassroomUsers");

                    b.Navigation("Meetings");

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.ClassroomUser", b =>
                {
                    b.Navigation("MeetingAttendances");

                    b.Navigation("Meetings");
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.Country", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.Course", b =>
                {
                    b.Navigation("Classrooms");
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.Meeting", b =>
                {
                    b.Navigation("MeetingAttendances");
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.MeetingAttendanceStatus", b =>
                {
                    b.Navigation("MeetingAttendances");
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.Program", b =>
                {
                    b.Navigation("Classrooms");
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.Role", b =>
                {
                    b.Navigation("ClassroomUsers");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.User", b =>
                {
                    b.Navigation("ClassroomUsers");
                });

            modelBuilder.Entity("KlockanAPI.Domain.Models.Weekday", b =>
                {
                    b.Navigation("Schedules");
                });
#pragma warning restore 612, 618
        }
    }
}
