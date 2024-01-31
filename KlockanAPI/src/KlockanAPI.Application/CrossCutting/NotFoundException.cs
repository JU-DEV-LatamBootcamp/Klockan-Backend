namespace KlockanAPI.Application.CrossCutting;

public class NotFoundException : ArgumentException
{
    public NotFoundException(string? message) : base(message) { }

    public static void ThrowIfNull<T>(T? param, string? message = null)
    {
        if(param is null)
        {
            throw new NotFoundException(message ?? "Resource was not found");
        }
    }
}
