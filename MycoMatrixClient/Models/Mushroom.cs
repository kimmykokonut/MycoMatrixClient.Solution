using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MycoMatrixClient.Models;

public class Mushroom
{
  public int MushroomId { get; set; }

  [Required]
  [Display(Name = "Common Name")]
  [StringLength(255)]
  public string CommonName { get; set; }

  [StringLength(255)]
  public string Genus { get; set; }

  [StringLength(255)]
  public string Species { get; set; }

  [StringLength(255)]
  [Display(Name = "Gill Type")]
  public string GillType { get; set; }

  [StringLength(255)]
  [Display(Name = "Image URL")]
  public string ImageURL { get; set; }

  [Range(0, 10, ErrorMessage = "Toxicity levels range 0-10")]
  [Display(Name = "Toxicity Level")]
  public int ToxicityLevel { get; set; }
  public string Notes { get; set; }
  [StringLength(45)]
  public string Editor { get; set; }

  public static SelectList GillList()
  {
    List<string> options = new() {
      "Gills",
      "None",
      "Teeth",
      "Pores",
      "Ridged",
      "Decurrent Gills",
      "Forked Gills",        
    };
    return new SelectList(options, "None");
  }

  public static List<Mushroom> GetMushrooms()
  {
    var apiCallTask = ApiHelper.GetAll();
    var result = apiCallTask.Result;

    JArray jsonResponse = JsonConvert.DeserializeObject<JArray>(result);
    List<Mushroom> mushroomList = JsonConvert.DeserializeObject<List<Mushroom>>(jsonResponse.ToString());

    return mushroomList;
  }
  public static Mushroom GetDetails(int id)
  {
    var apiCallTask = ApiHelper.Get(id);
    var result = apiCallTask.Result;

    JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
    Mushroom mushroom = JsonConvert.DeserializeObject<Mushroom>(jsonResponse.ToString());

    return mushroom;
  }
  public static void Post(Mushroom mushroom)
  {
    string jsonMushroom = JsonConvert.SerializeObject(mushroom);
    ApiHelper.Post(jsonMushroom);
  }
  public static void Put(Mushroom mushroom)
  {
    string jsonMushroom = JsonConvert.SerializeObject(mushroom);
    ApiHelper.Put(mushroom.MushroomId, jsonMushroom);
  }
}