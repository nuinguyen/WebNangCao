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
    [Route("Motel/UpdateStatus")]
    [HttpPost]
    public async Task<IActionResult> UpdateStatus(int id,int status)
    {
        try
        {
            Console.WriteLine("Id" + id);
            var motel = await _context.tblMotel.FindAsync(id);
            motel.Status=status;
            _context.tblMotel.Update(motel);
            await _context.SaveChangesAsync();
            return Content("success");
        }
        catch (System.Exception)
        {
            return Content("error");
            throw;
        }

    }

    [Route("/Motel/Filter")]
    public async Task<IActionResult> Filter(float? price, float? acreage)
    {
        // var motels = await _context.tblMotel.Where(m => m.Title.Contains("a")) .ToListAsync();
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
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            string userName = HttpContext.Session.GetString("UserName") ?? "";
            ViewBag.UserId = userId;
            ViewBag.UserName = userName;
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

    [Route("Motel/List_Favourite")]
    public async Task<IActionResult> List_Favourite()
    {
        int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
        string userName = HttpContext.Session.GetString("UserName") ?? "";
        ViewBag.UserId = userId;
        ViewBag.UserName = userName;
        //đếm yêu thích
        var iss = await _context.tblFavourite.Where(m => m.User_id == userId).FirstOrDefaultAsync();
        if (iss != null)
        {
            int count = await _context.tblFavourite_detail.Where(m => m.Favourite_id == iss.Id).CountAsync();
            ViewBag.count = count;
            ViewBag.favourite_id = iss.Id;
        }
        else
        {
            ViewBag.count = 0;
        }

        ViewBag.Context = _context;

        //
        var favourite = await _context.tblFavourite
    .Where(m => m.User_id == userId)
    .FirstOrDefaultAsync();

        var favouriteId = favourite.Id;

        var favouriteDetail = await _context.tblFavourite_detail
            .Join(_context.tblMotel,
                fd => fd.Motel_id,
                m => m.Id,
                (fd, m) => new
                {
                    Id = fd.Motel_id,
                    Favourite_id = fd.Favourite_id,
                    Title = m.Title,
                    Price = m.Price,
                    Acreage = m.Acreage,
                    Image = m.Image,
                    Phone = m.Phone,
                    Date_created = m.Date_created,
                    Address_post = m.Address_post,
                    Description = m.Description,
                    Name = m.Name,
                })
            .Where(m => m.Favourite_id == favouriteId)
            .ToListAsync();

        return View("~/Views/User/Favourite.cshtml", favouriteDetail);
        // return View("~/Views/User/Favourite.cshtml", favouriteDetail);
    }
    //List Post
    [Route("Motel/ListPost")]
    public async Task<IActionResult> ListPost()
    {
        int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
        string userName = HttpContext.Session.GetString("UserName") ?? "";
        ViewBag.UserId = userId;
        ViewBag.UserName = userName;
        //đếm yêu thích
        var iss = await _context.tblFavourite.Where(m => m.User_id == userId).FirstOrDefaultAsync();
        if (iss != null)
        {
            int count = await _context.tblFavourite_detail.Where(m => m.Favourite_id == iss.Id).CountAsync();
            ViewBag.count = count;
            ViewBag.favourite_id = iss.Id;
        }
        else
        {
            ViewBag.count = 0;
        }

        ViewBag.Context = _context;


        //
        var Motel_user = await _context.tblMotel
    .Where(m => m.User_id == userId)
    .ToListAsync();

        int count_post = await _context.tblMotel.Where(m => m.User_id == userId).CountAsync();
        ViewBag.count_post = count_post;

        return View("~/Views/User/ListPost.cshtml", Motel_user);
        // return View("~/Views/User/Favourite.cshtml", favouriteDetail);
    }

    //SẮp xếp
    [Route("/Motel/Arrange")]
    public async Task<IActionResult> Arrange(string value)
    {
        int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
        string userName = HttpContext.Session.GetString("UserName") ?? "";
        ViewBag.UserId = userId;
        ViewBag.UserName = userName;
        //đếm yêu thích
        var iss = await _context.tblFavourite.Where(m => m.User_id == userId).FirstOrDefaultAsync();
        if (iss != null)
        {
            int count = await _context.tblFavourite_detail.Where(m => m.Favourite_id == iss.Id).CountAsync();
            ViewBag.count = count;
            ViewBag.favourite_id = iss.Id;
        }
        else
        {
            ViewBag.count = 0;
        }

        ViewBag.Context = _context;

        //
        var favourite = await _context.tblFavourite
    .Where(m => m.User_id == userId)
    .FirstOrDefaultAsync();

        var favouriteId = favourite.Id;

        var favouriteDetail = await _context.tblFavourite_detail
            .Join(_context.tblMotel,
                fd => fd.Motel_id,
                m => m.Id,
                (fd, m) => new
                {
                    Id = fd.Motel_id,
                    Favourite_id = fd.Favourite_id,
                    Title = m.Title,
                    Price = m.Price,
                    Acreage = m.Acreage,
                    Image = m.Image,
                    Phone = m.Phone,
                    Date_created = m.Date_created,
                    Address_post = m.Address_post,
                    Description = m.Description,
                    Name = m.Name,
                })
            .Where(m => m.Favourite_id == favouriteId)
            .ToListAsync();



        switch (value)
        {
            case "increase_price":
                favouriteDetail = await _context.tblFavourite_detail
        .Join(_context.tblMotel,
            fd => fd.Motel_id,
            m => m.Id,
            (fd, m) => new
            {
                Id = fd.Motel_id,
                Favourite_id = fd.Favourite_id,
                Title = m.Title,
                Price = m.Price,
                Acreage = m.Acreage,
                Image = m.Image,
                Phone = m.Phone,
                Date_created = m.Date_created,
                Address_post = m.Address_post,
                Description = m.Description,
                Name = m.Name,
            })
        .Where(m => m.Favourite_id == favouriteId).OrderBy(m => m.Price)
        .ToListAsync();
                break;
            case "discount_price":
                favouriteDetail = await _context.tblFavourite_detail
      .Join(_context.tblMotel,
          fd => fd.Motel_id,
          m => m.Id,
          (fd, m) => new
          {
              Id = fd.Motel_id,
              Favourite_id = fd.Favourite_id,
              Title = m.Title,
              Price = m.Price,
              Acreage = m.Acreage,
              Image = m.Image,
              Phone = m.Phone,
              Date_created = m.Date_created,
              Address_post = m.Address_post,
              Description = m.Description,
              Name = m.Name,
          })
      .Where(m => m.Favourite_id == favouriteId).OrderByDescending(m => m.Price)
      .ToListAsync();
                break;

            default:
                // code block for default case
                break;
        }
        return View("~/Views/User/Favourite.cshtml", favouriteDetail);
    }

}
