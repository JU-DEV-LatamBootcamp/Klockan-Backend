
namespace KlockanAPI.Infrastructure.Extensions;

public static class ListExtensions
{
    public static IEnumerable<T> FilterTarget<T>(this IEnumerable<T> master, IEnumerable<T> target, MatchDelegate<T> matchFunc, bool match = true)
    {
        return target
            .Where(masterElement => match ?
                master.Any(targetElement => matchFunc(targetElement, masterElement)) :
                !master.Any(targetElement => matchFunc(targetElement, masterElement))
            )
            .ToList();
    }

    public static IEnumerable<T> FilterByTarget<T>(this IEnumerable<T> master, IEnumerable<T> target, MatchDelegate<T> matchFunc, bool match = true)
    {
        return master
            .Where(targetElement => match ?
                target.Any(masterElement => matchFunc(targetElement, masterElement)) :
                !target.Any(masterElement => matchFunc(targetElement, masterElement))
            )
            .ToList();
    }
}

public delegate bool MatchDelegate<T>(T targetElement, T element);
