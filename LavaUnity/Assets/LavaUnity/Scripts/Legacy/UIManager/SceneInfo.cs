using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct SceneInfo
{
	public SceneType Type;
	public object[] AdditionInfo;
	public Action OnOkClick;
	public Action OnCancelClick;

	public SceneInfo(SceneType type)
	{
		Type = type;
		AdditionInfo = null;
		OnOkClick = null;
		OnCancelClick = null;
	}

	public SceneInfo(SceneType type, params object[] objs)
	{
		Type = type;
		AdditionInfo = objs;
		OnOkClick = null;
		OnCancelClick = null;
	}
}
