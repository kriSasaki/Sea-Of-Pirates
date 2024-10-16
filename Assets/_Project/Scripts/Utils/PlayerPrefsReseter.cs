using UnityEngine;
using YG;

namespace Scripts.Utils
{
    public static class PlayerPrefsReseter
    {
        public static void ResetPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            YandexGame.ResetSaveProgress();
            YandexGame.SaveProgress();

            Debug.Log("Player Prefs is reseted");
        }
    }
}