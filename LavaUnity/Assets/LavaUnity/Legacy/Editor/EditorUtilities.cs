using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class EditorUtilities 
{
	public static List<T> GetAllAssetsAtPath<T>(string path) where T : Object
    {
        List<T> rs = new List<T>();
        string[] paths = AssetDatabase.FindAssets("", new string[]{path});

        for(int i = 0; i < paths.Length; ++i)
        {
            T obj = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(paths[i]));

            if (obj == null)
            {
                continue;
            }

            if (!rs.Contains(obj))
            {
                rs.Add(obj);
            }
        }

        return rs;
    }

}
