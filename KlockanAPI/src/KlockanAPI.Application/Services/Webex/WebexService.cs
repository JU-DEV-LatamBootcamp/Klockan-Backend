using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

using KlockanAPI.Domain.Models.Webex;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KlockanAPI.Application.Services.Webex;

public class WebexService
{
    private readonly HttpClient _httpClient;
    private readonly WebexOptions _options;
    private readonly ILogger<WebexService> _logger;

    public WebexService(HttpClient httpClient, IOptions<WebexOptions> options, ILogger<WebexService> logger)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.Value.AccessToken);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _options = options.Value;
        _logger = logger;
    }

    public async Task<string> CreateMeetingAsync(Meeting meetingDetails)
    {
        var json = JsonConvert.SerializeObject(meetingDetails);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(_options.MeetingsApiUrl, content);

        if (response.IsSuccessStatusCode)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            _logger.LogInformation($"Meeting created successfully: {responseString}");
            return responseString;
        }
        else
        {
            _logger.LogError($"Failed to create meeting. Status: {response.StatusCode}");
            return null;
        }
    }
}

