using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BTL.Models;
using Microsoft.Data.SqlClient;
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
    public async Task<IActionResult> Search(string Address, int min, int max)
    {
        var n = Address;
        var result = await _context.searchMotel.FromSqlRaw("EXEC searchMotel @Address_search, @min_price_search, @max_price_search",
        new SqlParameter("@Address_search", !string.IsNullOrEmpty(n) ? n : ""),
        new SqlParameter("@min_price_search", min),
        new SqlParameter("@max_price_search", max)).ToListAsync();
        return View("~/Views/User/Index.cshtml", result);
    }
    [Route("/Motel/Filter")]
    public async Task<IActionResult> Filter(float? price, float? acreage)
    {
        var motels = await _context.tblMotel.ToListAsync();

        if (price.HasValue)
        {
            float from_price = 0;
            float to_price = 0;
            switch (price)
            {
                case 1:
                    from_price = 0;
                    to_price = 1000000;
                    break;
                case 3:
                    from_price = 1000000;
                    to_price = 3000000;
                    break;
                case 5:
                    from_price = 3000000;
                    to_price = 5000000;
                    break;
                case 10:
                    from_price = 5000000;
                    to_price = 10000000;
                    break;
                case 40:
                    from_price = 10000000;
                    to_price = 40000000;
                    break;
                default:
                    // code block for default case
                    break;
            }
            motels = await _context.tblMotel.Where(m => m.Price >= from_price && m.Price <= to_price).ToListAsync();
        }
        else if (acreage.HasValue)
        {
            float from_acreage = 0;
            float to_acreage = 0;
            switch (acreage)
            {
                case 30:
                    from_acreage = 0;
                    to_acreage = 30;
                    break;
                case 50:
                    from_acreage = 30;
                    to_acreage = 50;
                    break;
                case 80:
                    from_acreage = 50;
                    to_acreage = 80;
                    break;
                case 100:
                    from_acreage = 80;
                    to_acreage = 100;
                    break;
                default:
                    // code block for default case
                    break;
            }
            motels = await _context.tblMotel.Where(m => m.Acreage >= from_acreage && m.Acreage <= to_acreage).ToListAsync();
        }

        return View("~/Views/User/Index.cshtml", motels);
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
        return RedirectToAction("Index", "Home");

    }
    [Route("Motel/Detail")]
    public async Task<IActionResult> Detail(int id)
    {
        if (id != null)
        {
            var motel = await _context.tblMotel.FindAsync(id);
            var list = await _context.tblMotel.Take(3).ToListAsync();

            var viewModel = new MotelViewModel
            {
                Motel = motel,
                ListMotel = list
            };

            return View("~/Views/User/Detail.cshtml", viewModel);
        }
        else
        {
            return NotFound();
        }
    }



}
