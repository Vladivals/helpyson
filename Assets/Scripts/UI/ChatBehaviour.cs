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

    public float minHeight = 30f; // ����������� ������ ���� �����
    public float maxHeight = 200f; // ������������ ������ ���� �����
    public float extraHeight = 5f; // �������������� ������ ��� �������������� ��������� ������
    private RectTransform _inputFieldRectTransform;
    private TMPro.TMP_Text _inputFieldText;

    private void OnEnable()
    {
        inputField.onValueChanged.AddListener(OnValueChanged);
    }

    void Start()
    {
        _inputFieldRectTransform = inputField.GetComponent<RectTransform>();
        _inputFieldText = inputField.textComponent;
    }

    private void Update()
    {
        OpenCloseMessageArea();
        // ���������� ����������� � ������������ ������ ���� �����
        SetMaxMinHeight();
    }

    private void SetMaxMinHeight()
    {
        _inputFieldRectTransform.sizeDelta = new Vector2(_inputFieldRectTransform.sizeDelta.x,
                    Mathf.Clamp(_inputFieldText.preferredHeight + extraHeight, minHeight, maxHeight));
    }

    private void OpenCloseMessageArea()
    {
        var currentSelectedObject = EventSystem.current.currentSelectedGameObject;
        messageArea.SetActive(
            currentSelectedObject == button.gameObject
            || currentSelectedObject == inputField.gameObject
            || currentSelectedObject == messageArea.gameObject
        );
        messageArea.transform.DOScale(Convert.ToInt32(messageArea.activeSelf), 1);
    }

    public void OnValueChanged(string value)
    {
        button.gameObject.active = value.Length >= 1 ? true : false;
    }
}
