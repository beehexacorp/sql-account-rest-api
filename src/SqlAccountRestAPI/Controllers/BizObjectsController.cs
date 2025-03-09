using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SqlAccountRestAPI.Core;
using SqlAccountRestAPI.Helpers;

namespace SqlAccountRestAPI.Controllers;

[Route("api/biz-objects")]
[ApiController]
public partial class BizObjectController : ControllerBase
{
    private readonly SqlAccountBizObjectHelper _bizObject;
    public BizObjectController(SqlAccountBizObjectHelper bizObject)
    {
        _bizObject = bizObject;
    }

    /// <summary>
    /// Executes a query on business objects
    /// </summary>
    /// <param name="request">Query request parameters</param>
    /// <remarks>
    /// This endpoint allows querying business objects using a raw SQL statement with optional parameters.
    /// It supports pagination via offset and limit.
    /// </remarks>    
    /// <response code="200">Returns the queried business object</response>
    /// <response code="400">General error</response>
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("query")]
    public IActionResult GetByQuery([FromBody] BizObjectQueryRequest request)
    {
        var results = _bizObject.Query(request.Sql, request.Offset, request.Limit);
        return Ok(results);
    }
    /// <summary>
    /// Adds a new business object
    /// </summary>
    /// <param name="entityType">Type of the business object</param>
    /// <param name="request">Business object data</param>
    /// <remarks>
    /// This endpoint creates a new business object of the specified entity type.
    /// The request body must contain valid business object data.
    /// </remarks>    
    /// <response code="200">Returns the details of the newly added business object</response>
    /// <response code="400">General error</response>
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("{entityType}")]
    public IActionResult Add(string entityType, [FromBody] BizObjectRequest request)
    {
        var result = _bizObject.AddDetail(entityType, request.Data);
        return Ok(result);
    }

    /// <summary>
    /// Updates an existing business object
    /// </summary>
    /// <param name="entityType">The type of business object</param>
    /// <param name="fieldKey">Field key used to identify the object</param>
    /// <param name="fieldValue">Field value corresponding to the key</param>
    /// <param name="request">Updated business object data</param>
    /// <remarks>
    /// This endpoint updates a business object by matching the specified field key and value.
    /// The request body should contain the updated data.
    /// </remarks>    
    /// <response code="200">Returns the updated business object details</response>
    /// <response code="400">General error</response>
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPut("{entityType}/{fieldKey}/{fieldValue}")]
    public IActionResult Update(string entityType, string fieldKey, string fieldValue, [FromBody] BizObjectUpdateRequest request)
    {
        var result = _bizObject.Update(entityType, request.MainKey, fieldKey, fieldValue, request.Data);
        return Ok(result);
    }

    /// <summary>
    /// Transfers a business object from one entity type to another
    /// </summary>
    /// <param name="request">Transfer details</param>
    /// <remarks>
    /// This endpoint allows transferring a business object from one entity type to another.
    /// It requires the source entity type, target entity type, and document number for transfer.
    /// </remarks>    
    /// <response code="200">Returns the details of the transferred business object</response>
    /// <response code="400">General error</response>
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("transfer")]
    public IActionResult Transfer([FromBody] BizObjectTransferRequest request)
    {
        var result = _bizObject.Transfer(request.FromEntityType, request.ToEntityType, request.DocNo);
        return Ok(result);
    }
}
