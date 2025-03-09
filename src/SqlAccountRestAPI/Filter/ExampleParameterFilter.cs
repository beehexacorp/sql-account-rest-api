using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Any;
using System.Reflection;

public class ExampleParameterFilter : IParameterFilter
{
    public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
    {
        var declaringType = context.ParameterInfo?.Member.DeclaringType;

        if (parameter.Schema != null) 
        {
            if (parameter.Name == "code")
            {
                if (declaringType != null)
                {
                    if (declaringType.Name.Equals("CustomerController", StringComparison.OrdinalIgnoreCase))
                    {
                        parameter.Schema.Example = new OpenApiString("300-A0001");
                    }
                    else if (declaringType.Name.Equals("StockItemController", StringComparison.OrdinalIgnoreCase))
                    {
                        parameter.Schema.Example = new OpenApiString("SEMI BOM");
                    }
                    else if (declaringType.Name.Equals("StockItemController", StringComparison.OrdinalIgnoreCase))
                    {
                        parameter.Schema.Example = new OpenApiString("SEMI BOM");
                    }
                }
            }
            if (parameter.Name == "documentNumber")
            {
                if (declaringType != null)
                {
                    if (declaringType.Name.Equals("CustomerInvoiceController", StringComparison.OrdinalIgnoreCase))
                    {
                        parameter.Schema.Example = new OpenApiString("IV-00001");
                    }
                    else if (declaringType.Name.Equals("CustomerPaymentController", StringComparison.OrdinalIgnoreCase))
                    {
                        parameter.Schema.Example = new OpenApiString("CS-00001");
                    }
                    else if (declaringType.Name.Equals("SalesInvoiceController", StringComparison.OrdinalIgnoreCase))
                    {
                        parameter.Schema.Example = new OpenApiString("IV-00001");
                    }
                    else if (declaringType.Name.Equals("SalesOrderController", StringComparison.OrdinalIgnoreCase))
                    {
                        parameter.Schema.Example = new OpenApiString("SO-00001");
                    }
                    else if (declaringType.Name.Equals("StockAdjustmentController", StringComparison.OrdinalIgnoreCase))
                    {
                        parameter.Schema.Example = new OpenApiString("AJ-00001");
                    }
                }
            }
            else if (parameter.Name == "limit")
            {
                parameter.Schema.Example = new OpenApiInteger(1);
            }
            else if (parameter.Name == "offset")
            {
                parameter.Schema.Example = new OpenApiInteger(0);

            }
            else if (parameter.Name == "days")
            {
                parameter.Schema.Example = new OpenApiInteger(1000);

            }
            else if (parameter.Name == "date")
            {
                parameter.Schema.Example = new OpenApiString("2020-01-01");

            }
            else if (parameter.Name == "bizObjectName")
            {
                parameter.Schema.Example = new OpenApiString("AR_CUSTOMER");

            }
            else if (parameter.Name == "email")
            {
                parameter.Schema.Example = new OpenApiString("delta@ahome.com");

            }
            else if (parameter.Name == "entityType")
            {
                parameter.Schema.Example = new OpenApiString("STOCK_ITEM");

            }
            else if (parameter.Name == "fieldKey")
            {
                parameter.Schema.Example = new OpenApiString("CODE");

            }
            else if (parameter.Name == "fieldValue")
            {
                parameter.Schema.Example = new OpenApiString("COVER");

            }
            else if (parameter.Name == "email")
            {
                parameter.Schema.Example = new OpenApiString("delta@ahome.com");

            }

        }
    }
}
