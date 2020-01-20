using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public partial class Config : ScriptableObject
{
    public float ObstacleSpeed = 9;
    public Vector2 FinalSucessRange = new Vector2(1.9f, 3.0f);
    public float PlayerShootRange = 2;
    public SoundID ShootSoundStart = SoundID.SHOOT_1_1;
    public float SongOffset = 0.25f;
    public int ShootToReload = 2;
    public float ReloadTimeRequire = 1f;
#if UNITY_EDITOR
    [MenuItem("Lava/Scriptable/Data/Config")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<Config>();
    }
#endif
}
