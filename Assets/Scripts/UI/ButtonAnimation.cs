using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonAnimation : MonoBehaviour
{
    private Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickAnimation);
    }

    private void OnClickAnimation()
    {
        button.transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 0.1f)
            .OnComplete(() => button.transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f));
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
