using GymManager.Application.Common.Interfaces;
using MediatR;

namespace GymManager.Application.Settings.Commands.EditSettings;

public class EditSettingsCommandHandler : IRequestHandler<EditSettingsCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IAppSettingsService _appSettingsService;
    private readonly IEmail _email;

    public EditSettingsCommandHandler(
        IApplicationDbContext context,
        IAppSettingsService appSettingsService,
        IEmail email)
    {
        _context = context;
        _appSettingsService = appSettingsService;
        _email = email;
    }
    public async Task<Unit> Handle(EditSettingsCommand request, CancellationToken cancellationToken)
    {
        foreach (var position in request.Positions)
        {
            var positionToUpdate = _context.SettingsPositions.Find(position.Id);
            positionToUpdate.Value = position.Value;
        }

        await _context.SaveChangesAsync(cancellationToken);

        await UpdateAppSettings();

        return Unit.Value;
    }

    private async Task UpdateAppSettings()
    {
        await _appSettingsService.Update(_context);
        await _email.Update(_appSettingsService); 
    }
}
