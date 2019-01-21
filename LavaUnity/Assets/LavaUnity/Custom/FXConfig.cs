using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public partial class FXConfig : ScriptableObject
{
    
#if UNITY_EDITOR
    [MenuItem("Scriptable/Data/FXConfig")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<FXConfig>();
    }
#endif
}