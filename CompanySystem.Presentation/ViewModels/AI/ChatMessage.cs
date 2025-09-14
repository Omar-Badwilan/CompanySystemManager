namespace CompanySystem.Presentation.ViewModels.AI
{
    public class ChatMessage
    {
        public string Sender { get; set; }  // "You" or "AI"
        public string Text { get; set; }    // The AI SQL query
        public bool IsJson { get; set; } = false;
        public List<Dictionary<string, object>> JsonData { get; set; }
        public List<string> JsonKeys { get; set; }
    }
}
