using GymManager.Application.Files.Queries.GetFiles;

namespace GymManager.Application.Files.Extensions;

public static class FileExtensions
{
    public static FileDto ToDto(this Domain.Entities.File file)
    {
        if (file == null)
            return null;

        return new FileDto
        {
            Id = file.Id,
            Name = file.Name,
            Bytes = file.Bytes,
            Description = file.Description,
            Url = $"/Content/Files/{file.Name}"
        };
    }
}
