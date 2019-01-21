using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class XInt : ScriptableObject
{
    public int Value;
    public Action OnValueChange;
    
#if UNITY_EDITOR
    [MenuItem("Scriptable/XData/XInt")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<XInt>();
    }
#endif
}
