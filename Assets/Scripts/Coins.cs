using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coins : MonoBehaviour
{
	public TMP_Text text;
	public int coinsVal;
	private static Coins coins;
	
	public static Coins Instance 
	{
		get
		{
			return coins;
		}
	}

	private void Awake()
	{
		coins = this;
	}

	private void Start()
	{
		if (!PlayerPrefs.HasKey("coinsVal"))
		{
			PlayerPrefs.SetInt("coinsVal", 0);
		}
		coinsVal = PlayerPrefs.GetInt("coinsVal");
		text.text = coinsVal.ToString();
	}

	public void AddCoins(int value)
	{
		Debug.Log(text + gameObject.name);
		PlayerPrefs.SetInt("coinsVal", PlayerPrefs.GetInt("coinsVal") + value);
		text.text = (coinsVal += value).ToString();
	}

	public void RemoveCoins(int value)
	{
		PlayerPrefs.SetInt("coinsVal", PlayerPrefs.GetInt("coinsVal") - value);
		text.text = (coinsVal -= value).ToString();
	}
}
