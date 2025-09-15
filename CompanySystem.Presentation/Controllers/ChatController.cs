using CompanySystem.Presentation.ViewModels.AI;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Data.SqlClient;
public class ChatController : Controller
{
    private readonly ChatService _chatService;

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

        // 2️ Execute SQL
        var result = await _chatService.ExecuteSqlAsync(aiResponse);



        if (!result.success || result.results == null)
            return Json(new { success = false, message = result.error });

        // 3. Convert DataTable → list of dictionaries
        var rows = result.results.AsEnumerable()
            .Select(r => result.results.Columns.Cast<DataColumn>()
                .ToDictionary(c => c.ColumnName, c => r[c]))
            .ToList();


        return Json(new
        {
            success = true,
            sql = result.sql,
            results = rows
        });
    }


}
