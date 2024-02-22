using Microsoft.AspNetCore.Mvc;
using MycoMatrixClient.Models;
using MycoMatrixClient.ViewModels;

namespace MycoMatrixClient.Controllers;

public class AccountsController : Controller
{
  [HttpGet]
  public IActionResult SignIn()
  {
    return View();
  }
  [HttpPost]
  public async Task<IActionResult> SignIn(ApplicationUser user)
  {
    if (ModelState.IsValid)
    {
      try
      { 
        await ApplicationUser.SignIn(user);
        return RedirectToAction("Index", "Mushrooms", new { area = "Api" });
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, "Sign in failure");
        return View(user);
      }
    }
    return View(user);
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