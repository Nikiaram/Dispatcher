using Dispatcher.Models;
using Microsoft.EntityFrameworkCore;

namespace Dispatcher.Data; 
public class ApplicationDbContext : DbContext
{   
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) {
    }

    public DbSet<JobRequest> JobRequests { get; set; }
}