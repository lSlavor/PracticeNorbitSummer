using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeTrackingApi.Data;
using TimeTrackingApi.Models;

namespace TimeTrackingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProjectsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/projects
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Project>>> GetAll()
    {
        return await _context.Projects.ToListAsync();
    }

    // GET: api/projects/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Project>> GetById(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null) return NotFound();
        return project;
    }

    // POST: api/projects
    [HttpPost]
    public async Task<ActionResult<Project>> Create(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
    }

    // PUT: api/projects/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Project updated)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null) return NotFound();

        project.Name = updated.Name;
        project.Code = updated.Code;
        project.IsActive = updated.IsActive;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/projects/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null) return NotFound();

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
