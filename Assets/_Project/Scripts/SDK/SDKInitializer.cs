using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Scripts.SDK
{
    public class SDKInitializer : MonoBehaviour
    {
        [SerializeField, Scene] private string _loadingScene;

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => YandexGame.SDKEnabled == true);

            LoadScene();
        }

        private void LoadScene()
        {
            SceneManager.LoadScene(_loadingScene);
        }
    }
}