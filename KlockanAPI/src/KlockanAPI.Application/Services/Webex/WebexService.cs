using KlockanAPI.Domain.Models.Webex;
using Newtonsoft.Json;
using System.Text;

namespace KlockanAPI.Application.Services.Webex;
public class WebexService
{
    private readonly HttpClient _httpClient;

    public WebexService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> CreateMeetingAsync(Meeting meetingDetails)
    {
        var jsonContent = JsonConvert.SerializeObject(meetingDetails);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(_httpClient.BaseAddress, content);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();

            var meetingResponse = JsonConvert.DeserializeObject<MeetingResponse>(responseContent);

            if (meetingResponse != null && !string.IsNullOrWhiteSpace(meetingResponse.Id))
            {
                return meetingResponse.Id;
            }
            else
            {
                throw new InvalidOperationException("The meeting was created, but the response does not have the expected format.");
            }
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error creating the meeting in Webex. Status code: {response.StatusCode}, Response: {errorContent}");
        }
    }
}
