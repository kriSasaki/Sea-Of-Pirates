using System;
using Scripts.Configs.UI;
using Scripts.Interfaces.Audio;
using Scripts.Utils.Tweens;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public abstract class UiButton : MonoBehaviour
    {
        [SerializeField] private ScaleTween _scaleTween;

        private Button _button;
        private IAudioService _audioService;
        private UiConfigs _config;

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void Bind(Action onClickCallback)
        {
            _button.onClick.AddListener(onClickCallback.Invoke);
        }

        public void Show(Action onClickCallback)
        {
            gameObject.SetActive(true);
            _audioService.PlaySound(_config.ShowButtonSound);
            _scaleTween.RunFrom();

            Bind(onClickCallback);
        }

        public void Hide()
        {
            _button.onClick.RemoveAllListeners();
            gameObject.SetActive(false);
        }

        [Inject]
        private void Construct(IAudioService audioService, UiConfigs config)
        {
            _button = GetComponent<Button>();
            _audioService = audioService;
            _config = config;

            _scaleTween.Initialize(transform);
        }
    }
}