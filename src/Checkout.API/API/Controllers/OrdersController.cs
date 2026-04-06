using System.Net;
using Checkout.API.API.Filters;
using Checkout.API.Application.Commands;
using Checkout.API.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.API.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(CreateOrderCommandHandler createOrderHandler) : ControllerBase
{
    [HttpPost]
    [IdempotencyFilter]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestDto request, CancellationToken cancellationToken)
    {
        var orderId = await createOrderHandler.Handle(request, cancellationToken);

        return StatusCode((int)HttpStatusCode.Created,
            new { OrderId = orderId, Message = "Pedido recebido e em processamento assíncrono." });
    }
}