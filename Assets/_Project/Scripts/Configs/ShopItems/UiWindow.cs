using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class UiWindow : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    private Canvas _windowCanvas;

    private void Awake()
    {
        _windowCanvas = GetComponent<Canvas>();
        _closeButton.onClick.AddListener(Hide);

        Hide();
    }

    protected virtual void OnDestroy()
    {
        _closeButton.onClick.RemoveListener(Hide);
    }

    public virtual void Show()
    {
        _windowCanvas.enabled = true;
    }

    public virtual void Hide()
    {
        _windowCanvas.enabled = false;
    }
}