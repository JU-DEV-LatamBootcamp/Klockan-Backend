﻿using NSubstitute;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Presentation.Controllers;
using KlockanAPI.Application.DTOs.Classroom;
using Moq;

namespace KlockanAPI.Presentation.Tests.Controllers;

public class ClassroomsControllerTests
{
    private readonly IClassroomService _classroomService;
    private readonly Mock<IClassroomService> _mockClassroomService;
    private readonly ClassroomsController _controller;
    public ClassroomsControllerTests()
    {
        _classroomService = Substitute.For<IClassroomService>();
    }
    private ClassroomsController GetControllerInstance() => new(_classroomService);

    [Fact]
    public async Task GetAllClassrooms_ShouldReturnOk()
    {
        // Arrange
        List<ClassroomDTO> sampleClassrooms = new List<ClassroomDTO>{
            new ClassroomDTO
            {
                Id = 1,
                StartDate = new DateOnly(2024, 2, 23),
                CourseId = 1,
                ProgramId = 1,
                Program = null,
                Course = null,
            },
        };

        _classroomService.GetAllClassroomsAsync().Returns(Task.FromResult<IEnumerable<ClassroomDTO>>(sampleClassrooms));
        var controller = GetControllerInstance();

        // Act
        var result = await controller.GetAllClassrooms();

        // Verify that the result is an ActionResult<IEnumerable<ClassroomDTO>>
        result.Should().BeOfType<ActionResult<IEnumerable<ClassroomDTO>>>();

        // Verify that the Result of the ActionResult is an OkObjectResult
        result.Result.Should().BeOfType<OkObjectResult>();

        // Verify that result is not null
        result.Result.Should().NotBeNull();

        // Verify the status code
        (result.Result as OkObjectResult)!.StatusCode.Should().Be(200);

        // Verify that first item of the value returned is equivalent to the first item of the classrooms sample
        ((result.Result as OkObjectResult)!.Value as IEnumerable<ClassroomDTO>)?.First().Should().BeEquivalentTo(sampleClassrooms.First());
    }

    [Fact]
    public async Task CreateClassroom_Returns201Created_WithValidInput()
    {
        // Arrange
        var createClassroomDTO = new CreateClassroomDTO { /* Populate required properties */ };
        var createdClassroomDTO = new ClassroomDTO { /* Populate with expected result */ };
        _mockClassroomService.Setup(service => service.CreateClassroomAsync(createClassroomDTO))
                           .ReturnsAsync(createdClassroomDTO);

        // Act
        var result = await _controller.CreateClassroom(createClassroomDTO);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(201, actionResult.StatusCode);
        Assert.Equal(createdClassroomDTO, actionResult.Value);
    }

}
