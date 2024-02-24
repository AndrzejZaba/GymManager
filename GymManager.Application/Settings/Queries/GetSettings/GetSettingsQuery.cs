using MediatR;


namespace GymManager.Application.Settings.Queries.GetSettings;

public class GetSettingsQuery : IRequest<IList<SettingsDto>>
{
}
