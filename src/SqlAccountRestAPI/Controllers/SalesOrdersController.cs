using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SqlAccountRestAPI.Core;
using SqlAccountRestAPI.Helpers;
using SqlAccountRestAPI.ViewModels.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SqlAccountRestAPI.Controllers;

[Route("api/sales-orders")]
[ApiController]
public class SalesOrderController : ControllerBase
{
    private readonly SqlAccountSalesOrderHelper _salesOrderHelper;
    public SalesOrderController(SqlAccountSalesOrderHelper salesOrderHelper)
    {
        _salesOrderHelper = salesOrderHelper;
    }
    /// <summary>
    /// Retrieves sales orders by document number
    /// </summary>
    /// <remarks>
    /// This API fetches sales orders that match the provided document number.
    /// </remarks>
    /// <param name="documentNumber">The document number to search.</param>
    /// <param name="limit">Maximum number of results to return (default: 100).</param>
    /// <param name="offset">Offset for pagination (default: 0).</param>
    /// <response code="200">Returns the details of the business object</response>
    /// <response code="400">General error</response>
    [ProducesResponseType(typeof(SalesOrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [HttpGet("docno/{documentNumber}")]
    public IActionResult GetByDocno([FromRoute] string documentNumber = "", [FromQuery] int limit = 100, int offset = 0)
    {
        try
        {
            var result = _salesOrderHelper.GetByDocno(documentNumber, limit, offset);
            return Ok(result);
        }
        catch (Exception ex)
        {
            var errorResponse = new
            {
                error = ex.ToString(),
                code = 400
            };
            return BadRequest(errorResponse);
        }
    }
    /// <summary>
    /// Retrieves sales orders from a certain number of days ago
    /// </summary>
    /// <remarks>
    /// Fetches sales orders based on the number of days before the current date.
    /// </remarks>
    /// <param name="days">The number of days ago to search.</param>
    /// <param name="limit">Maximum number of results to return (default: 100).</param>
    /// <param name="offset">Offset for pagination (default: 0).</param>
    /// <response code="200">Returns the details of the business object</response>
    /// <response code="400">General error</response>
    [ProducesResponseType(typeof(SalesOrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [HttpGet("days-ago/{days}")]
    public IActionResult GetFromDaysAgo([FromRoute] int days = 0, [FromQuery] int limit = 100, int offset = 0)
    {
        try
        {
            var result = _salesOrderHelper.GetFromDaysAgo(days, limit, offset);
            return Ok(result);
        }
        catch (Exception ex)
        {
            var errorResponse = new
            {
                error = ex.ToString(),
                code = 400
            };
            return BadRequest(errorResponse);
        }
    }
    /// <summary>
    /// Retrieves sales orders from a specific date
    /// </summary>
    /// <remarks>
    /// Fetches sales orders recorded from a given date onward.
    /// </remarks>
    /// <param name="date">The date in YYYY-MM-DD format.</param>
    /// <param name="limit">Maximum number of results to return (default: 100).</param>
    /// <param name="offset">Offset for pagination (default: 0).</param>
    /// <response code="200">Returns the details of the business object</response>
    /// <response code="400">General error</response>
    [ProducesResponseType(typeof(SalesOrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [HttpGet("from-date/{date}")]
    public IActionResult GetFromDate([FromRoute] string date = "", [FromQuery] int limit = 100, int offset = 0)
    {
        try
        {
            var result = _salesOrderHelper.GetFromDate(date, limit, offset);
            return Ok(result);
        }
        catch (Exception ex)
        {
            var errorResponse = new
            {
                error = ex.ToString(),
                code = 400
            };
            return BadRequest(errorResponse);
        }
    }

    
}