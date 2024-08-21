using Project.Interfaces.Quests;
using Project.Utils.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.Quests
{
    public class QuestWindow : UiWindow
    {
        [SerializeField] private TMP_Text _decription;
        [SerializeField] private Image _rewardImage;
        [SerializeField] private TMP_Text _rewardAmount;
        [SerializeField] private TMP_Text _progressValue;
        [SerializeField] private Button _confirmButton;

        public void Open(IQuest quest)
        {
            base.Show();

            _confirmButton.onClick.AddListener(() => OnButtonClicked(quest));

            _decription.text = quest.Config.Description;
            _rewardAmount.text = quest.Config.Reward.Amount.ToNumericalString();
            _rewardImage.sprite = quest.Config.Reward.Resource.Sprite;

            int currentProgress = quest.Status.Progress;
            int targetProgress = quest.Config.TargetAmount;
            string progress = $"{currentProgress}/{targetProgress}";
            _progressValue.text = progress;
        }

        public override void Hide()
        {
            base.Hide();
            _confirmButton.onClick.RemoveAllListeners();
        }

        private void OnButtonClicked(IQuest quest)
        {
            quest.UpdateState();
            Hide();
        }
    }
}