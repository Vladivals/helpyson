using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class MobileCameraController : MonoBehaviour
{
	public RawImage videoDisplay;
	public Mathpix mathpix;
	public TEXDraw tEXDraw;

	private WebCamTexture webcamTexture;

	void Start()
	{
		// PlayerPrefs.SetInt("HomePageCompletedTasks", 0);
		// PlayerPrefs.SetInt("HomePageCompletedTasks", PlayerPrefs.GetInt("HomePageCompletedTasks") + 1);
		// openCameraButton.onClick.AddListener(OpenCamera);
		// closeCameraButton.onClick.AddListener(CloseCamera);
		// savePhotoButton.onClick.AddListener(SavePhoto);
	}

	// Метод для открытия камеры
	public void OpenCamera()
	{
		videoDisplay.enabled = true;
		if (!HasCameraPermission())
		{
			Debug.Log("Доступ к камере не разрешен.");
			return;
		}

		webcamTexture = new WebCamTexture();
		videoDisplay.texture = webcamTexture;
		webcamTexture.Play();
	}

	// Метод для закрытия камеры
	public void CloseCamera()
	{
		videoDisplay.enabled = false;
		if (webcamTexture != null)
		{
			webcamTexture.Stop();
			videoDisplay.texture = null;
		}
	}

	// Метод для сохранения фотографии в формате JPEG
	public void SavePhoto()
	{
		if (webcamTexture != null)
		{
			// Создаем текстуру из текущего кадра видео с камеры
			Texture2D photo = new Texture2D(webcamTexture.width, webcamTexture.height);
			photo.SetPixels(webcamTexture.GetPixels());
			photo.Apply();

			// Преобразуем текстуру в массив байтов в формате JPEG
			byte[] bytes = photo.EncodeToJPG();

			// Сохраняем массив байтов в файл
			File.WriteAllBytes(Application.dataPath + "/SavedPhoto.jpg", bytes);

			Debug.Log("Фотография сохранена: " + Application.dataPath + "/SavedPhoto.jpg");
		}
		else
		{
			Debug.Log("Нет доступной текстуры для сохранения фотографии.");
		}

		CloseCamera();
		mathpix.localImageSrc = Application.dataPath + "/SavedPhoto.jpg";
		mathpix.SendLocal(() => tEXDraw.text = ParseToLatex(mathpix.latexResult));
	}
	
	private string ParseToLatex(string val)
	{
		var result = val.Replace(@"\\", @"\");
		return result;
	}

	private bool HasCameraPermission()
	{
		return Application.HasUserAuthorization(UserAuthorization.WebCam);
	}
}