using Microsoft.EntityFrameworkCore;
using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Data.Seeders;

namespace KlockanAPI.Infrastructure.Data;

public class KlockanContext : DbContext
{
    public KlockanContext(DbContextOptions options) : base(options)
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
            .HasForeignKey(m => m.TrainerId);

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

        // Seed data
        modelBuilder.SeedPrograms();
        modelBuilder.SeedCourses();
        modelBuilder.SeedClassrooms();
        modelBuilder.SeedMeetings();
        modelBuilder.SeedWeekdays();
        modelBuilder.SeedSchedules();
        modelBuilder.SeedCountries();
        modelBuilder.SeedCities();
        modelBuilder.SeedRoles();
        modelBuilder.SeedUsers();
        modelBuilder.SeedMeetingAttendanceStatus();
    }
}