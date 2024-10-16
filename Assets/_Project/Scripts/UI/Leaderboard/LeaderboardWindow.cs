using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Leaderboard
{
    public class LeaderboardWindow : UiWindow
    {
        [SerializeField] private RectTransform _leaderboardPanel;
        [SerializeField] private RectTransform _authorizationPanel;
        [SerializeField] private Button _authorizationButton;
        [SerializeField] private RectTransform _loadingPlaceHolder;

        public event Action AuthorizationRequested;

        protected override void Awake()
        {
            base.Awake();

            _authorizationButton.onClick.AddListener(OnAuthorizationClicked);
            Hide();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _authorizationButton.onClick.RemoveListener(OnAuthorizationClicked);
        }

        public void OpenLeaderboardPanel()
        {
            _leaderboardPanel.gameObject.SetActive(true);

            Show();
        }

        public void OpenAuthorizationPanel()
        {
            _authorizationPanel.gameObject.SetActive(true);

            Show();
        }

        public void OpenLoadingPlaceholder()
        {
            _loadingPlaceHolder.gameObject.SetActive(true);

            Show();
        }

        public override void Hide()
        {
            base.Hide();
            _authorizationPanel.gameObject.SetActive(false);
            _leaderboardPanel.gameObject.SetActive(false);
        }

        private void OnAuthorizationClicked()
        {
            AuthorizationRequested?.Invoke();
            Hide();
        }
    }
}