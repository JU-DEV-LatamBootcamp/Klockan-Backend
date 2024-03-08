using Microsoft.AspNetCore.Mvc;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Application.DTOs.Classroom;
using Asp.Versioning;
using KlockanAPI.Application.CrossCutting;
using KlockanAPI.Domain.Models;
using KlockanAPI.Application.DTOs.User;

namespace KlockanAPI.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ClassroomsController : ControllerBase
{
    private readonly IClassroomService _classroomService;
    private readonly IScheduleService _scheduleService;

    public ClassroomsController(IClassroomService classroomService, IScheduleService scheduleService)
    {
        _classroomService = classroomService;
        _scheduleService = scheduleService;
    }

    [HttpGet]
    [HttpHead]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ClassroomDTO>>> GetAllClassrooms()
    {
        var classrooms = await _classroomService.GetAllClassroomsAsync();
        return Ok(classrooms);
    }

    [HttpGet("{id}")]
    [HttpHead("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClassroomDTO>> GetClassroom(int id)
    {
        try
        {
            var classroom = await _classroomService.GetClassroomByIdAsync(id);
            return Ok(classroom);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("{id}/attendees")]
    [HttpHead("{id}/attendees")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<User>>> GetClassroomUsers(int id)
    {
        try
        {
            IEnumerable<User> users = await _classroomService.GetClassroomUsersAsync(id);
            return Ok(users);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClassroomDTO>> CreateClassroom([FromBody] CreateClassroomDTO createClassroomDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdClassroomDTO = await _classroomService.CreateClassroomAsync(createClassroomDTO);
            var createSchedulesDTOs = _classroomService.MapCreateClassroomSchedulesDTOsToCreateScheduleDTOs(createdClassroomDTO.Id, createClassroomDTO.Schedule);

            await _scheduleService.CreateManySchedulesAsync(createSchedulesDTOs);

            return CreatedAtAction(null, new { id = createdClassroomDTO.Id }, createdClassroomDTO);
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
    public async Task<ActionResult<ClassroomDTO>> DeleteClassroom(int id)
    {
        var classroom = await _classroomService.DeleteClassroomAsync(id);
        return Ok(classroom);
    }

    [HttpDelete("{classroomId}/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> RemoveUserFromClassroom(int classroomId, int userId)
    {
        var user = await _classroomService.RemoveUserFromClassroom(classroomId, userId);
        return Ok(user);
    }
}
