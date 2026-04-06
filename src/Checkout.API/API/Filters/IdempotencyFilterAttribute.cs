using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StackExchange.Redis;

namespace Checkout.API.API.Filters;

[AttributeUsage(AttributeTargets.Method)]
public class IdempotencyFilterAttribute : ActionFilterAttribute
{
    private const string IdempotencyHeaderName = "x-idempotency-key";

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(IdempotencyHeaderName, out var idempotencyKey))
        {
            context.Result = new BadRequestObjectResult(
                new {Error = "Cabeçalho X-Idempotency-Key é obrigatório para transações de compra"});
            return;
        }
        
        var key = idempotencyKey.ToString();
        
        var connectionMultiplexer = context.HttpContext.RequestServices.GetRequiredService<IConnectionMultiplexer>();
        var redisDb = connectionMultiplexer.GetDatabase();
        
        var cachedResult = await redisDb.StringGetAsync(key);
        if (cachedResult.HasValue)
        {
            context.Result = new OkObjectResult(new {Message = "Está requisição já foi processada anteriormente.", 
                CachedResult = cachedResult.ToString()});
            return;
        }
        
        var executedContext = await next();

        if (executedContext.Exception == null && executedContext.Result is OkObjectResult result &&
            (result.StatusCode is StatusCodes.Status200OK or StatusCodes.Status201Created))
        {
            var responseJson = JsonSerializer.Serialize(result.Value);
            await redisDb.StringSetAsync(key, responseJson, TimeSpan.FromHours(24));
        }
    }
}