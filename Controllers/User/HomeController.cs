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
    public async Task<IActionResult> IndexAsync()
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


        var motel = await _context.tblMotel.OrderByDescending(m => m.Date_created).ToListAsync();
        ViewBag.Context = _context;

        return View("~/Views/User/Index.cshtml", motel);
    }
    [Route("/Post")]
    public IActionResult Post()
    {
        int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
        string userName = HttpContext.Session.GetString("UserName") ?? "";
        ViewBag.UserId = userId;
        ViewBag.UserName = userName;
        return View("~/Views/User/Post.cshtml");
    }

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
                    int count = await _context.tblFavourite_detail.Where(m => m.Favourite_id == iss.Id ).CountAsync();
                    return Json(new { status = "success", count = count });
                }
                else
                {
                    var detail = await _context.tblFavourite_detail.Where(m => m.Motel_id == id  && m.Favourite_id==iss.Id).FirstOrDefaultAsync();
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
