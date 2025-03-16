using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace SqlAccountRestAPI.Middleware;
public class CustomApiMiddleware
{
    private readonly RequestDelegate _next;

    public CustomApiMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task<Dictionary<string, object>> GetRequestBody(HttpContext context)
    {
        // Read request body
        context.Request.EnableBuffering();
        using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
        var requestBody = await reader.ReadToEndAsync();
        context.Request.Body.Position = 0;

        // Deserialize request body
        var requestData = JsonSerializer.Deserialize<Dictionary<string, object>>(requestBody);
        if (requestData == null || !requestData.ContainsKey("data"))
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Invalid request format");
            return [];
        }

        var requestDataValues = JsonSerializer.Deserialize<Dictionary<string, object>>(requestData["data"].ToString()!);
        return requestDataValues!;
    }
    public async Task HandleBizObjectDictionary(HttpContext context, string entityType)
    {
        var requestDataValues = await GetRequestBody(context);
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Middleware", "CustomApiResponseTemplates", $"{entityType}.json");
        string jsonString = File.ReadAllText(filePath);
        var templateDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonString);

        if (templateDictionary != null)
        {
            foreach (var kvp in requestDataValues)
            {
                if (templateDictionary.ContainsKey(kvp.Key))
                {
                    templateDictionary[kvp.Key] = kvp.Value;
                }
            }
        }
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(templateDictionary));
        return;

    }
    public async Task InvokeAsync(HttpContext context)
    {
        List<string> entityTypeList = ["AR_CUSTOMER", "AR_PM", "AR_IV",
            "SL_IV", "SL_CS", "SL_SO",
            "ST_AJ", "ST_ITEM", "ST_ITEM_TPL"];
        foreach (var entity in entityTypeList)
        {
            if (context.Request.Path.StartsWithSegments($"/api/biz-objects/{entity}") && context.Request.Method == "POST")
            {
                await HandleBizObjectDictionary(context, entity);
                return;
            }
        }
        if (context.Request.Path.StartsWithSegments("/api/app/update") && context.Request.Method == "POST")
        {
            var responseObj = new { status = "Update process started. Service will restart soon." };
            string jsonResponse = JsonSerializer.Serialize(responseObj);

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(jsonResponse);
            return;
        }

        await _next(context);
    }

}