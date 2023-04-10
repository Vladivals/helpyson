using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsBehavior : MonoBehaviour
{
	private void Update() {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			PlayerPrefs.DeleteAll();
		}
	}
}
