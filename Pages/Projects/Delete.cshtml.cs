using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjektApp.Data;
using ProjektApp.Models;
using Microsoft.AspNetCore.Authorization;


namespace ProjektApp.Pages.Projects;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Project Project { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();

        Project = await _context.Projects.FindAsync(id);

        if (Project == null) return NotFound();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var project = await _context.Projects.FindAsync(Project.Id);

        if (project != null)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("Index");
    }
}
