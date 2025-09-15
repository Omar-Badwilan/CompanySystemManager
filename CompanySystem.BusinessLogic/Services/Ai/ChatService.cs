using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class ChatService
{
    private readonly HttpClient _httpClient;
    private readonly string _connectionString;
    private readonly string _apiUrl;
    private readonly string _apiKey;

    public ChatService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;

        // get from appsettings.json
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("DefaultConnection string is missing in appsettings.json");

        _apiUrl = config["ChatService:ApiUrl"]
            ?? throw new InvalidOperationException("ChatService:ApiUrl is missing in appsettings.json");

        _apiKey = config["ChatService:ApiKey"]
            ?? throw new InvalidOperationException("ChatService:ApiKey is missing in appsettings.json");


        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");


    }

    public async Task<string> GetSqlQueryAsync(string message)
    {
        // 1. Serialize the user’s message into JSON: {"message": "hi"}
        var json = JsonSerializer.Serialize(new { message });

        // 2. Wrap it in StringContent so HttpClient can send it
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // 3. Call your FastAPI endpoint (ngrok public URL)
        var response = await _httpClient.PostAsync(_apiUrl, content);

        // 4. Throw exception if HTTP status is not 2xx
        response.EnsureSuccessStatusCode();

        // 5. Read body (e.g., {"response": "Employees in IT: Omar, Mariam"})
        var body = await response.Content.ReadAsStringAsync();

        // 6. Parse JSON and extract the "response" field
        var doc = JsonDocument.Parse(body);


        return doc.RootElement.GetProperty("sql").GetString()
            ?? throw new InvalidOperationException("API response did not contain a 'sql' property.");
    }

        

    public async Task<(bool success, string? sql, DataTable? results, string? error)>
        ExecuteSqlAsync(string aiResponse)
    {
        // Clean up response
        aiResponse = aiResponse
            .Replace("<|im_end|>", "")
            .Replace("`", "")
            .Replace("@", "")
            .Trim();

        // Extract SQL (only SELECT)
        var match = Regex.Match(aiResponse,
            @"\b(SELECT)\b[\s\S]*?(;|$)",
            RegexOptions.IgnoreCase);

        if (!match.Success)
            return (false, null, null, "AI response did not contain valid SQL.");

        string aiSql = match.Value.Trim();


        //Handling some Ai errors might happen
        // Force table references and data types
        aiSql = aiSql
            .Replace("FROM Roles", "FROM dbo.Roles")
            .Replace("FROM AspNetUserRoles", "FROM dbo.AspNetUserRoles");


        // If the AI inserted a numeric filter on a GUID column, remove it
        aiSql = Regex.Replace(aiSql, @"WHERE\s+UserId\s*=\s*\d+", "");
        aiSql = Regex.Replace(aiSql, @"WHERE\s+RoleId\s*=\s*\d+", "");

        try
        {
            using var con = new SqlConnection(_connectionString);
            await con.OpenAsync();

            using var cmd = new SqlCommand(aiSql, con);
            using var reader = await cmd.ExecuteReaderAsync();

            var dt = new DataTable();
            dt.Load(reader);


            return (true, aiSql, dt, null);
        }
        catch (Exception ex)
        {
            return (false, aiSql, null, ex.Message);
        }
    }


}
