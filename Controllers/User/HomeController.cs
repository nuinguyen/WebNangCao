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


        var motel = await _context.tblMotel.ToListAsync();

        return View("~/Views/User/Index.cshtml",motel);
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
    // [Route("/Login")]
    // public IActionResult Login()
    // {
    //     return View("~/Views/User/Login.cshtml");
    // }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
