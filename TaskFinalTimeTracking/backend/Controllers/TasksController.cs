using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeTrackingApi.Data;
using TimeTrackingApi.Models;

namespace TimeTrackingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly AppDbContext _context;

    public TasksController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/tasks
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetAll()
    {
        return await _context.Tasks.Include(t => t.Project).ToListAsync();
    }

    // GET: api/tasks/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItem>> GetById(int id)
    {
        var task = await _context.Tasks.Include(t => t.Project).FirstOrDefaultAsync(t => t.Id == id);
        if (task == null) return NotFound();
        return task;
    }

    // POST: api/tasks
    [HttpPost]
    public async Task<ActionResult<TaskItem>> Create(TaskItem task)
    {
        var projectExists = await _context.Projects.AnyAsync(p => p.Id == task.ProjectId);
        if (!projectExists) return BadRequest("Указанный проект не найден.");

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    // PUT: api/tasks/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TaskItem updated)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return NotFound();

        task.Name = updated.Name;
        task.IsActive = updated.IsActive;
        task.ProjectId = updated.ProjectId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/tasks/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return NotFound();

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
