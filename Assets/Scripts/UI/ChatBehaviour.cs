using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChatBehaviour : MonoBehaviour
{
    public TMPro.TMP_InputField inputField;
    public GameObject messageArea;
    public Button button;

    private List<RaycastResult> raycastResults;

    private void Update()
    {
        var currentSelectedObject = EventSystem.current.currentSelectedGameObject;
        messageArea.SetActive(
            currentSelectedObject == button.gameObject
            || currentSelectedObject == inputField.gameObject
            || currentSelectedObject == messageArea.gameObject
        );
        messageArea.transform.DOScale(Convert.ToInt32(messageArea.activeSelf), 1);
    }
}
