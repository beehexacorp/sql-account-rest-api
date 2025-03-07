using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SqlAccountRestAPI.Core;
using SqlAccountRestAPI.Helpers;
using SqlAccountRestAPI.ViewModels.Responses;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Annotations;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SqlAccountRestAPI.Controllers;

[Route("api/stock-item-templates")]
[ApiController]
public class StockItemTemplateController : ControllerBase
{
    private readonly SqlAccountStockItemTemplateHelper _stockItemTemplateHelper;
    public StockItemTemplateController(SqlAccountStockItemTemplateHelper stockItemTemplateHelper)
    {
        _stockItemTemplateHelper = stockItemTemplateHelper;
    }

    /// <summary>
    /// Retrieves stock item templates by code
    /// </summary>
    /// <remarks>
    /// This API fetches stock item templates that match the provided code.
    /// </remarks>
    /// <param name="code">The code to search.</param>
    /// <param name="limit">Maximum number of results to return (example: 100).</param>
    /// <param name="offset">Offset for pagination (default: 0).</param>
    /// <response code="200">Returns the details of the business object</response>
    /// <response code="400">General error</response>
    [ProducesResponseType(typeof(StockItemTemplateResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [HttpGet("code/{code}")]
    public IActionResult GetByDocno(
        [FromRoute] 
        string code = "", 
        [FromQuery] 
        int limit = 100, 
        int offset = 0
    )
    {
        try
        {
            var result = _stockItemTemplateHelper.GetByCode(code, limit, offset);
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
    /// Retrieves stock item templates from a certain number of days ago
    /// </summary>
    /// <remarks>
    /// Fetches stock item templates based on the number of days before the current date.
    /// </remarks>
    /// <param name="days">The number of days ago to search.</param>
    /// <param name="limit">Maximum number of results to return (default: 100).</param>
    /// <param name="offset">Offset for pagination (default: 0).</param>
    /// <response code="200">Returns the details of the business object</response>
    /// <response code="400">General error</response>
    [ProducesResponseType(typeof(StockItemTemplateResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [HttpGet("days-ago/{days}")]
    public IActionResult GetFromDaysAgo([FromRoute] int days = 0, [FromQuery] int limit = 100, int offset = 0)
    {
        try
        {
            var result = _stockItemTemplateHelper.GetFromDaysAgo(days, limit, offset);
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
    /// Retrieves stock item templates from a specific date
    /// </summary>
    /// <remarks>
    /// Fetches stock item templates recorded from a given date onward.
    /// </remarks>
    /// <param name="date">The date in YYYY-MM-DD format.</param>
    /// <param name="limit">Maximum number of results to return (default: 100).</param>
    /// <param name="offset">Offset for pagination (default: 0).</param>
    /// <response code="200">Returns the details of the business object</response>
    /// <response code="400">General error</response>
    [ProducesResponseType(typeof(StockItemTemplateResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    [HttpGet("from-date/{date}")]
    public IActionResult GetFromDate([FromRoute] string date = "", [FromQuery] int limit = 100, int offset = 0)
    {
        try
        {
            var result = _stockItemTemplateHelper.GetFromDate(date, limit, offset);
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