using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TimeTrackingApi.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public int ProjectId { get; set; }

    [JsonIgnore]
    [ValidateNever]
    public Project Project { get; set; } = null!;

    [JsonIgnore]
    [ValidateNever]
    public ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
}
