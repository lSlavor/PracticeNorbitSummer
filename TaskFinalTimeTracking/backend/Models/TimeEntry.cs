using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TimeTrackingApi.Models;

public class TimeEntry
{
    public int Id { get; set; }
    public DateOnly EntryDate { get; set; }
    public decimal Hours { get; set; }
    public string? Description { get; set; }
    public string EmployeeName { get; set; } = string.Empty;

    public int TaskId { get; set; }

    [JsonIgnore]
    [ValidateNever]
    public TaskItem Task { get; set; } = null!;
}
