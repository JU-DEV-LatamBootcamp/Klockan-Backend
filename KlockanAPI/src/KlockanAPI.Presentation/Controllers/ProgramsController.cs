using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Application.DTOs.Program;
using Asp.Versioning;

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
    public async Task<ActionResult<IEnumerable<ProgramDTO>>> GetAllPrograms()
    {
        var programs = await _programService.GetAllProgramsAsync();
        return Ok(programs);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProgramDTO>> CreateProgram([FromBody] CreateProgramDTO createProgramDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdProgramDto = await _programService.CreateProgramAsync(createProgramDto);
            return CreatedAtAction(null, new { id = createdProgramDto.Id }, createdProgramDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
