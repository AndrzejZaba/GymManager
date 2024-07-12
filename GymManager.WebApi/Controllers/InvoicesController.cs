using GymManager.Application.Invoices.Commands.AddInvoice;
using GymManager.Application.Invoices.Commands.DeleteInvoice;
using GymManager.Application.Invoices.Commands.EditInvoice;
using GymManager.Application.Invoices.Queries.GetInvoice;
using GymManager.Application.Invoices.Queries.GetInvoices;
using GymManager.Application.Invoices.Queries.GetPdfInvoice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.WebApi.Controllers;

[ApiVersion("1")]
[ApiExplorerSettings(GroupName = "v1")]
[Authorize]
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

    [MapToApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    [HttpGet]
    public async Task<IActionResult> GetAllv2()
    {
        var invoices = await Mediator.Send(new GetInvoicesQuery
        {
            UserId = UserId
        });

        return Ok(new List<InvoiceBasicsDto> { new InvoiceBasicsDto 
            {
                Id = 100,
                CreatedDate = DateTime.Now,
                Title = "1",
                UserId = "1",
                UserName = "TEST",
                Value = 1
            } 
        });

    }

    [HttpPost]
    public async Task<IActionResult> Add(AddInvoiceCommand command)
    {
        command.UserId = UserId;

        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Edit(EditInvoiceCommand command)
    {
        command.UserId = UserId;
        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(DeleteInvoiceCommand command)
    {
        command.UserId = UserId;
        await Mediator.Send(command);

        return NoContent();
    }

    [HttpGet("pdf/{id}")]
    public async Task<IActionResult> GetPdf(int id)
    {
        var vm = await Mediator.Send(new GetPdfInvoiceQuery
        {
            UserId = UserId,
            InvoiceId = id,
            Context = ControllerContext
        });

        if (vm == null)
            return NotFound();

        return Ok(vm);

    }
}
