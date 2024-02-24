using GymManager.Application.Settings.Queries.Dtos;
using GymManager.Application.Settings.Queries.GetSettings;
using GymManager.Domain.Entities;
using System.Linq;

namespace GymManager.Application.Settings.Extensions;

public static class SettingsExtensions
{

    public static SettingsPositionDto ToDto(this SettingsPosition position)
    {
        if (position == null)
            return null;

        return new SettingsPositionDto
        {
            Id = position.Id,
            Description = position.Description,
            Key = position.Key,
            Type = position.Type,
            Value = position.Value,
        };
    }
    
    
    public static SettingsDto ToDto(this Domain.Entities.Settings settings)
    {
        if (settings == null)
            return null;

        return new SettingsDto
        {
            Id = settings.Id,
            Description = settings.Description,
            Order = settings.Order,
            Positions = settings.Positions.Select(x => x.ToDto())?.ToList()
        };
    }
}
