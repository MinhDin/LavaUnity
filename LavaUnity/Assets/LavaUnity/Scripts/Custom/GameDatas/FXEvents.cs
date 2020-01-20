using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public partial class FXEvents : ScriptableObject
{
    #if UNITY_EDITOR
    [MenuItem("Lava/Scriptable/Events/FXEvents")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<FXEvents>();
    }
#endif
}
