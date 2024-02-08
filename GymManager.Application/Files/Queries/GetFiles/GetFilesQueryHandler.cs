using GymManager.Application.Common.Interfaces;
using GymManager.Application.Files.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Application.Files.Queries.GetFiles;

public class GetFilesQueryHandler : IRequestHandler<GetFilesQuery, IEnumerable<FileDto>>
{
    private readonly IApplicationDbContext _context;

    public GetFilesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FileDto>> Handle(GetFilesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Files
            .AsNoTracking()
            .OrderByDescending(x => x.Id)
            .Select(x => x.ToDto())
            .ToListAsync();
    }
}
