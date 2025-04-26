using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using ProjektApp.Data;
using ProjektApp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace ProjektApp.Pages.Projects;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public CreateModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [BindProperty]
    public Project Project { get; set; }

    public SelectList StatusList { get; set; }

    public void OnGet()
    {
        StatusList = new SelectList(new[] { "Startat", "Slutfört" });
    }

    public async Task<IActionResult> OnPostAsync()
    {
        StatusList = new SelectList(new[] { "Startat", "Slutfört" });

        if (!ModelState.IsValid)
            return Page();

        var userId = _userManager.GetUserId(User);
        Project.UserId = userId;

        _context.Projects.Add(Project);
        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}
