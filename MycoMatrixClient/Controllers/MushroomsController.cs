using Microsoft.AspNetCore.Mvc;
using MycoMatrixClient.Models;
using MycoMatrixClient.ViewModels;

namespace MycoMatrixClient.Controllers;

public class MushroomsController : Controller
{
  public IActionResult Index()
  {
    List<Mushroom> mushrooms = Mushroom.GetMushrooms();
    return View(mushrooms);
  }
  public IActionResult Details(int id)
  {
    Mushroom mushroom = Mushroom.GetDetails(id);
    return View(mushroom);
  }
  public ActionResult Create()
  {
    
    return View("Form", new MushroomViewModel());
  }
  [HttpPost]
  public ActionResult Create(MushroomViewModel mushroom)
  {
    if(!ModelState.IsValid)
    {
      return View("Form", mushroom);
    }
    Mushroom.Post(mushroom.ToMushroom());
    return RedirectToAction("Index");
  }
}