using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
	public int subgoalsCount;
	public Action<float> onSubgoalComplete;
	public string taskName;

	public int subgoalsCompletedCount;
	
	private void Awake()
	{
		if (PlayerPrefs.HasKey(taskName))
		{
			subgoalsCompletedCount = PlayerPrefs.GetInt(taskName);
		}
		Debug.Log(taskName + subgoalsCompletedCount);
	}

	private void Start()
	{
		if (!PlayerPrefs.HasKey(taskName))
		{
			PlayerPrefs.SetInt(taskName, 0);
		}
	}
	
	public virtual void OnSubgoalComplete()
	{
		if (subgoalsCompletedCount >= subgoalsCount) return;
		subgoalsCompletedCount++;
		onSubgoalComplete?.Invoke(((float)subgoalsCompletedCount / (float)subgoalsCount));
		PlayerPrefs.SetInt(taskName, PlayerPrefs.GetInt(taskName) + 1);
		if (subgoalsCompletedCount >= subgoalsCount)
		{
			OnComplete();
		}
	}

	public virtual void OnComplete()
	{
		Coins.Instance.AddCoins(1);
	}
}
