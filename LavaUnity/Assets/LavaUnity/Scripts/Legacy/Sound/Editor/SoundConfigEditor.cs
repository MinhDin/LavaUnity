using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(SoundConfig))]
public class SoundConfigEditor : Editor
{
    SoundConfig sound;

    private void OnEnable()
    {
        sound = target as SoundConfig;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
       Array ids = Enum.GetValues(typeof(SoundID));
       int len = ids.Length;
       if(sound.Clips == null)
       {
           sound.Clips = new List<AudioClip>();
       }
       while(sound.Clips.Count < len)
       {
           sound.Clips.Add(null);
       }
       while(sound.Clips.Count > len)
       {
           sound.Clips.RemoveAt(sound.Clips.Count - 1);
       }
       for(int i = 0;i < len; ++i)
       {
           sound.Clips[i] = (AudioClip)EditorGUILayout.ObjectField(ids.GetValue(i).ToString(), sound.Clips[i], typeof(AudioClip), false);
       }

       if(GUI.changed)
       {
           EditorUtility.SetDirty(sound);
           AssetDatabase.SaveAssets();
           AssetDatabase.Refresh();
       }
    }
}
