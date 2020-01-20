using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public partial class GameEvents : ScriptableObject 
{
	public Action OnGameReady;
    public Action<int> OnGameReadyOrder;
	public Action<GameState> OnChangeState;
    public Action RequestSaveGame;
    public Action MarkDirtySaveGame;
    public Action OnUpdate;
    public Action OnLateUpdate;
    public Action OnSecondTick;
    public Action<DateTime> OnDayPass;
    public Action<bool> OnApplicationPause;
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
