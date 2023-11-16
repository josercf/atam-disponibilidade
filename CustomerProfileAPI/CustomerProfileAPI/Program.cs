using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CustomerProfileAPI;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", async () =>
{

    var userProfile = new UserProfile() {Nome = "José", Endereco = "Rua 1", Avaliacao = 5};
        
    using var client = new HttpClient();

    // URL que aponta para a API Node.js
    string url = "http://localhost:3000/get-rating/1";

    // Envia a solicitação GET assíncrona
    HttpResponseMessage response = await client.GetAsync(url);

    // Recebe o corpo da resposta e retorna
    if (response.IsSuccessStatusCode)
    {
        var responseString = await response.Content.ReadAsStringAsync();
        var userRating = JsonSerializer.Deserialize<UserRationg>(responseString);
        userProfile.Avaliacao = userRating.rating;
    }
    else
    {
        return $"Erro: {response.StatusCode}";
    }
    
    return JsonSerializer.Serialize(userProfile);
});

app.Run();