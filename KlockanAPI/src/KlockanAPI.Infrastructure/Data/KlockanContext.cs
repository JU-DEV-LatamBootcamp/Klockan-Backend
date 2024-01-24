﻿using Microsoft.EntityFrameworkCore;
using KlockanAPI.Domain.Models;

namespace KlockanAPI.Infrastructure.Data;

public class KlockanContext : DbContext
{
    public KlockanContext(DbContextOptions<KlockanContext> options) : base(options)
    {
    }

    public DbSet<City> Cities { get; set; }
    public DbSet<Classroom> Classrooms { get; set; }
    public DbSet<ClassroomUser> ClassroomUsers { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Holiday> Holidays { get; set; }
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<MeetingAttendance> MeetingAttendances { get; set; }
    public DbSet<MeetingAttendanceStatus> MeetingAttendanceStatuses { get; set; }
    public DbSet<Program> Programs { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Weekday> Weekdays { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Relation of Country with City
        modelBuilder.Entity<Country>()
            .HasMany(c => c.Cities)
            .WithOne(e => e.Country)
            .HasForeignKey(e => e.CountryId);

        // Relation of City with User
        modelBuilder.Entity<City>()
            .HasMany(c => c.Users)
            .WithOne(u => u.City)
            .HasForeignKey(u => u.CityId);

        // Relation of Course with Classroom
        modelBuilder.Entity<Course>()
            .HasMany(c => c.Classrooms)
            .WithOne(cr => cr.Course)
            .HasForeignKey(cr => cr.CourseId);

        // Relation of Program with Classroom
        modelBuilder.Entity<Program>()
            .HasMany(p => p.Classrooms)
            .WithOne(cr => cr.Program)
            .HasForeignKey(cr => cr.ProgramId);

        // Relation of Classroom with Meeting, Schedule and ClassroomUser
        modelBuilder.Entity<Classroom>()
            .HasMany(cr => cr.Meetings)
            .WithOne(m => m.Classroom)
            .HasForeignKey(m => m.ClassroomId);

        modelBuilder.Entity<Classroom>()
            .HasMany(cr => cr.Schedule)
            .WithOne(s => s.Classroom)
            .HasForeignKey(s => s.ClassroomId);

        modelBuilder.Entity<Classroom>()
            .HasMany(cr => cr.ClassroomUsers)
            .WithOne(cu => cu.Classroom)
            .HasForeignKey(cu => cu.ClassroomId);

        // Relation of ClassroomUser with Meeting and MeetingAttendance
        modelBuilder.Entity<ClassroomUser>()
            .HasMany(cu => cu.Meetings)
            .WithOne(m => m.Trainer)
            .HasForeignKey(m => m.ClassroomUserId);

        modelBuilder.Entity<ClassroomUser>()
            .HasMany(cu => cu.MeetingAttendances)
            .WithOne(ma => ma.User)
            .HasForeignKey(ma => ma.ClassroomUserId);

        // Relation of User with ClassroomUser
        modelBuilder.Entity<User>()
            .HasMany(u => u.ClassroomUsers)
            .WithOne(cu => cu.User)
            .HasForeignKey(cu => cu.UserId);

        // Relation of Meeting with MeetingAttendance
        modelBuilder.Entity<Meeting>()
            .HasMany(m => m.MeetingAttendances)
            .WithOne(ma => ma.Meeting)
            .HasForeignKey(ma => ma.MeetingId);

        // Relation of MeetingAttendanceStatus with MeetingAttendance
        modelBuilder.Entity<MeetingAttendanceStatus>()
            .HasMany(mas => mas.MeetingAttendances)
            .WithOne(ma => ma.Status)
            .HasForeignKey(ma => ma.MeetingAttendanceStatusId);

        // Relation of Role with User and ClassroomUser
        modelBuilder.Entity<Role>()
            .HasMany(r => r.Users)
            .WithOne(u => u.Role)
            .HasForeignKey(u => u.RoleId);

        modelBuilder.Entity<Role>()
            .HasMany(r => r.ClassroomUsers)
            .WithOne(cu => cu.Role)
            .HasForeignKey(cu => cu.RoleId);

        // Relation of Weekday with Schedule
        modelBuilder.Entity<Weekday>()
            .HasMany(w => w.Schedules)
            .WithOne(s => s.Weekday)
            .HasForeignKey(s => s.WeekdayId);

        // Seed data for Program
        modelBuilder.Entity<Program>().HasData(
        new Program
        {
            Id = 1,
            Name = "Bootcamp Developers 01",
            Description = "Program covering concepts in software development.",
            CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
        },
        new Program
        {
            Id = 2,
            Name = "Advanced Bootcamp Developers 01",
            Description = "Program focused on advanced software design and development techniques.",
            CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
        },
        new Program
        {
            Id = 3,
            Name = "Bootcamp Data Science and Analytics 01",
            Description = "Program designed to teach the fundamentals of data analysis, machine learning, and statistical modeling.",
            CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
        },
        new Program
        {
            Id = 4,
            Name = "Bootcamp Developers 02",
            Description = "Program covering concepts in software development.",
            CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
        });

        // Seed data for Course
        modelBuilder.Entity<Course>().HasData(
        new Course
        {
            Id = 1,
            Name = "Frontend Development",
            Code = "FE",
            Description = "Course to develop Web Applications focusing on HTML, CSS, JavaScript, and popular frameworks.",
            Sessions = 10,
            SessionDuration = 60,
            CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
        },
        new Course
        {
            Id = 2,
            Name = "Backend Development",
            Code = "BE",
            Description = "Course on server side programming, databases, and API construction.",
            Sessions = 12,
            SessionDuration = 75,
            CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
        },
        new Course
        {
            Id = 3,
            Name = "Full Stack Development",
            Code = "FS",
            Description = "Comprehensive course covering both frontend and backend development to build complete applications.",
            Sessions = 15,
            SessionDuration = 90,
            CreatedAt = new DateTime(2024, 1, 23, 0, 0, 0, DateTimeKind.Utc)
        });
    }
}