using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BTL.Models;

using Microsoft.EntityFrameworkCore;

namespace BTL.Controllers.User;
[Area("User")]
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
    //  [Route("/Login/Index")]
    // public IActionResult Index()
    // {
    //     return View("~/Views/User/Login.cshtml");
    // }

    [Route("Motel/Add")]
    [HttpPost]
    public async Task<IActionResult> Add(Motel motel)
    {
        if (motel.Images != null)
        {
            motel.Image = Path.GetFileName(motel.Images.FileName);
        }
        motel.Status = 1;
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
            return RedirectToAction("Index", "Home");

    }



}
