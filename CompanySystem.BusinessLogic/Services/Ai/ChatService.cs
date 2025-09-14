using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class ChatService
{
    private readonly HttpClient _httpClient;
    private const string ApiUrl = "https://d94eb252844c.ngrok-free.app/chat"; // Kaggle ngrok URL
    private const string ApiKey = "secret123";

    public ChatService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");
    }

    public async Task<string> GetSqlQueryAsync(string message)
    {
        // 1. Serialize the user’s message into JSON: {"message": "hi"}
        var json = JsonSerializer.Serialize(new { message });

        // 2. Wrap it in StringContent so HttpClient can send it
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // 3. Call your FastAPI endpoint (ngrok public URL)
        var response = await _httpClient.PostAsync(ApiUrl, content);

        // 4. Throw exception if HTTP status is not 2xx
        response.EnsureSuccessStatusCode();

        // 5. Read body (e.g., {"response": "Employees in IT: Omar, Mariam"})
        var body = await response.Content.ReadAsStringAsync();

        // 6. Parse JSON and extract the "response" field
        var doc = JsonDocument.Parse(body);
        return doc.RootElement.GetProperty("sql").GetString();
    }

}
