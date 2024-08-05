using Project.Utils;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    [RequireComponent(typeof(Button))]
    public abstract class UiButton : MonoBehaviour
    {
        [SerializeField] private ScaleTween _scaleTween;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _scaleTween.Initialize(transform);
            Hide();
        }
        public void Show(Action onClickCallback)
        {
            gameObject.SetActive(true);
            _scaleTween.Run();
            _button.onClick.AddListener(onClickCallback.Invoke);
        }

        public void Hide()
        {
            _button.onClick.RemoveAllListeners();
            gameObject.SetActive(false);
        }
    }
}