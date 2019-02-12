using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public partial class Config : ScriptableObject
{
	//[Header("Example")]
	//public float Difficult = 0.52f;
	//public float MoneyRate = 0.25f;
	
#if UNITY_EDITOR
    [MenuItem("Lava/Scriptable/Data/Config")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<Config>();
    }
#endif
}
