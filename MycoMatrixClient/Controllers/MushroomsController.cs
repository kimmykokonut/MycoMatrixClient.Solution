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
  public ActionResult Create(MushroomViewModel model)
  {
    if(!ModelState.IsValid)
    {
      return View("Form", model);
    }
    Mushroom.Post(model.ToMushroom());
    return RedirectToAction("Index");
  }
  public ActionResult Edit(int id)
  {
    return View("Form", new MushroomViewModel(Mushroom.GetDetails(id)));
  }

  [HttpPost]
  public ActionResult Edit(MushroomViewModel model)
  {
    if(!ModelState.IsValid) {
      return View("Form", model);
    }
    Mushroom.Put(model.ToMushroom());
    return RedirectToAction("Index");
  }
  public ActionResult Delete(int id)
  {
    Mushroom mushroom = Mushroom.GetDetails(id);
    return View(mushroom);
  }
  [HttpPost, ActionName("Delete")]
  public ActionResult DeleteConfirmed(int id)
  {
    Mushroom.Delete(id);
    return RedirectToAction("Index");
  }
}