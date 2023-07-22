using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BTL.Models;
using Microsoft.EntityFrameworkCore;

namespace BTL.Controllers.User;
[Area("User")]

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly dbContext _context;

    public HomeController(ILogger<HomeController> logger, dbContext context)
    {
        _logger = logger;
        _context = context;
    }
    [Route("/")]
    public async Task<IActionResult> Index(int? page)
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

        //Phân page

        int pageSize = 3;
        int pageNumber = page ?? 1;
        //end
        var motel = await _context.tblMotel.OrderByDescending(m => m.Date_created)
           .OrderBy(m => m.Date_created)//page
            .Skip((pageNumber - 1) * pageSize)//page
            .Take(pageSize)//page
        .ToListAsync();
        //Content
        ViewBag.Context = _context;
        //lay page
        ViewBag.page = page;
        //end
        //Tính trang
        var totalRecords = await _context.tblMotel.CountAsync();
        ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
        //end
        return View("~/Views/User/Index.cshtml", motel);
    }
    [Route("/Post")]
    public async Task<IActionResult> Post()
    {
        int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
        string userName = HttpContext.Session.GetString("UserName") ?? "";
        string userPhone = HttpContext.Session.GetString("UserPhone") ?? "";
        string userEmail = HttpContext.Session.GetString("UserEmail") ?? "";
        ViewBag.UserId = userId;
        ViewBag.UserName = userName;
        ViewBag.userPhone = userPhone;
        ViewBag.userEmail = userEmail;
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
        return View("~/Views/User/Post.cshtml");
    }

    //
    [Route("Home/District")]
    [HttpPost]
    public async Task<IActionResult> District(int cityId)
    {
        var districts = await _context.tblDistrict
            .Where(v => v.City_id == cityId)
            .ToListAsync();
        var html = districts.Select(v =>
             $"<option value='{v.Id}'>{v.Name}</option>");
        return Ok(html);
    }
    [Route("Home/Village")]
    [HttpPost]
    public async Task<IActionResult> Village(int districtId)
    {
        var villages = await _context.tblVillage
            .Where(v => v.District_id == districtId)
            .ToListAsync();
        var html = villages.Select(v =>
             $"<option value='{v.Id}'>{v.Name}</option>");
        return Ok(html);
    }
    //
    //   [Route("Home/District")]
    //     [HttpPost]
    //     public IActionResult District(int cityId)
    //     {
    //         var districts =  _context.tblDistrict
    //             .Where(v => v.City_id == cityId)
    //             .ToList();
    //         var html = districts.Select(v =>
    //              $"<option value='{v.Id}'>{v.Name}</option>");
    //         return Ok(html);
    //     }
    //
    [Route("Home/Favourite")]
    [HttpPost]
    public async Task<IActionResult> Favourite(int id, int status)
    {
        if (id != null)
        {
            var iss = await _context.tblFavourite.Where(m => m.User_id == HttpContext.Session.GetInt32("UserId")).FirstOrDefaultAsync();
            if (iss == null)
            {
                Favorite favorite = new Favorite();
                favorite.User_id = HttpContext.Session.GetInt32("UserId") ?? 0;
                _context.tblFavourite.Add(favorite);
                await _context.SaveChangesAsync();

                Favourite_detail favourite_Detail = new Favourite_detail();
                favourite_Detail.Favourite_id = favorite.Id;
                favourite_Detail.Motel_id = id;
                _context.tblFavourite_detail.Add(favourite_Detail);
                await _context.SaveChangesAsync();

                int count = await _context.tblFavourite_detail.Where(m => m.Favourite_id == favorite.Id).CountAsync();
                return Json(new { status = "success", count = count });
            }
            else
            {
                if (status == 1)
                {
                    Favourite_detail favourite_Detail = new Favourite_detail();
                    favourite_Detail.Favourite_id = iss.Id;
                    favourite_Detail.Motel_id = id;
                    _context.tblFavourite_detail.Add(favourite_Detail);
                    await _context.SaveChangesAsync();
                    int count = await _context.tblFavourite_detail.Where(m => m.Favourite_id == iss.Id).CountAsync();
                    return Json(new { status = "success", count = count });
                }
                else
                {
                    var detail = await _context.tblFavourite_detail.Where(m => m.Motel_id == id && m.Favourite_id == iss.Id).FirstOrDefaultAsync();
                    _context.tblFavourite_detail.Remove(detail);
                    await _context.SaveChangesAsync();
                    int count = await _context.tblFavourite_detail.Where(m => m.Favourite_id == iss.Id).CountAsync();
                    return Json(new { status = "success", count = count });
                }
            }
        }
        else
        {
            return Json(new { status = "false", count = 0 });
        }


    }

}
