using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SqlAccountRestAPI.Core;
using SqlAccountRestAPI.Helpers;

namespace SqlAccountRestAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public partial class AppController : ControllerBase
{

    private readonly SqlAccountAppHelper _app;
    private readonly SqlAccountORM _microORM;
    private readonly ILogger<AppController> _logger;

    public AppController(SqlAccountAppHelper app, SqlAccountORM microORM, ILogger<AppController> logger)
    {
        _app = app;
        _microORM = microORM;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> GetLogin([FromBody] LoginRequest request)
    {
        _microORM.Login(request.Username, request.Password);
        return Ok(await _app.GetInfo());
    }

    // GET: api/<AppController>
    [HttpGet("info")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _app.GetInfo());
    }
    [HttpGet("actions")]
    public IActionResult GetActions()
    {
        return Ok(_app.GetActions());
    }

    [HttpGet("modules")]
    public IActionResult GetModules()
    {
        return Ok(_app.GetModules());
    }
    [HttpGet("objects")]
    public IActionResult GetBizObjects()
    {
        return Ok(_app.GetBizObjects());
    }

    [HttpGet("objects/{name}")]
    public IActionResult GetBizObjectInfo(string name)
    {
        return Ok(_app.GetBizObjectInfo(name));
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update()
    {
        return Ok(await _app.Update());
    }
}