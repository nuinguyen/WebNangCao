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
            TempData["SuccessMessage"] = "Đăng nhập thành công";//Thông báo ra màn hình thánh công
            return RedirectToAction("Index", "Home");

        }
        else
        {
            TempData["ErrorMessage"] = "Tài khoản hoặc mật khẩu không chính xác";//Thông báo ra màn hình thánh công

            return RedirectToAction("Index");
        }

    }
    [Route("/Login/Register")]
    public IActionResult Register()
    {
        return View("~/Views/User/Register.cshtml");
    }
    [Route("Login/Singup")]
    [HttpPost]
    public async Task<IActionResult> Singup(Users user)
    {
        // string name = Request.Form["Email"];
        // string password = Request.Form["Password"];

        _context.tblUser.Add(new Users
        {
            Name = user.Name,
            Email = user.Email,
            Phone = user.Phone,
            Password = user.Password
        });
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Đăng ký thành công";//Thông báo ra màn hình thánh công

        return RedirectToAction("Index");
    }


}
