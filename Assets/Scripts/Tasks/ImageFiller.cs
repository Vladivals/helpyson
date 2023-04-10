using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImageFiller : MonoBehaviour
{
	public Goal goal;
	public Image image;
	public TMP_Text text;
	public float fillTime;
	private float currentTime;
	private float startFillValue;
	private float startPValue;

	private void Start()
	{
		goal.onSubgoalComplete += Fill;
		image.fillAmount = ((float)goal.subgoalsCompletedCount / (float)goal.subgoalsCount);
		text.text = "%" + (((float)goal.subgoalsCompletedCount / (float)goal.subgoalsCount) * 100f).ToString();
	}

	public IEnumerator FillRoutine(float fillValue)
	{
		while (currentTime < fillTime)
		{
			// var lerpVal = Mathf.Lerp(startFillValue, fillValue, currentTime += Time.deltaTime / fillTime);
			var lerpVal = Mathf.Lerp(startFillValue, fillValue, currentTime / fillTime);
			currentTime += Time.deltaTime;
			image.fillAmount = lerpVal;
			var lerpPVal = (int)(lerpVal * 100f);
			text.text = "%" + lerpPVal.ToString();
			yield return null;
		}
		text.text = "%" + (((float)goal.subgoalsCompletedCount / (float)goal.subgoalsCount) * 100f).ToString();
		currentTime = 0f;
	}
	
	public void Fill(float fillValue)
	{
		Debug.Log("fill val" + fillValue);
		startFillValue = image.fillAmount;
		StartCoroutine(FillRoutine(fillValue));
	}

	private void OnDestroy()
	{
		goal.onSubgoalComplete -= Fill;
	}
}
