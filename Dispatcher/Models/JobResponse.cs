namespace Dispatcher.Models;
public class JobResponse {
    public Guid Id { get; set; }
    public string JobType { get; set; }
    public string Payload { get; set; }
    public string Status { get; set; }
}