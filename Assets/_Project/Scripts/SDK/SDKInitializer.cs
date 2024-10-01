using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Project.SDK
{
    public class SDKInitializer : MonoBehaviour
    {
        [SerializeField, Scene] private string _loadingScene;

        private IEnumerator Start()
        {
            yield return null;
            //yield return Agava.YandexGames.YandexGamesSdk.Initialize(LoadScene);
            yield return new WaitUntil(() => YandexGame.SDKEnabled == true);
#if UNITY_WEBGL && !UNITY_EDITOR

#else
            LoadScene();
#endif
        }

        private void LoadScene()
        {
            SceneManager.LoadScene(_loadingScene);
        }
    }
}