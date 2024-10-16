using Scripts.Players.Logic;

namespace Scripts.Interactables
{
    public class ZoneUnload : InteractableZone
    {
        protected override void OnPlayerEntered(Player player)
        {
            base.OnPlayerEntered(player);

            player.UnloadHold();
        }
    }
}