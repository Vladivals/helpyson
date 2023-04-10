using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System.IO;

[Serializable]
public struct DataOptions
{
	public bool include_latex;
}

[Serializable]
public struct ObjData
{
	public string src;
	public string[] formats;
	public DataOptions data_options;
}

public class Mathpix : MonoBehaviour
{
	public readonly string url = "https://api.mathpix.com/v3/text";
	public readonly string appId = "starostenkovlad_gmail_com_aa9f05_b76088";
	public readonly string appKey = "6db0fab128fc858e68f4c6735294da47d1f2bb357f1d67b2abc1150a32b9f18b";
	public readonly string remoteImageSrc = "https://mathpix-ocr-examples.s3.amazonaws.com/cases_hw.jpg";
	public string localImageSrc = "D:/UNITYPROJECTS/helpyson-main/Assets/cases_hw.jpg";
	public string latexResult;

	[ContextMenu("Send url image")]
	public void SendUrl()
	{
		ObjData data = new ObjData
		{
			src = remoteImageSrc,
			formats = new string[2]{ "data", "text" },
			data_options = new DataOptions
			{
				include_latex = true
			}
		};

		StartCoroutine(Send(data));
	}

	[ContextMenu("Send local image")]
	public void SendLocal(Action callback = null)
	{
		ObjData data = new ObjData
		{
			src = "data:image/jpeg;base64," + Convert.ToBase64String(File.ReadAllBytes(localImageSrc)),
			formats = new string[2] { "data", "text" },
			data_options = new DataOptions
			{
				include_latex = true
			}
		};

		StartCoroutine(Send(data, callback));
	}

	public IEnumerator Send(ObjData data, Action callback = null)
	{
		var json = JsonUtility.ToJson(data);
		var bytes = Encoding.UTF8.GetBytes(json);

		Debug.Log(json);
		
		UnityWebRequest request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);
		request.SetRequestHeader("Content-Type", "application/json");
		request.SetRequestHeader("app_id", appId);
		request.SetRequestHeader("app_key", appKey);

		request.uploadHandler = new UploadHandlerRaw(bytes);
		request.downloadHandler = new DownloadHandlerBuffer();

		yield return request.SendWebRequest();

		if (request.result == UnityWebRequest.Result.Success)
		{
			Debug.Log($"Post request complete! Response Code: {request.responseCode}");
			Debug.Log($"Response Text: {request.downloadHandler.text}");
			var responseJson = JsonUtility.FromJson<MathpixResponse>(request.downloadHandler.text);

			latexResult = responseJson.data[0].value;
			callback?.Invoke();
			Debug.Log(latexResult);
		}
		else
		{
			Debug.Log($"Response Code: {request.responseCode}");
			Debug.Log(request.error);
		}
	}

	[Serializable]
	public class MathpixResponse
	{
		public Data[] data;
	}

	[Serializable]
	public class Data
	{
		public string value;
	}
}

