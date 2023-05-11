using Gip_API.Interfaces;

namespace Gip_API.Services;

public class RetrieveService
{
    private static readonly List<Component> _naam = new();

    public static string RetrieveComponent()
    {
        return "kanker";
    }

    public static void AddNaam(Component item)
    {
        _naam.Add(item);
    }
}