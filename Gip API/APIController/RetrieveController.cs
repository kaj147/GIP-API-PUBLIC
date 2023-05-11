using Gip_API.Interfaces;
using Gip_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gip_API.Controllers;

[ApiController]
[Route("[controller]")]

public class RetrieveController : ControllerBase
{
    [HttpGet("RetrieveComponents")]
    public List<Component> RetrieveComponent()
    {
        return RetrieveService.RetrieveComponent();
    }   

    [HttpPost("State")]
    public void SetState([FromBody] AGVState state)
    {
        ServiceHandler.state = state;   
    }

    [HttpGet("State")]
    public AGVState GetState()
    {
        return ServiceHandler.state;
    }
}

