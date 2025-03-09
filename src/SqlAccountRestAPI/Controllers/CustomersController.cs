using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SqlAccountRestAPI.Core;
using SqlAccountRestAPI.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SqlAccountRestAPI.Controllers;

[Route("api/customers")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly SqlAccountCustomerHelper _customerHelper;
    private readonly SqlAccountVersionHelper _sqlAccountVersionHelper;
    public CustomerController(SqlAccountCustomerHelper customerHelper, SqlAccountVersionHelper sqlAccountVersionHelper)
    {
        _customerHelper = customerHelper;
        _sqlAccountVersionHelper = sqlAccountVersionHelper;
    }
    /// <summary>
    /// Retrieves customers by customer email
    /// </summary>
    /// <remarks>
    /// This API fetches customers that match the provided customer email.
    /// </remarks>
    /// <param name="email">The customer email to search.</param>
    /// <param name="limit">Maximum number of results to return (default: 100).</param>
    /// <param name="offset">Offset for pagination (default: 0).</param>
    /// <response code="200">Returns the details of the business object</response>
    /// <response code="400">General error</response>
    [Produces("application/json")]
    [HttpGet("email/{email}")]
    // TODO: validate email
    public IActionResult GetByEmail([FromRoute] string email = "", [FromQuery] int limit = 100, int offset = 0)
    {
        try
        {
            var result = _customerHelper.GetByEmail(email, limit, offset);
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

    // [HttpPost("payment")]
    // public IActionResult Payment([FromBody] AddCustomerPaymentRequest request)
    // {
    //     try
    //     {
    //         var sqlAccountVersion = _sqlAccountVersionHelper.GetRunningVersion();
    //         var acceptedParams = _sqlAccountVersionHelper.GetConfig(sqlAccountVersion, "CUSTOMER", "AddPayment");
    //         var result = SystemHelper.InvokeMethod(_customerHelper, "AddPayment", request, acceptedParams);
    //         return Ok(result);
    //     }
    //     catch (Exception ex)
    //     {
    //         var errorResponse = new
    //         {
    //             error = ex.ToString(),
    //             code = 400
    //         };
    //         return BadRequest(errorResponse);
    //     }
    // }
    
    /// <summary>
    /// Retrieves customers by customer code
    /// </summary>
    /// <remarks>
    /// This API fetches customers that match the provided customer code.
    /// It supports pagination through `limit` and `offset` parameters.
    /// </remarks>
    /// <param name="code">The customer code to search.</param>
    /// <param name="limit">Maximum number of results to return (default: 100).</param>
    /// <param name="offset">Offset for pagination (default: 0).</param>
    /// <response code="200">Returns the details of the business object</response>
    /// <response code="400">General error</response>
    [Produces("application/json")]
    [HttpGet("code/{code}")]
    public IActionResult GetByCode([FromRoute] string code = "", [FromQuery] int limit = 100, int offset = 0)
    {
        try
        {
            var result = _customerHelper.GetByCode(code, limit, offset);
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
    /// Retrieves customers from a certain number of days ago
    /// </summary>
    /// <remarks>
    /// Fetches customers based on the number of days before the current date.
    /// </remarks>
    /// <param name="days">The number of days ago to search.</param>
    /// <param name="limit">Maximum number of results to return (default: 100).</param>
    /// <param name="offset">Offset for pagination (default: 0).</param>
    /// <response code="200">Returns the details of the business object</response>
    /// <response code="400">General error</response>
    [Produces("application/json")]
    [HttpGet("days-ago/{days}")]
    public IActionResult GetFromDaysAgo([FromRoute] int days = 0, [FromQuery] int limit = 100, int offset = 0)
    {
        try
        {
            var result = _customerHelper.GetFromDaysAgo(days, limit, offset);
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
    /// Retrieves customers from a specific date
    /// </summary>
    /// <remarks>
    /// Fetches customers recorded from a given date onward.
    /// </remarks>
    /// <param name="date">The date in YYYY-MM-DD format.</param>
    /// <param name="limit">Maximum number of results to return (default: 100).</param>
    /// <param name="offset">Offset for pagination (default: 0).</param>
    /// <response code="200">Returns the details of the business object</response>
    /// <response code="400">General error</response>
    [Produces("application/json")]
    [HttpGet("from-date/{date}")]
    public IActionResult GetFromDate([FromRoute] string date = "", [FromQuery] int limit = 100, int offset = 0)
    {
        try
        {
            var result = _customerHelper.GetFromDate(date, limit, offset);
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