using KlockanAPI.Application.Services.Interfaces;
using KlockanAPI.Application.Services.Webex;
using Microsoft.Extensions.DependencyInjection;

namespace KlockanAPI.Application.Services;

public static class MeetingServiceFactory
{
    public static IThirdPartyMeeting CreateMeetingService(string serviceType, IServiceProvider serviceProvider)
    {
        switch (serviceType)
        {
            case "Webex":
                return serviceProvider.GetRequiredService<WebexMeetingAdapter>();
            default:
                throw new ArgumentException($"Unrecognized service type: {serviceType}", nameof(serviceType));
        }
    }
}

