using Zenject;

namespace Scripts.UI.Quests
{
    public class QuestButton : UiButton
    {
        [Inject]
        private void Construct()
        {
            Hide();
        }
    }
}