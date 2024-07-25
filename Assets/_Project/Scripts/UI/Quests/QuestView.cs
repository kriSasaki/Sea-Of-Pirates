using Project.Interfaces.Quests;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.Quests
{
    public class QuestView : MonoBehaviour
    {
        [SerializeField] private QuestWindow _questWindow;
        [SerializeField] private Button _questButton;

        private void Awake()
        {
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