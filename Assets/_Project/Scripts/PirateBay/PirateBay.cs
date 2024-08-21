using Project.Players.PlayerLogic;
using UnityEngine;

public class PirateBay : MonoBehaviour
{
    [SerializeField] private GameObject _canvasPirateBay;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            _canvasPirateBay.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
          
            _canvasPirateBay.SetActive(false);
        }
    }
}
