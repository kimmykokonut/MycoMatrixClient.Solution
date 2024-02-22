using Microsoft.AspNetCore.Mvc;
using MycoMatrixClient.Models;
using MycoMatrixClient.ViewModels;

namespace MycoMatrixClient.Controllers;

public class AccountsController : Controller
{
  [HttpGet]
  public IActionResult SignIn(ApplicationUser user)
  {
    return View();
  }

  public IActionResult Register()
  {
    return View(new ApplicationUser());
  }

  [HttpPost]
  public IActionResult Register(ApplicationUser model)
  {
    if(!ModelState.IsValid)
    {
      return View(model);
    }
    try
    {
      ApplicationUser.Register(model);
      return RedirectToAction("SignIn");
    }
    catch (Exception)
        {
      ModelState.AddModelError("", "An error occurred during registration. Please try again later.");
      return View(model);
    }
    
  }

}