using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeUpdater : MonoBehaviour
{
    public Action<DateTime> OnNewDay;
    public Action<DateTime> OnPass12Noon;
	public Action OnTimeTick;
	public TimeSpan TimeFromInstall
	{
		return new TimeSpan(secondFromInstall);
	}
    const string SecondsFromAnchorKey = "SecondsFromAnchorKey";
    public static DateTime INITIAL_DAY = new DateTime(2018, 0, 0, 0, 0, 0, DateTimeKind.Local);

    float delayUpdateCounter;
    const float UpdateTimeFrequent = 1;
    int lastDay;
    int lastNoonDay;

	float time;
	long secondFromInstall;

    void Awake()
    {
        DateTime now = DateTime.Now;

        int secondsFromAnchor = PlayerPrefs.GetInt(SecondsFromAnchorKey, 0);
        if (secondsFromAnchor == 0)
        {
            secondsFromAnchor = (int)now.Subtract(INITIAL_DAY).TotalSeconds;
            PlayerPrefs.SetInt(SecondsFromAnchorKey, secondsFromAnchor);
        }
        secondFromInstall = (int)now.Subtract(INITIAL_DAY).TotalSeconds - PlayerPrefs.GetInt(SecondsFromAnchorKey, 0);

        lastDay = now.Day;
        lastNoonDay = now.AddHours(-12).Day;
    }

    void Update()
    {
        delayUpdateCounter += Time.unscaledDeltaTime;

        if (delayUpdateCounter >= UpdateTimeFrequent)
        {
            DateTime now = DateTime.Now;
            int newValue = (int)now.Subtract(INITIAL_DAY).TotalSeconds - PlayerPrefs.GetInt(SecondsFromAnchorKey, 0);
            if (newValue != secondFromInstall)
            {
                secondFromInstall = newValue;
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
