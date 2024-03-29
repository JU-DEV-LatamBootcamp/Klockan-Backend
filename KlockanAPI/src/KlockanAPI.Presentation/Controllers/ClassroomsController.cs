﻿using Microsoft.AspNetCore.Mvc;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Application.DTOs.Classroom;
using Asp.Versioning;
using KlockanAPI.Application.DTOs.ClassroomUser;

namespace KlockanAPI.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ClassroomsController : ControllerBase
{
    private readonly IClassroomService _classroomService;

    public ClassroomsController(IClassroomService classroomService)
    {
        _classroomService = classroomService;
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClassroomDTO>> GetClassroomById(int id, bool populate)
    {
        var classroom = await _classroomService.GetClassroomByIdAsync(id, populate);
        return Ok(classroom);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClassroomDTO>> CreateClassroom([FromBody] CreateClassroomDTO createClassroomDTO)
    {
        var createdClassroomDTO = await _classroomService.CreateClassroomAsync(createClassroomDTO);

        return CreatedAtAction(null, new { id = createdClassroomDTO.Id }, createdClassroomDTO);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClassroomDTO>> UpdateClassroom(int id, [FromBody] UpdateClassroomDTO updateClassroomDTO)
    {
        updateClassroomDTO.Id = id;
        var classroomDTO = await _classroomService.UpdateClassroomAsync(updateClassroomDTO);

        return Ok(classroomDTO);
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

    [HttpPut("{id}/users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ClassroomUserDTO>>> UpdateClassroomUsers(int id, [FromBody] UpdateClassroomUsersDTO updateClassroomUsersDTO)
    {
        updateClassroomUsersDTO.Id = id;
        var classroomUsersDTOs = await _classroomService.UpdateClassroomUsersAsync(updateClassroomUsersDTO);

        return Ok(classroomUsersDTOs);
    }
}
