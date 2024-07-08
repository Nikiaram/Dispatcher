using Dispatcher.Repositories;
using RabbitMQ.Client;
using System.Text;
using Dispatcher.Models;

namespace Dispatcher.Services;
public class JobService : IJobService
{
    private readonly IJobRepository _repository;
    private readonly IModel _channel;

    public JobService(IJobRepository repository, IModel channel) {
        _repository = repository;
        _channel = channel;
    }

    public async Task<JobResponse> CreateJobAsync(JobRequest jobRequest) {
        jobRequest.Id = Guid.NewGuid();
        jobRequest.Status = "Created";

        await _repository.AddAsync(jobRequest);

        var body = Encoding.UTF8.GetBytes(jobRequest.Id.ToString());
        _channel.BasicPublish(exchange: "",
                              routingKey: "job_queue",
                              basicProperties: null,
                              body: body);

        return new JobResponse {
            Id = jobRequest.Id,
            JobType = jobRequest.JobType,
            Payload = jobRequest.Payload,
            Status = jobRequest.Status
        };
    }

    public async Task<JobResponse> GetJobAsync(Guid id) {
        var jobRequest = await _repository.GetByIdAsync(id);
        if (jobRequest == null) {
            return null;
        }

        return new JobResponse {
            Id = jobRequest.Id,
            JobType = jobRequest.JobType,
            Payload = jobRequest.Payload,
            Status = jobRequest.Status
        };
    }
}