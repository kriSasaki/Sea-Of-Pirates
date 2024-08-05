﻿using Project.Utils;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class UiWindow : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private ScaleTween _scaleTween;

    private Canvas _windowCanvas;
    

    private void Awake()
    {
        _windowCanvas = GetComponent<Canvas>();
        _closeButton.onClick.AddListener(Hide);
        _scaleTween.Initialize(transform);

        Hide();
    }

    protected virtual void OnDestroy()
    {
        _closeButton.onClick.RemoveListener(Hide);
    }

    public virtual void Show()
    {
        _windowCanvas.enabled = true;
        _scaleTween.Run();
    }

    public virtual void Hide()
    {
        _windowCanvas.enabled = false;
    }
}