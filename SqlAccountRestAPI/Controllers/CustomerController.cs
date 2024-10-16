using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SqlAccountRestAPI.Lib;
using SqlAccountRestAPI.Models;
using StockItem = SqlAccountRestAPI.Lib.StockItem;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SqlAccountRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly SqlComServer app;
        public CustomerController(SqlComServer comServer)
        {
            app = comServer;
        }
        [HttpGet("AllDaysToNow")]
        public IActionResult GetByDaysToNow([FromQuery] int days=0)
        {
            try
            {
                var ivHelper = new Customer(app);
                string jsonResult = ivHelper.LoadAllByDaysToNow(days);
                return Ok(jsonResult);
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
        [HttpGet("Email")]
        public IActionResult GetByEmail([FromQuery] string email="")
        {
            try
            {
                var ivHelper = new Customer(app);
                string jsonResult = ivHelper.LoadByEmail(email);
                return Ok(jsonResult);
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
        [HttpPost("Payment")]
        public IActionResult Payment([FromBody] JsonElement body){
             try
            {
                JObject jsonBody = Newtonsoft.Json.Linq.JObject.Parse(body.GetRawText());
                var ivHelper = new Customer(app);
                JObject result = ivHelper.Payment(jsonBody);
                jsonBody["Result"] = result;
                return Ok(jsonBody.ToString());
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
        [HttpPost("test")]
        public IActionResult test([FromBody] JsonElement body){
             try
            {
                JObject jsonBody = Newtonsoft.Json.Linq.JObject.Parse(body.GetRawText());
                var ivHelper = new Customer(app);
                ivHelper.Test(jsonBody);
                return Ok(jsonBody.ToString());
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
}