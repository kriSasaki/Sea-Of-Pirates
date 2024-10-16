using Scripts.Interfaces.Quests;
using UnityEngine;

namespace Scripts.UI.Quests
{
    public class QuestView
    {
        [SerializeField] private QuestWindow _questWindow;
        [SerializeField] private QuestButton _questButton;

        public QuestView(QuestWindow questWindow, QuestButton questButton)
        {
            _questWindow = questWindow;
            _questButton = questButton;
        }

        public void Show(IQuest quest)
        {
            _questButton.Show(() => _questWindow.Open(quest));
        }

        public void Hide()
        {
            _questWindow.Hide();
            _questButton.Hide();
        }
    }
}