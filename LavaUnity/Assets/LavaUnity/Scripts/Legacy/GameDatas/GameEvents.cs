using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameEvents : ScriptableObject 
{
	public Action OnGameReady;
    public Action<int> OnGameReadyOrder;
	public Action<GameState> OnChangeState;
    public Action RequestSaveGame;
    public Func<string, int, bool> RequestRewardedVideo;
    public Action OnLateUpdate;
    
    //Legacy
    public Action<StackChangeInfo> OnUIStackChange;
    
#if UNITY_EDITOR
    [MenuItem("Lava/Scriptable/Events/GameEvents")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<GameEvents>();
    }
#endif
}
