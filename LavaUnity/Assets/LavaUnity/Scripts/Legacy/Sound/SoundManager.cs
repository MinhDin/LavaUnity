using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    public const string KEY_BGM_SETTING = "Key_BGM_SETTING";
    public const string KEY_FX_SETTING = "Key_FX_SETTING";
    public const string KEY_VIBRATE_SETTING = "Key_VIBRATE_SETTING";

    public SoundConfig SoundConfig{get{return soundConfig;}}
    public bool IsBGM
    {
        get
        {
            return isBGM;
        }
        set
        {
            isBGM = value;
            PlayerPrefs.SetInt(KEY_BGM_SETTING, isBGM ? 1 : 0);
            RefreshSoundSnapShot();
        }
    }
    public bool IsFX
    {
        get
        {
            return isFX;
        }
        set
        {
            isFX = value;
            PlayerPrefs.SetInt(KEY_FX_SETTING, isFX ? 1 : 0);
            if(isFX)
            {
                RefreshSoundSnapShot(0);
            }
            else
            {
                RefreshSoundSnapShot();
            }
        }
    }
    public bool IsVibrate
    {
        get
        {
            return isVibrate;
        }
        set
        {
            isVibrate = value;
            PlayerPrefs.SetInt(KEY_VIBRATE_SETTING, isVibrate ? 1 : 0);
        }
    }
    SoundConfig soundConfig;
    AudioSource sfxSource;
    AudioSource bgmSource;
    GameObject objHolder;
    int initAmount;
    bool isBGM;
    bool isFX;
    bool isVibrate;

    public SoundManager(int amount = 10)
    {
        soundConfig = Global.Instance.SoundConfig;

        this.initAmount = amount;
    }

    public void Init()
    {
        soundConfig.GenerateDictionary();

        objHolder = new GameObject("SoundManager");

        bgmSource = objHolder.AddComponent<AudioSource>();
        bgmSource.loop = true;
        bgmSource.outputAudioMixerGroup = soundConfig.BgmGroup;
        bgmSource.playOnAwake = false;

        sfxSource = objHolder.AddComponent<AudioSource>();
        sfxSource.loop = false;
        sfxSource.outputAudioMixerGroup = soundConfig.SfxGroup;
        sfxSource.playOnAwake = false;

        isBGM = PlayerPrefs.GetInt(KEY_BGM_SETTING, 1) == 1;
        isFX = PlayerPrefs.GetInt(KEY_FX_SETTING, 1) == 1;
        isVibrate = PlayerPrefs.GetInt(KEY_VIBRATE_SETTING, 0) == 1;

        RefreshSoundSnapShot(0);
    }

    ~SoundManager()
    {

    }

    public void Vibrate()
    {
        #if !UNITY_EDITOR && !UNITY_STANDALONE
        if(isVibrate)
        {
            Handheld.Vibrate();
        }
        #endif
    }

    public void PlaySfx(SoundID id, float pitch = 1, float volumnScale = 1)
    {
        sfxSource.pitch = pitch;

        sfxSource.PlayOneShot(soundConfig.DClips[id], volumnScale);
    }

    public void PlayBgm(SoundID id, bool loop = true)
    {
        AudioClip clip = soundConfig.DClips[id];
        if ((clip != bgmSource.clip) || !bgmSource.loop)
        {
            bgmSource.clip = clip;
            bgmSource.loop = loop;
            bgmSource.Play();
        }
    }

    public void StopBgm()
    {
        if(bgmSource.isPlaying)
        {
            bgmSource.Stop();
            bgmSource.clip = null;
        }
        //if(isFX)
        //{
        //    soundConfig.BGMOffSnapshot.TransitionTo(time);
        //}
        //else
        //{
        //    soundConfig.AllOffSnapshot.TransitionTo(time);
        //}
    }

    public void RefreshSoundSnapShot(float time = 1)
    {
        if(isBGM && isFX)
        {
            soundConfig.AllOnSnapshot.TransitionTo(time);
        }
        else if(isBGM && !isFX)
        {
            soundConfig.FXOffSnapshot.TransitionTo(time);
        }
        else if(!isBGM && isFX)
        {
            soundConfig.BGMOffSnapshot.TransitionTo(time);
        }
        else if(!isBGM && !isFX)
        {
            soundConfig.AllOffSnapshot.TransitionTo(time);
        }
    }
}
