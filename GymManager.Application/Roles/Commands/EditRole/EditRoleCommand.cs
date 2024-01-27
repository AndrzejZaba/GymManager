using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.Application.Roles.Commands.EditRole;

public class EditRoleCommand : IRequest
{
    public string Id { get; set; }
    
    [Required(ErrorMessage = "Pole 'Nazwa' jest wymagane.")]
    [DisplayName("Nazwa")]
    public string Name { get; set; }
}
