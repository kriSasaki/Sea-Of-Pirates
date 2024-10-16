using Scripts.Utils;
using Zenject;

namespace Scripts.UI.Upgrades
{
    public class UpgradeButton : UiButton
    {
        [Inject]
        private void Construct()
        {
            Hide();
        }
    }
}