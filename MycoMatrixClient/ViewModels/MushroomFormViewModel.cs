using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using MycoMatrixClient.Models;

namespace MycoMatrixClient.ViewModels;

public class MushroomViewModel
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
  public bool Edit { get; set; } = false;
  public SelectList Gills { get; } = Mushroom.GillList();

  public Mushroom ToMushroom()
    {
      return new Mushroom() {
        CommonName = CommonName,
        Genus = Genus,
        Species = Species,
        GillType = GillType,
        ImageURL = ImageURL,
        ToxicityLevel = ToxicityLevel,
        Notes = Notes,
        Editor = Editor
      };
    }
}