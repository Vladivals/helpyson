using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubscribeScreenBehaviour : MonoBehaviour
{
    public Button close;

    private void Start()
    {
        close.onClick.AddListener(() => this.gameObject.SetActive(false));
    }

    private void OnDestroy()
    {
        close.onClick.RemoveAllListeners();
    }
}
