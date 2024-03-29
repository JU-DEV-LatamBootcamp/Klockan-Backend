﻿using Microsoft.AspNetCore.Mvc;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Application.DTOs.Program;
using Asp.Versioning;
using KlockanAPI.Infrastructure.CrossCutting.Authorization;

namespace KlockanAPI.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ProgramsController : ControllerBase
{
    private readonly IProgramService _programService;

    public ProgramsController(IProgramService programService)
    {
        _programService = programService;
    }

    [HttpGet]
    [HttpHead]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<ProgramDTO>>> GetAllPrograms()
    {
        if (!JwtTokenHelper.HasRequiredRole(HttpContext, "admin"))
        {
            return Forbid(); // Return 403 Forbidden if the user does not have the required role
        }
        var programs = await _programService.GetAllProgramsAsync();
        return Ok(programs);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ProgramDTO>> CreateProgram([FromBody] CreateProgramDTO createProgramDTO)
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
            var createdProgramDTO = await _programService.CreateProgramAsync(createProgramDTO);
            return CreatedAtAction(null, new { id = createdProgramDTO.Id }, createdProgramDTO);
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
    public async Task<ActionResult<ProgramDTO>> DeleteProgram(int id)
    {
        if (!JwtTokenHelper.HasRequiredRole(HttpContext, "admin"))
        {
            return Forbid(); // Return 403 Forbidden if the user does not have the required role
        }
        var course = await _programService.DeleteProgramAsync(id);

        return Ok(course);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ProgramDTO>> EditProgram([FromBody] ProgramDTO editProgramDTO)
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
            var editedProgramDTO = await _programService.EditProgramAsync(editProgramDTO);
            return Ok(editedProgramDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
