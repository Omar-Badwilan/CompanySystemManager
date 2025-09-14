using CompanySystem.Presentation.ViewModels.AI;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Data.SqlClient;
public class ChatController : Controller
{
    private readonly ChatService _chatService;
    private readonly string _connectionString = "Server = . ; Database = MvcProjectDb; Trusted_Connection = true ; TrustServerCertificate = true ;";

    public ChatController(ChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new ChatViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Index(ChatViewModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Message))
            return Json(new { success = false, message = "Message is empty" });

        // 1️ Get AI response
        string aiResponse = await _chatService.GetSqlQueryAsync(model.Message);

        // 2️ Clean response
        aiResponse = aiResponse
            .Replace("<|im_end|>", "")
            .Replace("`", "")
            .Replace("@", "")  
            .Trim();
        // 3️ Extract SQL
        var match = System.Text.RegularExpressions.Regex.Match(aiResponse,
            @"\b(SELECT|INSERT|UPDATE|DELETE)\b[\s\S]*?(;|$)",
            System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        if (!match.Success)
            return Json(new { success = false, message = "AI response did not contain valid SQL." });

        string aiSql = match.Value.Trim();

        // 4️ Optional: fix table names
        aiSql = aiSql.Replace("FROM Roles", "FROM dbo.Roles");

        // 5️ Execute SQL
        var dt = new DataTable();
        try
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(aiSql, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                con.Open();
                da.Fill(dt);
            }

            // Convert DataTable to JSON
            var rows = new List<Dictionary<string, object>>();
            foreach (DataRow row in dt.Rows)
            {
                var dict = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                    dict[col.ColumnName] = row[col];
                rows.Add(dict);
            }

            return Json(new
            {
                success = true,
                sql = aiSql,
                results = rows
            });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }


}
