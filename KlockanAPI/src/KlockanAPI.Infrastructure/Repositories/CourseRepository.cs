﻿using Microsoft.EntityFrameworkCore;
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
        await _context.AddAsync(course);
        await _context.SaveChangesAsync();
        return course;
    }
}