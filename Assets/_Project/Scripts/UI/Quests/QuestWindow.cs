using Scripts.Interfaces.Quests;
using Scripts.Systems.Quests;
using Scripts.UI.Bars;
using Scripts.Utils.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Quests
{
    public class QuestWindow : UiWindow
    {
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Image _rewardImage;
        [SerializeField] private Image _targetImage;
        [SerializeField] private TMP_Text _rewardAmount;
        [SerializeField] private TMP_Text _progressValue;
        [SerializeField] private FillableBar _progressBar;
        [SerializeField] private Button _confirmButton;

        public void Open(IQuest quest)
        {
            Show();

            _confirmButton.onClick.AddListener(() => OnButtonClicked(quest));

            if (quest.Status.State != QuestState.Done)
                _description.text = quest.Config.Description;
            else
                _description.text = quest.Config.CompleteMessage;

            _rewardAmount.text = quest.Config.Reward.Amount.ToNumericalString();
            _rewardImage.sprite = quest.Config.Reward.Resource.Sprite;
            _targetImage.sprite = quest.Config.TargetType.Icon;

            int currentProgress = quest.Status.Progress;
            int targetProgress = quest.Config.TargetAmount;
            string progress = $"{currentProgress}/{targetProgress}";

            _progressValue.text = progress;
            _progressBar.Set(currentProgress, targetProgress);
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