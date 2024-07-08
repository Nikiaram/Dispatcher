using Dispatcher.Models;
using Dispatcher.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dispatcher.Controllers;

[ApiController]
[Route("[controller]")]
public class JobController : ControllerBase {
    private readonly IJobService _jobService;

    public JobController(IJobService jobService) {
        _jobService = jobService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateJob([FromBody] JobRequest jobRequest) {
        if (jobRequest == null) {
            return BadRequest("Job request is null.");
        }

        var result = await _jobService.CreateJobAsync(jobRequest);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetJob(Guid id) {
        var job = await _jobService.GetJobAsync(id);
        if (job == null) {
            return NotFound();
        }

        return Ok(job);
    }
}