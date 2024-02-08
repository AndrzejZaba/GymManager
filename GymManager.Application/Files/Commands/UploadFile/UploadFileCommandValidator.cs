using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace GymManager.Application.Files.Commands.UploadFile;

public class UploadFileCommandValidator : AbstractValidator<UploadFileCommand>
{
    public UploadFileCommandValidator()
    {
        RuleForEach(x => x.Files).SetValidator(new IFormValidator());    
    }
}

public class IFormValidator : AbstractValidator<IFormFile>
{
    private string[] _extensions = new string[5] { ".PDF", ".JPG", ".PNG", ".JPEG", ".ICO" };

    public IFormValidator()
    {
        RuleFor(x => x.Length)
            .LessThan(2000000)
            .WithMessage("Wybrany plik jest zbyt duży.");

        RuleFor(x => x.FileName)
            .Must(ValidName).WithMessage("Nieprawidłowa nazwa pliku.")
            .Must(ValidExtensions).WithMessage("Nieprawidłowe rozszerzenie pliku.")
            .Must(x => x.Length < 200).WithMessage("Zbyt długa nazwa pliku");
    }

    private bool ValidExtensions(string fileName)
    {
        var fileExtensions = Path.GetExtension(fileName).ToUpper();
        return _extensions.Contains(fileExtensions);
    }

    private bool ValidName(string fileName)
    {
        var dotCount = fileName.Where(x => x == '.').Count();

        if (dotCount > 1)
            return false;

        if(fileName.Contains("\\") || fileName.Contains("/") || fileName.Contains(":") || fileName.Contains(";") || fileName.Contains(" ") || fileName.Contains(","))
            return false;

        return true;
    }
}