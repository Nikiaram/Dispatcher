using Dispatcher.Data;
using Dispatcher.Models;

namespace Dispatcher.Repositories; 
public class JobRepository : IJobRepository 
{
    private readonly ApplicationDbContext _context;

    public JobRepository(ApplicationDbContext context) {
        _context = context;
    }

    public async Task AddAsync(JobRequest jobRequest) {
        await _context.JobRequests.AddAsync(jobRequest);
        await _context.SaveChangesAsync();
    }

    public async Task<JobRequest> GetByIdAsync(Guid id) {
        return await _context.JobRequests.FindAsync(id);
    }
}