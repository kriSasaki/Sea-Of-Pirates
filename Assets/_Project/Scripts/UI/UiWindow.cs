using Ami.BroAudio;
using Scripts.Configs.UI;
using Scripts.Interfaces.Audio;
using Scripts.Utils.Tweens;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.UI
{
    [RequireComponent(typeof(Canvas))]
    public class UiWindow : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private ScaleTween _scaleTween;

        private IAudioService _audioService;
        private SoundID _openWindowSound;
        private SoundID _closeWindowSound;

        private Canvas _windowCanvas;

        private bool IsClosed => _windowCanvas.enabled == false;

        protected virtual void Awake()
        {
            _windowCanvas = GetComponent<Canvas>();

            _scaleTween.Initialize(transform);
            _closeButton.onClick.AddListener(Hide);
            Close();
        }

        protected virtual void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(Hide);
        }

        public virtual void Hide()
        {
            if (IsClosed)
                return;

            _audioService.PlaySound(_closeWindowSound);
            Close();
        }

        protected void Show()
        {
            _windowCanvas.enabled = true;
            _audioService.PlaySound(_openWindowSound);
            _scaleTween.RunFrom();
        }

        private void Close()
        {
            _windowCanvas.enabled = false;
        }

        [Inject]
        private void Construct(IAudioService audioService, UiConfigs uiConfigs)
        {
            _audioService = audioService;
            _openWindowSound = uiConfigs.OpenWindowSound;
            _closeWindowSound = uiConfigs.CloseWindowSound;
        }
    }
}