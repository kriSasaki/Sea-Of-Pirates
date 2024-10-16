using Scripts.Interactables;
using Scripts.Players.Logic;
using UnityEngine;

public class DamageZone : InteractableZone
{
    [SerializeField] private int _damage = 10;

    protected override void OnPlayerEntered(Player player)
    {
        base.OnPlayerEntered(player);

        player.TakeDamage(_damage);
    }

    protected override void OnPlayerExited(Player player)
    {
        base.OnPlayerExited(player);

        Destroy(gameObject);
    }
}
