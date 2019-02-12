using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;

public static class ScriptableObjectUtility
{

#if UNITY_EDITOR
    /// <summary>
    //	This makes it easy to create, name and place unique new ScriptableObject asset files.
    /// </summary>
    public static T CreateAsset<T>() where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T>();

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path == "")
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/" + typeof(T).ToString() + ".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;

        return asset;
    }

    public static T CreateAssetAtPath<T>(string path, string name, bool opt = false) where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T>();

        if (!opt)
        {
            if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }
        }
        string assetPathAndName = (path + "/" + name + ".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);

        if (!opt)
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        return asset;
    }
#endif

}