using GymManager.Application.Announcements.Queries.Dtos;
using GymManager.Application.Charts.Queries.Dtos;
using GymManager.Application.Common.Models;

namespace GymManager.Application.Clients.Queries.GetClientDashboard;

public class GetClientDashboardVm
{
    public string Email { get; set; }
    public DateTime? TicketEndDate { get; set; }
    public PaginatedList<AnnouncementDto> Announcements { get; set; }
    public ChartDto TrainingCountChart { get; set; }
    public ChartDto TheBestTrainingsChart { get; set; }
}
