using Project.Interfaces.Quests;
using UnityEngine;

namespace Project.UI.Quests
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