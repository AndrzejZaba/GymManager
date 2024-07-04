using GymManager.Application.Invoices.Queries.GetInvoice;
using GymManager.Application.Invoices.Queries.GetInvoices;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.WebApi.Controllers;


public class InvoicesController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var invoices = await Mediator.Send(new GetInvoicesQuery
        {
            UserId = UserId
        });

        return Ok(invoices);

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var invoice = await Mediator.Send(new GetInvoiceQuery
        {
            Id = id,
            UserId = UserId
        });

        if(invoice == null)
            return NotFound();

        return Ok(invoice);

    }
}
