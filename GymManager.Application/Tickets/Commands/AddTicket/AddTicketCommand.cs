using MediatR;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GymManager.Application.Tickets.Commands.AddTicket;

public class AddTicketCommand : IRequest<string>
{
    [Required(ErrorMessage = "FieldStartDateTicketIsRequired")]
    [DisplayName("StartDateTicket")]
    public DateTime StartDate { get; set; }

    [DisplayName("Price")]
    public decimal Price { get; set; }

    [DisplayName("TypeOfTicket")]
    public int TicketTypeId { get; set; }
    public string UserId { get; set; }
}
