using BankingApplication.Data;
using BankingApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleHashing.Net;

namespace BankingApplication.Controllers;

[Route("/Banking/SignUp")]
public class SignUpController : Controller
{
    private static readonly ISimpleHash s_simpleHash = new SimpleHash();
    private readonly BankingApplicationContext _context;

    public SignUpController(BankingApplicationContext context)
    {
        _context = context;
    }

    public IActionResult SignUp() => View();

    [HttpPost]
    public IActionResult SignUp(string loginID, string password, string confirmPassword)
    {
        var login = _context.Logins.Include(x => x.Customer).FirstOrDefault(y => y.LoginID == loginID);
        if(login != null && password != null && confirmPassword != null){
            ModelState.AddModelError("SignUpFailed", "User already exists.");
            // return View(new Login { LoginID = loginID });
            if(password != confirmPassword){
                ModelState.AddModelError("MatchPassword", "Password does not match.");
                // return View(new Login { LoginID = loginID });
            }
        }


        // if(password != confirmPassword){
        //     ModelState.AddModelError("MatchPassword", "Password does not match.");
        //     return View(new Login { LoginID = loginID });
        // }

        if(login == null && (password == confirmPassword))
        {
            return RedirectToAction("Index", "Home");
        }

        return View(new Login { LoginID = loginID});
        

    }

}