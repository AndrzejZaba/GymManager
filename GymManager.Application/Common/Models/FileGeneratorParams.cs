using Microsoft.AspNetCore.Mvc;

namespace GymManager.Application.Common.Models;

public class FileGeneratorParams
{
    public ActionContext Context { get; set; }
    public string ViewTemplate { get; set; }
    public object Model { get; set; }   
}
