﻿using Microsoft.AspNetCore.Mvc;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Application.DTOs.Classroom;
using Asp.Versioning;
using KlockanAPI.Application.DTOs.Schedule;

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

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClassroomDTO>> CreateClassroom([FromBody] CreateClassroomDTO createClassroomDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var createdClassroomDTO = await _classroomService.CreateClassroomAsync(createClassroomDTO);

            return CreatedAtAction(null, new { id = createdClassroomDTO.Id }, createdClassroomDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{classroomId}/schedules")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ScheduleDTO>>> GetClassroomSchedules(int classroomId)
    {
        var schedules = await _scheduleService.GetSchedulesByClassroomIdAsync(classroomId);

        return Ok(schedules);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateClassroom(int id, [FromBody] UpdateClassroomDTO updateClassroomDTO)
    {
        updateClassroomDTO.Id = id;
        var updatedClassroomDTO = await _classroomService.UpdateClassroomAsync(updateClassroomDTO);

        return Ok(updatedClassroomDTO);
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
}
