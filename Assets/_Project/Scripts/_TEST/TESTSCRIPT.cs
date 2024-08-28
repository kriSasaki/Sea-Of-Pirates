using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.SceneManagement;

public class TESTSCRIPT : MonoBehaviour
{
    [SerializeField] TESTVLE _test;
    [SerializeField,Scene] string _sceneName;

    private void Start()
    {
        _test = Instantiate(_test);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Destroy(_test);
            return;
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            Destroy(gameObject);
            SceneManager.LoadScene(_sceneName);
            return;
        }

        if (Input.GetKeyUp (KeyCode.R))
        {
            _test.Change();
        }
    }
}
