using Gip_API.Interfaces;
using Gip_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gip_API.Controllers;

[ApiController]
[Route("[controller]")]
public class ComponentController : ControllerBase
{
    [HttpGet("GetAllComponents")]
    public List<Component> GetAllComponents()
    {
        var databaseService = new DatabaseService();
        var components = databaseService.GetAllComponents();
        return components;
    }

    [HttpPost("AddComponent")]
    public bool AddComponent([FromBody] Component component)
    {
        var databaseService = new DatabaseService();
        var components = databaseService.AddComponent(component);
        return components;
    }

    [HttpPost("DeleteComponent")]
    public bool DeleteComponent([FromBody] Component component)
    {
        var databaseService = new DatabaseService();
        var components = databaseService.DeleteComponent(component);
        return components;
    }

    [HttpPost("UpdateComponent")]
    public bool RetrieveComponent([FromBody] Component component)
    {
        var databaseService = new DatabaseService();
        var components = databaseService.UpdateComponent(component);
        RetrieveService.AddNaam(component);
        return components;
    }

    [HttpGet("AlertComponents")]
    public List<Component> AlertComponents()
    {
        var databaseService = new DatabaseService();
        var components = databaseService.AlertComponents();
        return components;
    }
}