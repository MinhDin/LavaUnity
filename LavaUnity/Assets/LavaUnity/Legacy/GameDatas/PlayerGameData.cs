using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerGameData : ScriptableObject 
{
    public PlayerSavePack SavedPack;
    public PlayerSaveData SaveData{get{return SavedPack.SaveData;}}
    public PlayerSaveData UIData{get{return SavedPack.UIData;}}

#if UNITY_EDITOR
    [MenuItem("Scriptable/GameData/PlayerGameData")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<PlayerGameData>();
    }
#endif
}

[System.Serializable]
public class PlayerSavePack
{
    public PlayerSaveData SaveData;
    public PlayerSaveData UIData;

    public PlayerSavePack()
    {
        SaveData = new PlayerSaveData();
        UIData = new PlayerSaveData();
    }
}

