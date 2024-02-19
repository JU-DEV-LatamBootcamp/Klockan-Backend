﻿
using KlockanAPI.Application.DTOs.City;

namespace KlockanAPI.Application.DTOs.Country;

public class CountryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Code { get; set; } = string.Empty;
    public IEnumerable<CityDto>? Cities { get; set; }
}
