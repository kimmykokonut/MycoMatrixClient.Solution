using System.Threading.Tasks;
using RestSharp;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace MycoMatrixClient.Models;

public class ApiHelper
{
  
  private static string jwtToken;
  public static void SetToken(string token)
  {
    jwtToken = token;
  }
  private static void AddAuthorizationHeader(RestRequest request)
  {
    if (!string.IsNullOrEmpty(jwtToken))
    {
      request.AddHeader("Authorization", $"Bearer {jwtToken}");
    }
    else
    {
      throw new InvalidOperationException("JWT token not set, please sign in first");
    }
  }

  public static async Task<string> GetAll()
  {
    RestClient client = new("http://localhost:5000/");
    RestRequest request = new($"api/v1/Mushrooms/?pageSize=30", Method.Get);
    AddAuthorizationHeader(request);
    RestResponse response = await client.GetAsync(request);

    if (response.StatusCode == HttpStatusCode.Unauthorized) //if 401
    {
      throw new UnauthorizedAccessException("Your session has expired, please log in again");
    }

    return response.Content;
  }
  public static async Task<string> Get(int id)
  {
    RestClient client = new("http://localhost:5000/");
    RestRequest request = new($"api/v1/Mushrooms/{id}", Method.Get);
    AddAuthorizationHeader(request); //add auth...
    RestResponse response = await client.GetAsync(request);

    if (response.StatusCode == HttpStatusCode.Unauthorized) //if 401
    {
      throw new UnauthorizedAccessException("Your session has expired, please log in again");
    }

    return response.Content;
  }
  public static async void Post(string newMush)
  {
    RestClient client = new("http://localhost:5000/");
    RestRequest request = new($"api/v1/Mushrooms/", Method.Post);
    AddAuthorizationHeader(request);
    request.AddHeader("Content-Type", "application/json");
    request.AddBody(newMush);
    RestResponse response = await client.PostAsync(request);

    if (response.StatusCode == HttpStatusCode.Unauthorized) //if 401
    {
      throw new UnauthorizedAccessException("Your session has expired, please log in again");
    }
  }
  public static async void Put(int id, string newMush)
  {
    RestClient client = new("http://localhost:5000/");
    RestRequest request = new($"api/v1/Mushrooms/{id}", Method.Put);
    AddAuthorizationHeader(request);
    request.AddHeader("Content-Type", "application/json");
    request.AddJsonBody(newMush);
    RestResponse response = await client.PutAsync(request);

    if (response.StatusCode == HttpStatusCode.Unauthorized) //if 401
    {
      throw new UnauthorizedAccessException("Your session has expired, please log in again");
    }
  }
  public static async void Delete(int id)
  {
    RestClient client = new("http://localhost:5000/");
    RestRequest request = new($"api/v1/Mushrooms/{id}", Method.Delete);
    AddAuthorizationHeader(request);
    request.AddHeader("Content-Type", "application/json");
    RestResponse response = await client.DeleteAsync(request);

    if (response.StatusCode == HttpStatusCode.Unauthorized) //if 401
    {
      throw new UnauthorizedAccessException("Your session has expired, please log in again");
    }
  }

  public static async Task Register(string newUser)
  {
    RestClient client = new("http://localhost:5000/");
    RestRequest request = new($"Accounts/Register", Method.Post);
    request.AddHeader("Content-Type", "application/json");
    request.AddJsonBody(newUser); //
    await client.PostAsync(request);
  }

  public static async Task<string> SignIn(string credentials)
  {
    RestClient client = new("http://localhost:5000");
    RestRequest request = new($"Accounts/SignIn", Method.Post);
    request.AddHeader("Content-Type", "application/json");
    request.AddJsonBody(credentials);

    RestResponse response = await client.PostAsync(request);

    if (response.IsSuccessful)
    {
      JObject jsonResponse = JObject.Parse(response.Content);
      string token = jsonResponse["token"].ToString();
      SetToken(token);
    }
    return response.Content;
  }
}