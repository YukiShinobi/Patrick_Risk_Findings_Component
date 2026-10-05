using System.Security.Claims;
using MedicalDeviceSafetyPlatform.Data;
using MedicalDeviceSafetyPlatform.Models;
using MedicalDeviceSafetyPlatform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicalDeviceSafetyPlatform.Controllers;

/// <summary>
/// Handles creation, storage, retrieval and editing of employee risk-review findings.
/// Employees can create, store, view, edit and comment on their own findings.
/// </summary>
[Authorize(Roles = "Employee")]
public class FindingsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<FindingsController> _logger;

    public FindingsController(ApplicationDbContext context, ILogger<FindingsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    private string CurrentEmployeeId =>
        User.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? throw new InvalidOperationException("Authenticated employee ID was not available.");

    // Lists findings owned by the current employee.
    public async Task<IActionResult> Index()
    {
        var employeeId = CurrentEmployeeId;

        var findings = await _context.Findings
            .AsNoTracking()
            .Include(f => f.SafetyNotice)
            .Where(f => f.EmployeeId == employeeId)
            .OrderByDescending(f => f.UpdatedAtUtc)
            .ToListAsync();

        return View(findings);
    }

    // Displays an owned finding with its linked notice and comments.
    public async Task<IActionResult> Details(int id)
    {
        var employeeId = CurrentEmployeeId;

        var finding = await _context.Findings
            .AsNoTracking()
            .Include(f => f.SafetyNotice)
            .Include(f => f.Comments)
                .ThenInclude(c => c.Author)
            .SingleOrDefaultAsync(f => f.Id == id && f.EmployeeId == employeeId);

        return finding is null ? NotFound() : View(finding);
    }

    // Starts a finding for a selected safety or recall notice.
    public async Task<IActionResult> Create(int safetyNoticeId)
    {
        var notice = await _context.SafetyNotices
            .AsNoTracking()
            .SingleOrDefaultAsync(n => n.Id == safetyNoticeId);

        if (notice is null)
            return NotFound();

        return View(new FindingFormViewModel
        {
            SafetyNoticeId = notice.Id,
            NoticeReference = notice.Reference,
            DeviceName = notice.DeviceName
        });
    }

    // Validates and persists a new finding.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(FindingFormViewModel model)
    {
        var notice = await _context.SafetyNotices
            .AsNoTracking()
            .SingleOrDefaultAsync(n => n.Id == model.SafetyNoticeId);

        if (notice is null)
            ModelState.AddModelError(nameof(model.SafetyNoticeId), "The selected notice no longer exists.");

        if (!ModelState.IsValid)
        {
            if (notice is not null)
            {
                model.NoticeReference = notice.Reference;
                model.DeviceName = notice.DeviceName;
            }
            return View(model);
        }

        var finding = new Finding
        {
            SafetyNoticeId = model.SafetyNoticeId,
            EmployeeId = CurrentEmployeeId,
            Title = model.Title.Trim(),
            Analysis = model.Analysis.Trim(),
            Relevance = model.Relevance,
            Status = model.Status,
            CreatedAtUtc = DateTime.UtcNow,
            UpdatedAtUtc = DateTime.UtcNow
        };

        try
        {
            _context.Findings.Add(finding);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Finding saved successfully.";
            return RedirectToAction(nameof(Details), new { id = finding.Id });
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Unable to save finding for notice {NoticeId}.", model.SafetyNoticeId);
            ModelState.AddModelError(string.Empty, "The finding could not be saved. Please try again.");
            model.NoticeReference = notice!.Reference;
            model.DeviceName = notice.DeviceName;
            return View(model);
        }
    }

    // Loads an owned finding for editing.
    public async Task<IActionResult> Edit(int id)
    {
        var employeeId = CurrentEmployeeId;

        var finding = await _context.Findings
            .AsNoTracking()
            .Include(f => f.SafetyNotice)
            .SingleOrDefaultAsync(f => f.Id == id && f.EmployeeId == employeeId);

        if (finding is null)
            return NotFound();

        return View(new FindingFormViewModel
        {
            Id = finding.Id,
            SafetyNoticeId = finding.SafetyNoticeId,
            Title = finding.Title,
            Analysis = finding.Analysis,
            Relevance = finding.Relevance,
            Status = finding.Status,
            RowVersion = finding.RowVersion,
            NoticeReference = finding.SafetyNotice?.Reference ?? string.Empty,
            DeviceName = finding.SafetyNotice?.DeviceName ?? string.Empty
        });
    }

    // Updates an owned finding and detects conflicting edits.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, FindingFormViewModel model)
    {
        if (model.Id != id)
            return BadRequest();

        var employeeId = CurrentEmployeeId;
        var finding = await _context.Findings
            .Include(f => f.SafetyNotice)
            .SingleOrDefaultAsync(f => f.Id == id && f.EmployeeId == employeeId);

        if (finding is null)
            return NotFound();

        if (!ModelState.IsValid)
        {
            model.NoticeReference = finding.SafetyNotice?.Reference ?? string.Empty;
            model.DeviceName = finding.SafetyNotice?.DeviceName ?? string.Empty;
            return View(model);
        }

        _context.Entry(finding).Property(f => f.RowVersion).OriginalValue = model.RowVersion!;

        finding.Title = model.Title.Trim();
        finding.Analysis = model.Analysis.Trim();
        finding.Relevance = model.Relevance;
        finding.Status = model.Status;
        finding.UpdatedAtUtc = DateTime.UtcNow;

        try
        {
            await _context.SaveChangesAsync();
            TempData["Success"] = "Finding updated successfully.";
            return RedirectToAction(nameof(Details), new { id });
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, "Concurrency conflict while editing finding {FindingId}.", id);
            ModelState.AddModelError(string.Empty,
                "This finding was changed by another operation. Reload it and review the latest version before saving.");
            model.NoticeReference = finding.SafetyNotice?.Reference ?? string.Empty;
            model.DeviceName = finding.SafetyNotice?.DeviceName ?? string.Empty;
            return View(model);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Unable to update finding {FindingId}.", id);
            ModelState.AddModelError(string.Empty, "The finding could not be updated. Please try again.");
            model.NoticeReference = finding.SafetyNotice?.Reference ?? string.Empty;
            model.DeviceName = finding.SafetyNotice?.DeviceName ?? string.Empty;
            return View(model);
        }
    }

    // Adds a comment to an owned finding.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddComment(int id, string body)
    {
        var employeeId = CurrentEmployeeId;
        var findingExists = await _context.Findings
            .AnyAsync(f => f.Id == id && f.EmployeeId == employeeId);

        if (!findingExists)
            return NotFound();

        if (string.IsNullOrWhiteSpace(body) || body.Trim().Length > 1500)
        {
            TempData["Error"] = "Enter a comment between 1 and 1500 characters.";
            return RedirectToAction(nameof(Details), new { id });
        }

        _context.Comments.Add(new Comment
        {
            FindingId = id,
            AuthorId = employeeId,
            Body = body.Trim(),
            CreatedAtUtc = DateTime.UtcNow
        });

        try
        {
            await _context.SaveChangesAsync();
            TempData["Success"] = "Comment added.";
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Unable to add comment to finding {FindingId}.", id);
            TempData["Error"] = "The comment could not be saved.";
        }

        return RedirectToAction(nameof(Details), new { id });
    }
}
