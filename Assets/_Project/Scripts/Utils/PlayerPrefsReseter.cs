using UnityEngine;

public class PlayerPrefsReseter : MonoBehaviour
{
    // Для сбросв префсов в плеймоде выбрать в выпадающем меню компонента (3 точки справа - сверху)
    [ContextMenu("Reset Prefs")]
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}