using Scripts.Players.Logic;

namespace Scripts.Interactables
{
    public class ZoneHeal : InteractableZone
    {
        protected override void OnPlayerEntered(Player player)
        {
            base.OnPlayerEntered(player);

            player.Heal();
        }
    }
}