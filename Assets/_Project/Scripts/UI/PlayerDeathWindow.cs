using System;
using Ami.BroAudio;
using Scripts.Configs.UI;
using Scripts.Interfaces.Audio;
using Scripts.Utils.Tweens;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.UI
{
    public class PlayerDeathWindow : MonoBehaviour
    {
        [SerializeField] private Button _confirmButton;
        [SerializeField] private ScaleTween _scaleTween;
        [SerializeField] private Canvas _windowCanvas;

        private IAudioService _audioService;
        private SoundID _playerLooseSound;

        public void Show(Action onConfirmCallback)
        {
            _confirmButton.onClick.AddListener(() =>
            {
                onConfirmCallback();
                Hide();
            });

            _windowCanvas.enabled = true;
            _audioService.PlaySound(_playerLooseSound);
            _scaleTween.RunFrom();
        }

        public void Hide()
        {
            _windowCanvas.enabled = false;

            _confirmButton.onClick.RemoveAllListeners();
        }

        [Inject]
        private void Construct(IAudioService audioService, UiConfigs uiConfigs)
        {
            _audioService = audioService;
            _playerLooseSound = uiConfigs.PlayerLooseSound;
            _windowCanvas = GetComponent<Canvas>();
            _scaleTween.Initialize(transform);
            Hide();
        }
    }
}