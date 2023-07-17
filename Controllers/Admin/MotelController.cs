using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BTL.Models;
using Microsoft.EntityFrameworkCore;

namespace BTL.Controllers.Admin;
[Area("Admin")]
public class MotelController : Controller
{
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly ILogger<MotelController> _logger;
    private readonly dbContext _context;

    public MotelController(ILogger<MotelController> logger, dbContext context, IWebHostEnvironment hostEnvironment)
    {
        _logger = logger;
        _context = context;

        this._hostEnvironment = hostEnvironment;
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
        return View("~/Views/Admin/Motel/Create.cshtml");
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


    [Route("Motel/Add")]
    [HttpPost]
    public async Task<IActionResult> Add(Motel motel)
    {
        if (motel.Images != null)
        {
            motel.Image = Path.GetFileName(motel.Images.FileName);
        }
        motel.Status = 1;
        motel.Date_created = DateTime.Now;
        motel.User_id = HttpContext.Session.GetInt32("UserId") ?? 0;
        _context.tblMotel.Add(motel);
        await _context.SaveChangesAsync();

        if (motel.Images != null)
        {
            var fileName = Path.GetFileName(motel.Images.FileName);
            var directoryPath = Path.Combine(_hostEnvironment.WebRootPath, "Images", motel.Id.ToString());
            var filePath = Path.Combine(directoryPath, fileName);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                motel.Images.CopyTo(fileStream);
            }
        }
        return RedirectToAction("Index");

    }


    [Route("Motel/Update")]
    [HttpPost]
    public async Task<IActionResult> Update(Motel motel)
    {

        var apartment = await _context.tblMotel.FindAsync(motel.Id);
        if (apartment == null)
        {
            return NotFound();
        }
        apartment.Name = motel.Name;
        apartment.Title = motel.Title;
        apartment.Description = motel.Description;
        apartment.Acreage = motel.Acreage;
        apartment.Price = motel.Price;
        apartment.Address = motel.Address;
        apartment.Address_post = motel.Address_post;
        apartment.Email = motel.Email;
        apartment.Phone = motel.Phone;
        apartment.Address = motel.Address;
        
        _context.tblMotel.Update(apartment);
        await _context.SaveChangesAsync();
        

        return RedirectToAction("Index");

    }

    [Route("Motel/Delete")]
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            Console.WriteLine("Idd" + id) ;
            var model = await _context.tblMotel.FindAsync(id);
            _context.tblMotel.Remove(model);
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
