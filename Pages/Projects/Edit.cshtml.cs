using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektApp.Data;
using ProjektApp.Models;
using System.Security.Claims;

namespace ProjektApp.Pages.Projects;

[Authorize]
public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Project Project { get; set; } = default!;

    public SelectList StatusList { get; set; } = new(new[] { "Startat", "Slutfört" });

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);

        if (Project == null)
            return NotFound();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (Project.UserId != userId)
            return Forbid();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            StatusList = new SelectList(new[] { "Startat", "Slutfört" }, Project.Status);
            return Page();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var projectInDb = await _context.Projects.FirstOrDefaultAsync(p => p.Id == Project.Id && p.UserId == userId);

        if (projectInDb == null)
            return NotFound();

        projectInDb.Title = Project.Title;
        projectInDb.Description = Project.Description;
        projectInDb.Status = Project.Status;

        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}
