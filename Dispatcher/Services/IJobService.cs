using Dispatcher.Models;

namespace Dispatcher.Services;
public interface IJobService
{
    Task<JobResponse> CreateJobAsync(JobRequest jobRequest);
    Task<JobResponse> GetJobAsync(Guid id);
}