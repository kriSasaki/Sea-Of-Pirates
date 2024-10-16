using System.IO;
using UnityEditor;
using UnityEngine;

namespace Scripts.Utils.CSV
{
    public static class AssetCreator
    {
        public static TextAsset ConvertStringToTextAsset(string path, string folderPath, string text, string fileName)
        {
#if UNITY_EDITOR
            File.WriteAllText(path + fileName + ".csv", text);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            TextAsset textAsset = Resources.Load(folderPath + fileName) as TextAsset;

            return textAsset;
#else
            return default(TextAsset);
#endif
        }
    }
}