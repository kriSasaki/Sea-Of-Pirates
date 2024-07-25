using Project.Interfaces.Quests;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.Quests
{
    public class QuestView
    {
        [SerializeField] private QuestWindow _questWindow;
        [SerializeField] private Button _questButton;

        public QuestView(QuestWindow questWindow, Button questButton)
        {
            _questWindow = questWindow;
            _questButton = questButton;

            if (questButton == null) 
            {
                Debug.LogError("Не прокинули ссылку на кнопку квеста в SceneContextInstaller на сцене");
            }
            if (_questWindow == null) 
            {
                Debug.LogError("Добавьте на сцену префаб QuestWindow");
            }

            Hide();
            _questButton.gameObject.SetActive(false);
        }

        public void Show(IQuest quest)
        {
            _questButton.gameObject.SetActive(true);
            _questButton.onClick.AddListener(() => _questWindow.Show(quest));
        }

        public void Hide()
        {
            _questWindow.Hide();
            _questButton.onClick.RemoveAllListeners();
            _questButton.gameObject.SetActive(false);
        }
    }
}