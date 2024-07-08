using Dispatcher.Models;

namespace Dispatcher.Repositories; 
public interface IJobRepository
{
    Task AddAsync(JobRequest jobRequest);
    Task<JobRequest> GetByIdAsync(Guid id);
}