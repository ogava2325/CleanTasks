using Application.Common.Interfaces.Persistence.Repositories;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CleanTasks.Functions;

public class DeleteOldArchivedProjects(
    ILogger<DeleteOldArchivedProjects> logger,
    IProjectRepository projectRepository)
{
    [Function("DeleteOldArchivedProjects")]
    public async Task Run([TimerTrigger("0 0 10 * * *")] TimerInfo myTimer)
    {
        logger.LogInformation("Starting cleanup of archived projects...");

        try
        {
            await projectRepository.DeleteArchivedOlderThanAsync(TimeSpan.FromDays(30));
            logger.LogInformation("Cleanup completed successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while cleaning up archived projects.");
        }

        logger.LogInformation("Function executed at: {ExecutionTime}", DateTime.UtcNow);

        if (myTimer.ScheduleStatus is not null)
        {
            logger.LogInformation("Next scheduled run: {NextRun}", myTimer.ScheduleStatus.Next);
        }
    }
}