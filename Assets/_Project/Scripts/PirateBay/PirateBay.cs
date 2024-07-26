using Project.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateBay : MonoBehaviour
{
    [SerializeField] private GameObject _canvasPirateBay;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
           
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
           
        }
    }

}
