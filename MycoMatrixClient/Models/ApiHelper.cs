using System.Threading.Tasks;
using RestSharp;

namespace MycoMatrixClient.Models;

public class ApiHelper
{
  public static async Task<string> GetAll()
  {
    RestClient client = new("http://localhost:5000/");
    RestRequest request = new($"api/v1/Mushrooms/?pageSize=30", Method.Get);
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
}