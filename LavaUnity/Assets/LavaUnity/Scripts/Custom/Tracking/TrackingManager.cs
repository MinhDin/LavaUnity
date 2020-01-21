using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class TrackingManager : MonoBehaviour
{
    public static TrackingManager Instance;
    Action WaitForServiceAvailableTask;

    Action onAwake;
    Action onStart;
    Action onDestroy;
    Action onApplicationQuit;
    Action onUpdate;
    Action<bool> onApplicationFocus;
    Action<bool> onApplicationPause;

    bool IsServiceAvailable()
    {
        return false;
    }

   // public static TrackingParameter[] ToArray(params TrackingParameter[] param)
    //{
    //    return param;
    //}

    string GetTodayInString()
    {
        return DateTime.Now.Year.ToString("D4") + DateTime.Now.Month.ToString("D2") + DateTime.Now.Day.ToString("D2");
    }

    private void Awake()
    {
        onAwake?.Invoke(); 
    }

    private void Start()
    {
        onStart?.Invoke(); 
#if UNITY_IOS
        OnApplicationPause(false);
#endif
    }

    private void Update()
    {
        onUpdate?.Invoke();
        if (WaitForServiceAvailableTask != null)
        {
            if (IsServiceAvailable())
            {
                WaitForServiceAvailableTask();
                WaitForServiceAvailableTask = null;
            }
        }
    }

    private void OnApplicationFocus(bool focusStatus)
    {
        onApplicationFocus?.Invoke(focusStatus);
    }

    public void OnApplicationPause(bool pauseStatus)
    {
        onApplicationPause?.Invoke(pauseStatus);
    }

    private void OnApplicationQuit()
    {
        onApplicationQuit?.Invoke();
    }

    private void OnDestroy()
    {
        onDestroy?.Invoke();
    }

}
