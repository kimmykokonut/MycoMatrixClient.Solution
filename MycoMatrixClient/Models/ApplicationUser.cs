using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MycoMatrixClient.Models;

public class ApplicationUser
{
  
  [StringLength(16, ErrorMessage = "Must be between 3 and 16 characters", MinimumLength = 3)]
  public string UserName { get; set; }

  [Required(ErrorMessage = "Password is required")]
  [StringLength(20, ErrorMessage = "Must be between 5 and 20 characters", MinimumLength = 5)]

  public string Password { get; set; }
  [Required(ErrorMessage = "Email is required")]
  [EmailAddress]
  public string Email { get; set; }

  public async static void SignIn(ApplicationUser user)
  {
    string jsonAccount = JsonConvert.SerializeObject(user);
    await ApiHelper.SignIn(jsonAccount);
      
  }

  public static void Register(ApplicationUser user)
  {
    string jsonUser = JsonConvert.SerializeObject(user);
    ApiHelper.Register(jsonUser);
  }
}