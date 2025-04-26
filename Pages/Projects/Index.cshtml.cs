using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjektApp.Data;
using ProjektApp.Models;
using System.Security.Claims;

namespace ProjektApp.Pages.Projects;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Project> Projects { get; set; } = new List<Project>();

    public async Task OnGetAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        Projects = await _context.Projects
            .Where(p => p.UserId == userId)
            .ToListAsync();
    }
}
