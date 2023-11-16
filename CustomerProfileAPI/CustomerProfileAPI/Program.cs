using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CustomerProfileAPI;
using Polly;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", async () =>
{

    var userProfile = new UserProfile() {Nome = "José", Endereco = "Rua 1", Avaliacao = -1};
        
    var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(2));

    var retryPolicy = Policy.Handle<HttpRequestException>()
        .Or<TaskCanceledException>()
        .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

    using var client = new HttpClient();

    string url = "http://localhost:3000/get-rating/1";

    HttpResponseMessage response = await retryPolicy.ExecuteAsync(async () =>
        await timeoutPolicy.ExecuteAsync(async token =>
            await client.GetAsync(url), CancellationToken.None));

    if (response.IsSuccessStatusCode)
    {
        var responseString = await response.Content.ReadAsStringAsync();
        var userRating = JsonSerializer.Deserialize<UserRating>(responseString);
        userProfile.Avaliacao = userRating.rating;
    }
    else
    {
        return $"Erro: {response.StatusCode}";
    }
    
    return JsonSerializer.Serialize(userProfile);
});

app.Run();