using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeUpdater : MonoBehaviour
{
    public Action<DateTime> OnNewDay;
    public Action<DateTime> OnPass12Noon;
	public Action OnTimeTick;
	public TimeSpan InstallTime
	{
		get
		{
			return installTime;
		}
	}
    const string INSTALL_TIME_KEY = "InstallTimeKey";

    float delayUpdateCounter;
    const float UpdateTimeFrequent = 1;
    int lastDay;
    int lastNoonDay;

	float time;
	TimeSpan installTime;

    long lastTimeInSecond;

    void Awake()
    {
        DateTime now = DateTime.Now;

        string timeStr = PlayerPrefs.GetString(INSTALL_TIME_KEY, string.Empty);
        long installTick = 0;

        if(string.IsNullOrEmpty(timeStr))
        {    
            installTick = DateTime.Now.Ticks;
            PlayerPrefs.SetString(INSTALL_TIME_KEY, installTick.ToString());
        }
        else
        {
            long.TryParse(timeStr, out installTick);
        }
        
        installTime = new TimeSpan(installTick);
        lastDay = now.Day;
        lastNoonDay = now.AddHours(-12).Day;
        lastTimeInSecond = now.Ticks / 10000;
    }

    void Update()
    {
        delayUpdateCounter += Time.unscaledDeltaTime;

        if (delayUpdateCounter >= UpdateTimeFrequent)
        {
            DateTime now = DateTime.Now;
            long newTimeInSecond = now.Ticks / 10000;

            if (lastTimeInSecond != newTimeInSecond)
            {
                lastTimeInSecond = newTimeInSecond;
                if (OnTimeTick != null)
                {
                    OnTimeTick();
                }
            }

            delayUpdateCounter -= UpdateTimeFrequent;

            if (lastDay != now.Day)
            {
                lastDay = now.Day;
                if (OnNewDay != null)
                {
                    OnNewDay(now);
                }
            }

            if (lastNoonDay != now.AddHours(-12).Day)
            {
                lastNoonDay = now.AddHours(-12).Day;
                if (OnPass12Noon != null)
                {
                    OnPass12Noon(now);
                }
            }
        }

    }
}