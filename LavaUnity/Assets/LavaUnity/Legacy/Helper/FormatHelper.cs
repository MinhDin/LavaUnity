using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Helper 
{
	public static string FloatPercentFormat(float percent)
	{
		return Mathf.FloorToInt(percent * 100).ToString() + '%';
	}

	public static string TimeFormat(TimeSpan timeSpan)
	{
		return string.Format("{0}", timeSpan);
	}

	public static TimeSpan TimeCountDown(long lastTime, double countdownTime)
	{
		double deltaSecond = (System.DateTime.Now.Ticks - lastTime) / System.TimeSpan.TicksPerSecond;
		if(deltaSecond >= countdownTime)
		{			
			return TimeSpan.Zero;
		}
		else
		{
			return System.TimeSpan.FromSeconds(countdownTime - deltaSecond);
		}
	}

	public static Vector3 CenterOfRectTransform(RectTransform trans)
	{
		Vector3[] corner = new Vector3[4];
		trans.GetWorldCorners(corner);
		return (corner[0] + corner[1] + corner[2] + corner[3]) / 4;
	}
}
