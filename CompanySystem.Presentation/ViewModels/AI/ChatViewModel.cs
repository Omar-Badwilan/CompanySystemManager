namespace CompanySystem.Presentation.ViewModels.AI
{
    using System.Collections.Generic;

    public class ChatViewModel
    {
        public string Message { get; set; }                
        public List<ChatMessage> ChatHistory { get; set; } = new List<ChatMessage>(); 
    }
}
