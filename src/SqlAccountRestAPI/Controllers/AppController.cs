using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SqlAccountRestAPI.Core;
using SqlAccountRestAPI.Helpers;
using SqlAccountRestAPI.ViewModels.Responses;

namespace SqlAccountRestAPI.Controllers;

[Route("api/app")]
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

    /// <summary>
    /// Authenticates a user and retrieves their account info
    /// </summary>
    /// <remarks>
    /// This endpoint allows users to authenticate with their credentials. If authentication is successful, user information is returned.
    /// </remarks>
    /// <response code="200">Returns application information</response>
    /// <response code="400">General error</response>
    [ProducesResponseType(typeof(AppInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("login")]
    public async Task<IActionResult> GetLogin([FromBody] LoginRequest request)
    {
        _microORM.Login(request.Username, request.Password);
        return Ok(await _app.GetInfo());
    }

    /// <summary>
    /// Retrieves application information
    /// </summary>
    /// <remarks>
    /// This API returns general information about the application.
    /// </remarks>
    /// <response code="200">Returns application information</response>
    /// <response code="400">General error</response>
    [ProducesResponseType(typeof(AppInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [HttpGet("info")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _app.GetInfo());
    }

    /// <summary>
    /// Retrieves available user actions
    /// </summary>
    /// <remarks>
    /// This endpoint returns a list of actions that users can perform.
    /// </remarks>
    /// <response code="200">Returns application action names</response>
    /// <response code="400">General error</response>
    [ProducesResponseType(typeof(AppActionsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [HttpGet("actions")]
    public IActionResult GetActions()
    {
        return Ok(_app.GetActions());
    }

    /// <summary>
    /// Retrieves available system modules
    /// </summary>
    /// <remarks>
    /// This API provides details about the available system modules.
    /// </remarks>
    /// <response code="200">Returns application module names</response>
    /// <response code="400">General error</response>
    [ProducesResponseType(typeof(AppModulesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [HttpGet("modules")]
    public IActionResult GetModules()
    {
        return Ok(_app.GetModules());
    }

    /// <summary>
    /// Retrieves business objects
    /// </summary>
    /// <remarks>
    /// This endpoint returns all available business objects in the system.
    /// </remarks>
    /// <response code="200">Returns application business object names</response>
    /// <response code="400">General error</response>
    [ProducesResponseType(typeof(AppBizObjectsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [HttpGet("biz-objects")]
    public IActionResult GetBizObjects()
    {
        return Ok(_app.GetBizObjects());
    }

    /// <summary>
    /// Retrieves details of a specific business object
    /// </summary>
    /// <param name="bizObjectName">The name of the business object</param>
    /// <remarks>
    /// This API returns detailed information about a given business object.
    /// </remarks>
    /// <response code="200">Returns application business object details</response>
    /// <response code="400">General error</response>
    [ProducesResponseType(typeof(AppBizObjectInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [HttpGet("biz-objects/{bizObjectName}")]
    public IActionResult GetBizObjectInfo(string bizObjectName)
    {
        return Ok(_app.GetBizObjectInfo(bizObjectName));
    }

    /// <summary>
    /// Updates the application
    /// </summary>
    /// <remarks>
    /// This API triggers an update process for the application.
    /// </remarks>
    /// <response code="200">Returns application update status</response>
    /// <response code="400">General error</response>
    [ProducesResponseType(typeof(AppUpdateResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [HttpPost("update")]
    public async Task<IActionResult> Update()
    {
        return Ok(await _app.Update());
    }
}
