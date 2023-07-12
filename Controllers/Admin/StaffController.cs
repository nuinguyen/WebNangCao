using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BTL.Models;
using Microsoft.EntityFrameworkCore;

namespace BTL.Controllers.Admin;
[Area("Admin")]
public class StaffController : Controller
{
    private readonly ILogger<StaffController> _logger;
    private readonly dbContext _context;

    public StaffController(ILogger<StaffController> logger, dbContext context)
    {
        _logger = logger;
        _context = context;
    }
    [Route("Staff/index")]
    public async Task<IActionResult> Index()
    {
        var staffs = await _context.tblStaff.ToListAsync();

        return View("~/Views/Admin/Staff/Index.cshtml", staffs);
    }

    [Route("Staff/Create")]
    public IActionResult Create()
    {
        return View("~/Views/Admin/Staff/Create.cshtml");
    }
    [Route("Staff/Edit")]
    public async Task<IActionResult> Edit(int id)
    {
        if (id != null)
        {
            var staff = await _context.tblStaff.FindAsync(id);
            return View("~/Views/Admin/Staff/Edit.cshtml", staff);
        }
        else
        {
            return NotFound();
        }
    }

    [Route("Staff/Add")]
    [HttpPost]
    public async Task<IActionResult> Add(Staff staff)
    {
        _context.tblStaff.Add(staff);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");

    }
    [Route("Staff/Update")]
    [HttpPost]
    public async Task<IActionResult> Update(Staff staff)
    {

        var member = await _context.tblStaff.FindAsync(staff.Id);
        if (member == null)
        {
            return NotFound();
        }

        member.Name = staff.Name;
        member.Email = staff.Email;
        member.Birthday = staff.Birthday;
        member.Phone = staff.Phone;
        member.Gender = staff.Gender;
        member.Address = staff.Address;
        member.Password = staff.Password;

        _context.tblStaff.Update(member);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");

    }
        [Route("Staff/Delete")]

      [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            Console.WriteLine("Id" + id) ;
            var staff = await _context.tblStaff.FindAsync(id);
            _context.tblStaff.Remove(staff);
            await _context.SaveChangesAsync();
            return Content("success");
        }
        catch (System.Exception)
        {
            return Content("error");
            throw;
        }
        
    }

}
