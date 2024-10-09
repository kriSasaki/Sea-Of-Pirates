using Project.Players.Logic;

namespace Project.Interactables
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