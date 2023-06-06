using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenAI
{
	public class ChatGPT : MonoBehaviour
	{
		public Goal goal;
		[SerializeField] private TMPro.TMP_InputField inputField;
		[SerializeField] private Button button;
		[SerializeField] private RectTransform context;

		[SerializeField] private RectTransform sent;
		[SerializeField] private RectTransform received;

		private float height;
		private OpenAIApi openai = new OpenAIApi("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");

		private List<ChatMessage> messages = new List<ChatMessage>();
		private string prompt = "Act as a random stranger in a chat room and reply to the questions. Don't break character. Don't ever mention that you are an AI model.";

		private void Start()
		{
			button.onClick.AddListener(SendReply);
		}

		private async void AppendMessage(ChatMessage message)
		{
			//context.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

			var item = Instantiate(message.Role == "user" ? sent : received, context);
			item.GetChild(0).GetChild(0).GetComponent<Text>().text = "";
			item.anchoredPosition = new Vector2(0, -height);

			foreach (char c in message.Content)
			{
				item.GetChild(0).GetChild(0).GetComponent<Text>().text += c;
				await Task.Delay(50);
			}
			

			await Task.Delay(200);

		
			height += item.sizeDelta.y;
			context.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

			var scrollRect = context.parent?.GetComponent<ScrollRect>();
			if (scrollRect != null)
			{
				// Check if the scrollbar is at the bottom
				bool scrollbarAtBottom = Mathf.Approximately(scrollRect.verticalNormalizedPosition, 0f);

				// Set the verticalNormalizedPosition to 0
				scrollRect.verticalNormalizedPosition = 0f;

				// If the scrollbar was at the bottom, scroll to the bottom again to show the new message
				{
					Canvas.ForceUpdateCanvases();
					scrollRect.verticalNormalizedPosition = 0f;
				}
			}
		}

		private async void SendReply()
		{
			if (string.IsNullOrEmpty(inputField.text))
			{
				return;
			}

			goal.OnSubgoalComplete();
			var newMessage = new ChatMessage()
			{
				Role = "user",
				Content = inputField.text
			};

			inputField.text = null;
			AppendMessage(newMessage);

			if (messages.Count == 0) newMessage.Content = prompt + "\n" + inputField.text;

			messages.Add(newMessage);

			button.enabled = false;
			inputField.enabled = false;

			// Complete the instructions
			var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
			{
				Model = "gpt-3.5-turbo-0301",
				Messages = messages
			});

			if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
			{
				var message = completionResponse.Choices[0].Message;
				message.Content = message.Content.Trim();

				messages.Add(message);
				AppendMessage(message);
			}
			else
			{
				Debug.LogWarning("No text was generated from this prompt.");
			}

			button.enabled = true;
			inputField.enabled = true;
		}
	}
}