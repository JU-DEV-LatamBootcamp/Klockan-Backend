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

    public async Task<MeetingReport> GetMeetingReportAsync(string meetingId)
    {                
        var response = await _httpClient.GetAsync("https://webexapis.com/v1/meetingParticipants?meetingId=6aaf4dad853543049f9f47e9ba36d4df");

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            
            var meetingReport = JsonConvert.DeserializeObject<MeetingReport>(responseContent);                                   

            if (meetingReport != null)
            {
                foreach (ParticipantReport participant in meetingReport.items)
                {
                    participant.DurationInSeconds = participant.leftTime.Subtract(participant.joinedTime).TotalSeconds.ToString();
                }
                return meetingReport;
            }
            else
            {
                throw new InvalidOperationException("The report was created but it turned empty.");
            }            
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error creating the meeting in Webex. Status code: {response.StatusCode}, Response: {errorContent}");
        }        
    }
}
