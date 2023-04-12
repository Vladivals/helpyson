using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImageFiller : MonoBehaviour
{
	public Goal goal;
	public GameObject percents;
	public Sprite red;
	public Sprite white;
	public Image mainImage;
	public Image fillImage;
	public TMP_Text text;
	public float fillTime;
	private float currentTime;
	private float startFillValue;
	private float startPValue;

	private void Start()
	{
		if ((float)goal.subgoalsCompletedCount / (float)goal.subgoalsCount < 0.5f)
		{
			mainImage.sprite = red;
		}
		else
		{
			mainImage.sprite = white;
		}

		goal.onSubgoalComplete += Fill;
		fillImage.fillAmount = ((float)goal.subgoalsCompletedCount / (float)goal.subgoalsCount);
		text.text = (((float)goal.subgoalsCompletedCount / (float)goal.subgoalsCount) * 100f).ToString() + "%";
	}

	public IEnumerator FillRoutine(float fillValue)
	{
		if (!percents.activeSelf) percents.SetActive(true);

		while (currentTime < fillTime)
		{
			// var lerpVal = Mathf.Lerp(startFillValue, fillValue, currentTime += Time.deltaTime / fillTime);
			var lerpVal = Mathf.Lerp(startFillValue, fillValue, currentTime / fillTime);
			currentTime += Time.deltaTime;
			fillImage.fillAmount = lerpVal;
			var lerpPVal = (int)(lerpVal * 100f);
			text.text = lerpPVal.ToString() + "%";
			yield return null;
		}
		text.text = (((float)goal.subgoalsCompletedCount / (float)goal.subgoalsCount) * 100f).ToString() + "%";
		currentTime = 0f;
		if ((float)goal.subgoalsCompletedCount / (float)goal.subgoalsCount < 0.5f)
		{
			mainImage.sprite = red;
		}
		else
		{
			mainImage.sprite = white;
		}

		yield return new WaitForSeconds(2f);
		if (percents.activeSelf) percents.SetActive(false);
	}
	
	public void Fill(float fillValue)
	{
		Debug.Log("fill val" + fillValue);
		startFillValue = fillImage.fillAmount;
		StartCoroutine(FillRoutine(fillValue));
	}

	private void OnDestroy()
	{
		goal.onSubgoalComplete -= Fill;
	}
}
