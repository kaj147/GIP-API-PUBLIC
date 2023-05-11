using Gip_API.Interfaces;

namespace Gip_API.Services;

public class ServiceHandler
{
    public static DatabaseService? DatabaseService;
    public static RetrieveService? RetrieveService;

    public static AGVState state = AGVState.CACHING;

    public ServiceHandler()
    {
        DatabaseService = new DatabaseService();
        RetrieveService = new RetrieveService();

        state = AGVState.WAITING;
    }
}