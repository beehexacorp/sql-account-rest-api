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

[Route("api/[controller]")]
[ApiController]
public class StockAdjustmentController : ControllerBase
{
    private readonly SqlAccountingStockAdjustmentHelper _stockAdjustmentHelper;
    public StockAdjustmentController(SqlAccountingStockAdjustmentHelper stockAdjustmentHelper)
    {
        _stockAdjustmentHelper = stockAdjustmentHelper;
    }

    [HttpGet("docno/{documentNumber}")]
    public IActionResult GetByDocno([FromRoute] string documentNumber = "")
    {
        try
        {
            var result = _stockAdjustmentHelper.GetByDocno(documentNumber);
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