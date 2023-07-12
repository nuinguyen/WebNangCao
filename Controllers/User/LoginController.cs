using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BTL.Models;
using Microsoft.EntityFrameworkCore;

namespace BTL.Controllers.User;
[Area("User")]
public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly dbContext _context;

    public LoginController(ILogger<LoginController> logger, dbContext context)
    {
        _logger = logger;
        _context = context;
    }
     [Route("/Login/Index")]
    public IActionResult Index()
    {
        return View("~/Views/User/Login.cshtml");
    }

    [Route("Login/Singin")]
     [HttpPost]
    public async Task<IActionResult> Singin(Users user)
    {
        // string name = Request.Form["Email"];
        // string password = Request.Form["Password"];

        var member = await _context.tblUser.FirstOrDefaultAsync(m => m.Email == user.Email && m.Password == user.Password);

        if (member != null)
        {
            
            HttpContext.Session.SetInt32("UserId", member.Id);
            HttpContext.Session.SetString("UserName", member.Name);

            return RedirectToAction("Index", "Home");

        }
        else
        {
            return RedirectToAction("Index");
        }

    }

    

}
