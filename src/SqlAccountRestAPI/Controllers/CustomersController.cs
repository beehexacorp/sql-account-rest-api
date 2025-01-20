using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
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
    public CustomerController(SqlAccountCustomerHelper customerHelper)
    {
        _customerHelper = customerHelper;
    }
    // [HttpGet("AllDaysToNow")]
    // public IActionResult GetByDaysToNow([FromQuery] int days = 0)
    // {
    //     try
    //     {
    //         var ivHelper = new Customer(_customerHelper);
    //         string jsonResult = ivHelper.LoadAllByDaysToNow(days);
    //         return Ok(jsonResult);
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

    [HttpPost("payment")]
    public IActionResult Payment([FromBody] AddCustomerPaymentRequest request)
    {
        try
        {
            var result = _customerHelper.AddPayment(
                request.DocumentNo,
                request.PaymentMethod,
                request.Project);
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