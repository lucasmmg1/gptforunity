using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class GPTManager : MonoBehaviour
{
    private OpenAIApi openAI = new OpenAIApi();
    private List<ChatMessage> messages = new List<ChatMessage>();

    public UnityEvent<string> OnResponseEvent;
    
    public async void Ask(string text)
    {
        var msg = new ChatMessage();
        msg.Content = text;
        msg.Role = "user";
        
        messages.Add(msg);

        var request = new CreateChatCompletionRequest();
        request.Messages = messages;
        request.Model = "gpt-3.5-turbo";

        var response = await openAI.CreateChatCompletion(request);

        if (response.Choices != null && response.Choices.Count > 0)
        {
            var chatResponse = response.Choices[0].Message;
            messages.Add(chatResponse);

            OnResponseEvent?.Invoke(chatResponse.Content);
        }
    }
}
