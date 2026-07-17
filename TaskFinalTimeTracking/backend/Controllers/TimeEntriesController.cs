using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeTrackingApi.Data;
using TimeTrackingApi.Models;

namespace TimeTrackingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimeEntriesController : ControllerBase
{
    private readonly AppDbContext _context;

    public TimeEntriesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/timeentries
    // Поддерживает фильтры: ?date=2026-07-09  |  ?year=2026&month=7
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TimeEntry>>> GetAll(
        [FromQuery] DateOnly? date,
        [FromQuery] int? year,
        [FromQuery] int? month)
    {
        var query = _context.TimeEntries.Include(t => t.Task).AsQueryable();

        if (date.HasValue)
        {
            query = query.Where(t => t.EntryDate == date.Value);
        }
        else if (year.HasValue && month.HasValue)
        {
            query = query.Where(t => t.EntryDate.Year == year.Value && t.EntryDate.Month == month.Value);
        }

        return await query.OrderByDescending(t => t.EntryDate).ToListAsync();
    }

    // GET: api/timeentries/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TimeEntry>> GetById(int id)
    {
        var entry = await _context.TimeEntries.Include(t => t.Task).FirstOrDefaultAsync(t => t.Id == id);
        if (entry == null) return NotFound();
        return entry;
    }

    // GET: api/timeentries/summary?date=2026-07-09&employeeName=Иванов
    // Возвращает сумму часов за день и статус-стикер (yellow/green/red)
    [HttpGet("summary")]
    public async Task<IActionResult> GetDaySummary([FromQuery] DateOnly date, [FromQuery] string employeeName)
    {
        var totalHours = await _context.TimeEntries
            .Where(t => t.EntryDate == date && t.EmployeeName == employeeName)
            .SumAsync(t => t.Hours);

        string status = totalHours < 8 ? "yellow" : totalHours == 8 ? "green" : "red";

        return Ok(new { date, employeeName, totalHours, status });
    }

    // POST: api/timeentries
    [HttpPost]
    public async Task<ActionResult<TimeEntry>> Create(TimeEntry entry)
    {
        var task = await _context.Tasks.FindAsync(entry.TaskId);
        if (task == null) return BadRequest("Задача не найдена.");
        if (!task.IsActive) return BadRequest("Нельзя указать неактивную задачу.");

        if (entry.Hours <= 0 || entry.Hours > 24)
            return BadRequest("Количество часов должно быть положительным и не превышать 24.");

        // Лимит: суммарно по сотруднику за день не более 24 часов
        var sumHours = await _context.TimeEntries
            .Where(t => t.EmployeeName == entry.EmployeeName && t.EntryDate == entry.EntryDate)
            .SumAsync(t => t.Hours);

        if (sumHours + entry.Hours > 24)
            return BadRequest($"Превышен лимит 24 часа в день. Уже внесено: {sumHours} ч.");

        _context.TimeEntries.Add(entry);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = entry.Id }, entry);
    }

    // PUT: api/timeentries/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TimeEntry updated)
    {
        var existing = await _context.TimeEntries.Include(t => t.Task).FirstOrDefaultAsync(t => t.Id == id);
        if (existing == null) return NotFound();

        // Если задача в проводке меняется — старая задача должна быть активна, иначе поле "Задача" заблокировано
        if (existing.TaskId != updated.TaskId && !existing.Task.IsActive)
            return BadRequest("Нельзя изменить задачу — исходная задача уже неактивна.");

        if (updated.Hours <= 0 || updated.Hours > 24)
            return BadRequest("Количество часов должно быть положительным и не превышать 24.");

        // Пересчёт лимита 24ч без учёта текущей редактируемой записи
        var sumHours = await _context.TimeEntries
            .Where(t => t.Id != id && t.EmployeeName == updated.EmployeeName && t.EntryDate == updated.EntryDate)
            .SumAsync(t => t.Hours);

        if (sumHours + updated.Hours > 24)
            return BadRequest($"Превышен лимит 24 часа в день. Уже внесено: {sumHours} ч.");

        existing.EntryDate = updated.EntryDate;
        existing.Hours = updated.Hours;
        existing.Description = updated.Description;
        existing.EmployeeName = updated.EmployeeName;
        existing.TaskId = updated.TaskId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/timeentries/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entry = await _context.TimeEntries.FindAsync(id);
        if (entry == null) return NotFound();

        _context.TimeEntries.Remove(entry);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
