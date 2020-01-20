using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SoundConfig : ScriptableObject
{
    public AudioMixerGroup MasterGroup;
    public AudioMixerGroup BgmGroup;
    public AudioMixerGroup SfxGroup;
    public AudioMixerSnapshot AllOnSnapshot;
    public AudioMixerSnapshot BGMOffSnapshot;
    public AudioMixerSnapshot FXOffSnapshot;
    public AudioMixerSnapshot AllOffSnapshot;
    
    [HideInInspector]
    public List<AudioClip> Clips;
    public Dictionary<SoundID, AudioClip> DClips;

    public void GenerateDictionary()
    {
        Array ids = Enum.GetValues(typeof(SoundID));
        int len = Mathf.Min(ids.Length, Clips.Count);
        DClips = new Dictionary<SoundID, AudioClip>(len);
        
        for(int i = 0; i < len; ++i)
        {
            DClips.Add((SoundID)ids.GetValue(i), Clips[i]);
        }
    }
#if UNITY_EDITOR
    [MenuItem("Lava/Scriptable/Data/SoundConfig")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<SoundConfig>();
    }
#endif
}

public enum SoundID
{
    BGM_1,
    BTN_CLICK_NEUTRAL = 1,//Neutral
    BTN_CLICK_POSITIVE = 2,//Good
    BTN_CLICK_NEGATIVE = 3,//Back

    SHOOT_1_1 = 4,
    SHOOT_1_2 = 5,
    SHOOT_1_3 = 6,
    RELOAD_1 = 7,

    SHOOT_2_1 = 8,
    SHOOT_2_2 = 9,
    SHOOT_2_3 = 10,
    RELOAD_2 = 11,

    SHOOT_3_1 = 12,
    SHOOT_3_2 = 13,
    SHOOT_3_3 = 14,
    RELOAD_3 = 15,

    SHOOT_4_1 = 16,
    SHOOT_4_2 = 17,
    SHOOT_4_3 = 18,
    RELOAD_4 = 19,

    SHOOT_5_1 = 20,
    SHOOT_5_2 = 21,
    SHOOT_5_3 = 22,
    RELOAD_5 = 23,
}
