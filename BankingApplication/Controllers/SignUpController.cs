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
        return RedirectToAction("Index", "Home");
    }

}