using GymManager.Application.Common.Interfaces;
using GymManager.Application.Dictionaries;
using MediatR;

namespace GymManager.Application.Contacts.Commands.SendContactEmail;

public class SendContactEmailCommandHandler :
    IRequestHandler<SendContactEmailCommand>
{
    private readonly IEmail _email;
    private readonly IAppSettingsService _appSettingsService;
    private readonly IBackgroundWorkerQueue _backgroundWorkerQueue;

    public SendContactEmailCommandHandler(
        IEmail email,
        IAppSettingsService appSettingsService,
        IBackgroundWorkerQueue backgroundWorkerQueue)
    {
        _email = email;
        _appSettingsService = appSettingsService;
        _backgroundWorkerQueue = backgroundWorkerQueue;
    }
    public async Task<Unit> Handle(SendContactEmailCommand request, 
        CancellationToken cancellationToken)
    {
        var body = $@"Nazwa: {request.Name}.<br /><br />Email nadawcy:
            {request.Email}.<br /><br />Tytuł wiadomości: {request.Title}.<br /><br />
            Wiadomość: {request.Message}<br /><br />Wysłano z: GymManager.";


        _backgroundWorkerQueue.QueueBackgroundWorkItem(async x =>
        {
            await _email.SendAsync(
            $@"Wiadomość z GymManager: {request.Title}",
            body,
            await _appSettingsService.Get(SettingsDict.AdminEmail));
            await Task.Delay(5000);
        }, $"Kontakt. E-mail: {request.Email}");

        return Unit.Value;
    }
}
