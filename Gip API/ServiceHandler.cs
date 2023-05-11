using Gip_API.Interfaces;
using Gip_API.Services;

namespace Gip_API;

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