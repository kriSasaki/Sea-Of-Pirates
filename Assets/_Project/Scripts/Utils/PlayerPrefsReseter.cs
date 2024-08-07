using NaughtyAttributes;
using UnityEngine;

namespace Project.Utils
{
    [ExecuteInEditMode]
    public class PlayerPrefsReseter : MonoBehaviour
    {
        [Button("Reset Player Prefs")]
        public void ResetPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();

            Debug.Log("Player Prefs is reseted");
        }
    }
}