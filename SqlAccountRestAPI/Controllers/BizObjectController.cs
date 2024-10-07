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
    public class BizObjectController : ControllerBase
    {
        private readonly SqlComServer app;
        public BizObjectController(SqlComServer comServer)
        {
            app = comServer;
        }
        [HttpGet("DaysToNow")]
        public IActionResult GetByDaysToNow([FromQuery] string type="ST_ITEM", int days=0, int offset=0, int limit=0)
        {
            try
            {
                var ivHelper = new BizObject(app);
                string jsonResult = ivHelper.LoadByDaysToNow(type, days, offset, limit);
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
        [HttpGet("AllDaysToNow")]
        public IActionResult GetAllByDaysToNow([FromQuery] string type="ST_ITEM", int days=0, int offset=0, int limit=0)
        {
            try
            {
                var ivHelper = new BizObject(app);
                string jsonResult = ivHelper.LoadAllByDaysToNow(type, days, offset, limit);
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
        [HttpGet("AllDaysToNowDetail")]
        public IActionResult GetAllByDaysToNowDetail([FromQuery] string type="ST_AJ", int days=0, string dataset="cdsDocDetail", string key="DOCKEY", string param="DOCKEY", int offset=0, int limit=0)
        {
            try
            {
                var ivHelper = new BizObject(app);
                string jsonResult = ivHelper.LoadAllByDaysToNowDetail(new JObject{
                    {"type",type},
                    {"days",days},
                    {"dataset",dataset},
                    {"key",key},
                    {"param",param},
                    {"offset",offset},
                    {"limit",limit}
                });
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
        [HttpGet("QueryDetail")]
        public IActionResult GetByQueryDetail([FromQuery] string type="AR_CUSTOMER", string dataset="cdsBranch", string key="CODE", string param="CODE", int offset=0, int limit=0)
        {
            try
            {
                var ivHelper = new BizObject(app);
                string jsonResult = ivHelper.LoadByQueryDetail(new JObject{
                    {"type",type},
                    {"dataset",dataset},
                    {"key",key},
                    {"param",param},
                    {"where",""},
                    {"offset", offset},
                    {"limit", limit}
                });
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
        [HttpGet("Query")]
        public IActionResult GetByQuery([FromQuery] string type="ST_ITEM", string where="", string orderBy="", int offset=0, int limit=0)
        {
            try
            {
                var ivHelper = new BizObject(app);
                string jsonResult = ivHelper.LoadByQuery(type, where, orderBy, offset, limit);
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

        [HttpPost("Add")]
        public IActionResult Add([FromBody] JsonElement body){
            try
            {
                JObject jsonBody = Newtonsoft.Json.Linq.JObject.Parse(body.GetRawText());
                var ivHelper = new BizObject(app);
                JObject result = ivHelper.Add(jsonBody);
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
        [HttpPost("AddDetail")]
        public IActionResult AddDetail([FromBody] JsonElement body){
            try
            {
                JObject jsonBody = Newtonsoft.Json.Linq.JObject.Parse(body.GetRawText());
                var ivHelper = new BizObject(app);
                JObject result = ivHelper.AddDetail(jsonBody);
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
    }
}
