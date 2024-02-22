using FluentAssertions;
using KlockanAPI.Application.DTOs.Country;
using KlockanAPI.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSubstitute;

namespace KlockanAPI.Presentation.Tests;

public class CountriesControllerTests
{
    private readonly ICountryService _countryService;
    private readonly Mock<ICountryService> _mockCountryService;
    private readonly CountriesController _controller;

    public CountriesControllerTests()
    {
        _countryService = Substitute.For<ICountryService>();
        _mockCountryService = new Mock<ICountryService>();
        _controller = new CountriesController(_mockCountryService.Object);
    }

    private CountriesController GetControllerInstance() => new(_countryService);

    [Fact]
    public async Task GetAll_ShouldReturnOk()
    {
        // Arrange
        var sampleCountries = new List<CountryDto>{
            new CountryDto { Id = 1, Name = "Argentina", Code = "AR" },
            new CountryDto { Id = 2, Name = "Bolivia", Code = "BO" },
            new CountryDto { Id = 3, Name = "Brazil", Code = "BR" },
            new CountryDto { Id = 4, Name = "Chile", Code = "CL" }
        };

        _countryService.GetCountries().Returns(Task.FromResult<IEnumerable<CountryDto>>(sampleCountries));
        var controller = GetControllerInstance();

        // Act
        var result = await controller.GetAllCountries();

        // Assert
        result.Should().BeOfType<ActionResult<IEnumerable<CountryDto>>>();

        result.Result.Should().BeOfType<OkObjectResult>();

        (result?.Result as OkObjectResult)?.StatusCode.Should().Be(200);

        var okResult = result?.Result as OkObjectResult;
        var coursesData = okResult?.Value as IEnumerable<CountryDto>;
        coursesData.Should().BeEquivalentTo(sampleCountries);
    }
}
