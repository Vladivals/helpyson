using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using OpenAI;

public class ChatManager : MonoBehaviour {

	public Transform content;

	public GameObject chatBarPrefab;

	public List<string> chatData = new List<string>();

	public Sprite user1ChatBarSprite;
	public Sprite user2ChatBarSprite;

	public Color textColor;
	public int fontSize;

	private VerticalLayoutGroup verticalLayoutGroup;

	public InputField inputField;

	// ratio = heightinoriginalscreenheight/originalscreenheight
	// Use this for initialization
	void Start () {
		verticalLayoutGroup = content.GetComponent<VerticalLayoutGroup>();

	}


	public void ShowUserMsg () {
		
		for(int i = 0; i < chatData.Count; i++) {
			StartCoroutine(ShowUserMsgCoroutine (chatData[i]));
		}
	}

	public void SendMsgFromInputfield()
    {
        if (inputField && !string.IsNullOrWhiteSpace(inputField.text))
        {
			chatData.Clear();
			chatData.Add (inputField.text + "~0");
			StartCoroutine(ShowUserMsgCoroutine(chatData[0]));
			inputField.text = "";
		}
    }

	public void Send(ChatMessage message)
	{
		if (message.Content.Count() < 12)
		{
			for (int i = 0; i < 12 - message.Content.Count(); i++)
			{
				message.Content += " ";
			}
		}
		if (message.Role == "user")
		{
			message.Content += "~1";
		}
		else
		{
			message.Content += "~0";
		}
		StartCoroutine(ShowUserMsgCoroutine(message.Content));
	}

	private string lastUser;
	IEnumerator ShowUserMsgCoroutine (string msg) {

		GameObject chatObj = Instantiate(chatBarPrefab,Vector3.zero, Quaternion.identity) as GameObject;
		chatObj.transform.SetParent(content.transform,false);

		chatObj.SetActive(true);

		ChatListObject clb = chatObj.GetComponent<ChatListObject>();

		string[] split  = msg.Split('~');
		msg = split[0];
		// fontSize = (int)(Screen.height*0.03f);

		// clb.parentText.fontSize = fontSize;
		// clb.childText.fontSize = fontSize;
			
		clb.parentText.text = msg;

		clb.childText.color = Color.black;

		yield return new WaitForEndOfFrame();

		float height = chatObj.GetComponent<RectTransform>().rect.height;
		float width = chatObj.GetComponent<RectTransform>().rect.width;

		clb.chatbarImage.rectTransform.sizeDelta = new Vector2(width,height + 20);
		clb.childText.rectTransform.sizeDelta = new Vector2(width,height);
		clb.childText.fontSize = (int)(clb.childText.fontSize * 0.8f);
		clb.childText.rectTransform.sizeDelta = new Vector2(clb.childText.rectTransform.sizeDelta.x * 0.8f, clb.childText.rectTransform.sizeDelta.y * 0.8f);


		clb.childText.text = msg;

		if (split[1] == "0") {
			clb.chatbarImage.sprite = user1ChatBarSprite;

			//clb.userImage.transform.parent.GetComponent<RectTransform>().anchoredPosition = new Vector2(-26f,clb.userImage.transform.parent.GetComponent<RectTransform>().anchoredPosition.y);
			clb.chatbarImage.rectTransform.anchoredPosition = new Vector2(-3f,clb.chatbarImage.rectTransform.anchoredPosition.y);

			lastUser = "0";

		} else if (split[1] == "1") {

			clb.chatbarImage.sprite = user2ChatBarSprite;

			clb.chatbarImage.rectTransform.anchoredPosition = new Vector2(((content.GetComponent<RectTransform>().rect.width-(verticalLayoutGroup.padding.left+verticalLayoutGroup.padding.right))-chatObj.GetComponent<RectTransform>().rect.width),clb.chatbarImage.rectTransform.anchoredPosition.y);

		}

		content.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, content.GetComponent<RectTransform>().sizeDelta.y);
	}
}
