using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using KlockanAPI.Application.DTOs.Course;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Application;
using KlockanAPI.Domain.Models;
using KlockanAPI.Infrastructure.CrossCutting.Authorization;

namespace KlockanAPI.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    [HttpHead]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<CourseDTO>>> GetAll()
    {
        if (!JwtTokenHelper.HasRequiredRole(HttpContext, "admin"))
        {
            return Forbid(); // Return 403 Forbidden if the user does not have the required role
        }
        var courses = await _courseService.GetAllCoursesAsync();
        return Ok(courses);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CourseDTO>> CreateCourse([FromBody] CreateCourseDTO createCourseDTO)
    {
        if (!JwtTokenHelper.HasRequiredRole(HttpContext, "admin"))
        {
            return Forbid(); // Return 403 Forbidden if the user does not have the required role
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdCourseDTO = await _courseService.CreateCourseAsync(createCourseDTO);
            return CreatedAtAction(null, new { id = createdCourseDTO.Id }, createdCourseDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CourseDTO>> Delete(int id)
    {
        if (!JwtTokenHelper.HasRequiredRole(HttpContext, "admin"))
        {
            return Forbid(); // Return 403 Forbidden if the user does not have the required role
        }
        var course = await _courseService.DeleteCourseAsync(id);

        return Ok(course);
    }

    [HttpPut("{courseId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<CourseDTO>> UpdateCourse([FromBody] Course course)
    {
        if (!JwtTokenHelper.HasRequiredRole(HttpContext, "admin"))
        {
            return Forbid(); // Return 403 Forbidden if the user does not have the required role
        }
        var _course = await _courseService.UpdateCourseAsync(course);
        return Ok(_course);
    }
}
