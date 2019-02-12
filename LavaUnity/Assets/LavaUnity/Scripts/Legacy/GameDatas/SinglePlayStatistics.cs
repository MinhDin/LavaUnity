using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SinglePlayStatistics : ScriptableObject
{
	public float X_Highest;
	public int Level_Total_Passed;

#if UNITY_EDITOR
    [MenuItem("Lava/Scriptable/GameData/SinglePlayStatistics")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<SinglePlayStatistics>();
    }
#endif
}

