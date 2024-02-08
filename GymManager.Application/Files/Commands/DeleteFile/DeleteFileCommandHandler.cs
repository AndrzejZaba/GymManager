using GymManager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace GymManager.Application.Files.Commands.DeleteFile;

public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IFileManagerService _fileManagerService;

    public DeleteFileCommandHandler(
        IApplicationDbContext context,
        IFileManagerService fileManagerService)
    {
        _context = context;
        _fileManagerService = fileManagerService;
    }
    public async Task<Unit> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        var fileDb = await _context.Files
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        _fileManagerService.Delete(fileDb.Name);

        _context.Files.Remove(fileDb);

        await _context.SaveChangesAsync(); 

        return Unit.Value;

    }
}
