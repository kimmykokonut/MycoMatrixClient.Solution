using System.Threading.Tasks;
using RestSharp;

namespace MycoMatrixClient.Models;

public class ApiHelper
{
  public static async Task<string> GetAll()
  {
    RestClient client = new RestClient("http://localhost:5000/");
    RestRequest request = new RestRequest($"api/v1/Mushrooms/?pageSize=30", Method.Get);
    RestResponse response = await client.GetAsync(request);
    return response.Content;
  }
  public static async Task<string> Get(int id)
  {
    RestClient client = new RestClient("http://localhost:5000/");
    RestRequest request = new RestRequest($"api/v1/Mushrooms/{id}", Method.Get);
    RestResponse response = await client.GetAsync(request);
    return response.Content;
  }
  public static async void Post(string newMush)
  {
    RestClient client = new RestClient("http://localhost:5000/");
    RestRequest request = new RestRequest($"api/v1/Mushrooms/", Method.Post);
    request.AddHeader("Content-Type", "application/json");
    request.AddBody(newMush);
    await client.PostAsync(request);
  }
}