using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BTL.Models;
using Microsoft.EntityFrameworkCore;

namespace BTL.Controllers.Admin;
[Area("Admin")]
public class MotelController : Controller
{
    private readonly ILogger<MotelController> _logger;
    private readonly dbContext _context;

    public MotelController(ILogger<MotelController> logger, dbContext context)
    {
        _logger = logger;
        _context = context;
    }
    [Route("Motel/index")]
    public async Task<IActionResult> Index()
    {
        var staffs = await _context.tblMotel.ToListAsync();

        return View("~/Views/Admin/Motel/Index.cshtml", staffs);
    }

    [Route("Motel/Create")]
    public IActionResult Create()
    {
        return View("~/Views/Admin/Staff/Create.cshtml");
    }
    [Route("Motel/Edit")]
    public async Task<IActionResult> Edit(int id)
    {
        if (id != null)
        {
            var motel = await _context.tblMotel.FindAsync(id);
            return View("~/Views/Admin/Motel/Edit.cshtml", motel);
        }
        else
        {
            return NotFound();
        }
    }

    // [Route("Motel/Add")]
    // [HttpPost]
    // public async Task<IActionResult> Add(Staff staff)
    // {
    //     _context.tblStaff.Add(staff);
    //     await _context.SaveChangesAsync();
    //     return RedirectToAction("Index");

    // }
    // [Route("Motel/Update")]
    // [HttpPost]
    // public async Task<IActionResult> Update(Staff staff)
    // {

    //     var member = await _context.tblStaff.FindAsync(staff.Id);
    //     if (member == null)
    //     {
    //         return NotFound();
    //     }

    //     member.Name = staff.Name;
    //     member.Email = staff.Email;
    //     member.Birthday = staff.Birthday;
    //     member.Phone = staff.Phone;
    //     member.Gender = staff.Gender;
    //     member.Address = staff.Address;
    //     member.Password = staff.Password;

    //     _context.tblStaff.Update(member);
    //     await _context.SaveChangesAsync();
    //     return RedirectToAction("Index");

    // }
    //     [Route("Motel/Delete")]

    //   [HttpPost]
    // public async Task<IActionResult> Delete(int id)
    // {
    //     try
    //     {
    //         Console.WriteLine("Id" + id) ;
    //         var staff = await _context.tblStaff.FindAsync(id);
    //         _context.tblStaff.Remove(staff);
    //         await _context.SaveChangesAsync();
    //         return Content("success");
    //     }
    //     catch (System.Exception)
    //     {
    //         return Content("error");
    //         throw;
    //     }
        
    // }

}
