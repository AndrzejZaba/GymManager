

namespace GymManager.Application.Common.Interfaces;

public interface IBackgroundWorkerQueue
{
    Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken);

    void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem, string workItemDescription);
}
