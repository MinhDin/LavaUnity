using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DebugMenuUI : MonoBehaviour
{
    public GameObject CheatMenu;
    Config config{get{return Global.Instance.GameConfig;}}
    public Slider ObstacleSpeedSlider;
    public Slider PlayerShootRangeSlider;
    public InputField FinalSuccessRangeX;
    public InputField FinalSuccessRangeY;

    private void Awake()
    {
#if !UNITY_EDITOR
#if !ENABLE_CHEAT
        Destroy(this.gameObject);
        return;
#endif
#endif
        ObstacleSpeedSlider.value = config.ObstacleSpeed;
        PlayerShootRangeSlider.value = config.PlayerShootRange;
        FinalSuccessRangeX.text = config.FinalSucessRange.x.ToString();
        FinalSuccessRangeY.text = config.FinalSucessRange.y.ToString();
    }

    public void OnToggle(bool active)
    {
        CheatMenu.SetActive(active);
    }

#if UNITY_EDITOR
    private void OnDestroy()
    {
    }
#endif
    public void OnObstacleSpeedSlider(float value)
    {
        config.ObstacleSpeed = value;
    }
    public void OnPlayerShootRangeSlider(float value)
    {
        config.PlayerShootRange = value;
    }
    public void OnFinalSuccessRangeX(string value)
    {
        config.FinalSucessRange.x = float.Parse(value);
    }

    public void OnFinalSuccessRangeY(string value)
    {
        config.FinalSucessRange.y = float.Parse(value);
    }

    public void OnSoundSetting(int index)
    {
        config.ShootSoundStart = (SoundID)index;
    }

    public void OnRestartGame()
    {
        Global.Instance.GameE.RequestRestartSong?.Invoke();
    }
}
