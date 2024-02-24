using FluentAssertions;
using KlockanAPI.Application;
using KlockanAPI.Application.DTOs.Classroom;
using KlockanAPI.Application.DTOs.Schedule;

public class UpdateClassroomDTOValidatorTests
{
    public UpdateClassroomDTOValidator GetValidatorInstance() => new();

    [Fact]
    public void Validate_ShouldReturnInvalid_WhenNoStartDateIsProvided()
    {
        // Arrange
        var validator = GetValidatorInstance();
        var updateClassroomDTO = new UpdateClassroomDTO();

        // Act
        var result = validator.Validate(updateClassroomDTO);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Count().Should().Be(1);
        result.Errors.First().PropertyName.Should().Be(nameof(updateClassroomDTO.StartDate));
    }

    [Fact]
    public void Validate_ShouldReturnInvalid_WhenInvalidScheduleIsProvided()
    {
        // Arrange
        var validator = GetValidatorInstance();
        var udpateScheduleDTO = new UpdateScheduleDTO();
        var updateClassroomDTO = new UpdateClassroomDTO()
        {
            StartDate = new DateOnly(2024, 2, 23),
            Schedule = new List<UpdateScheduleDTO>()
            {
                udpateScheduleDTO
            }
        };

        // Act
        var result = validator.Validate(updateClassroomDTO);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Count().Should().Be(1);
        result.Errors.First().PropertyName.Should().Contain(nameof(udpateScheduleDTO.WeekdayId));
    }
}
