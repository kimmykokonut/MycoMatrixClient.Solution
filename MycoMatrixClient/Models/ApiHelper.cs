using System.Threading.Tasks;
using RestSharp;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
    // IRestResponse response = await client.ExecuteAsync(request);
    RestResponse response = await client.GetAsync(request);
    return response.Content;
  }
  public static async Task<string> Get(int id)
  {
    RestClient client = new("http://localhost:5000/");
    RestRequest request = new($"api/v1/Mushrooms/{id}", Method.Get);
    RestResponse response = await client.GetAsync(request);
    return response.Content;
  }
  public static async void Post(string newMush)
  {
    RestClient client = new("http://localhost:5000/");
    RestRequest request = new($"api/v1/Mushrooms/", Method.Post);
    request.AddHeader("Content-Type", "application/json");
    request.AddBody(newMush);
    await client.PostAsync(request);
  }
  public static async void Put(int id, string newMush)
  {
    RestClient client = new("http://localhost:5000/");
    RestRequest request = new($"api/v1/Mushrooms/{id}", Method.Put);
    request.AddHeader("Content-Type", "application/json");
    request.AddJsonBody(newMush);
    await client.PutAsync(request);
  }
  public static async void Delete(int id)
  {
    RestClient client = new("http://localhost:5000/");
    RestRequest request = new($"api/v1/Mushrooms/{id}", Method.Delete);
    request.AddHeader("Content-Type", "application/json");
    await client.DeleteAsync(request);
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