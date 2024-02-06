using Microsoft.EntityFrameworkCore;

using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.Data;
using KlockanAPI.Infrastructure.Repositories.Interfaces;

namespace KlockanAPI.Infrastructure.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly KlockanContext _context;

    public CourseRepository(KlockanContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _context.Courses.ToListAsync();
    }

    public async Task<Course> CreateAsync(Course course)
    {
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();
        return course;
    }

    public async Task<Course?> GetCourseByIdAsync(int id)
    {
        return await _context.Courses.FindAsync(id);
    }

    public async Task<Course> DeleteCourseAsync(Course course)
    {
        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();

        return course;
    }

    public async Task<Course> UpdateCourseAsync(Course course)
    {
        // var _course = await _context.Courses.FindAsync(course.Id);

        // if (_course != null)
        // {
        //     _course.Name = course.Name;
        //     _course.Sessions = course.Sessions;
        //     _course.SessionDuration = course.SessionDuration;
        //     _course.Description = course.Description;
        //     await _context.SaveChangesAsync();
        // }

        // return _course;
        var editedCourse = await _context.Courses.FindAsync(course.Id);
        _context.Courses.Entry(editedCourse!).CurrentValues.SetValues(course);
        await _context.SaveChangesAsync();
        return course;
    }
}
