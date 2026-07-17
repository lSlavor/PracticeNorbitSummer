using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TimeTrackingApi.Models;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    [JsonIgnore]
    [ValidateNever]
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
