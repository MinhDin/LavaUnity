using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SceneInfo
{
	public SceneType Type;
	public List<object> AdditionInfo;
	
	public SceneInfo(SceneType type)
	{
		Type = type;
		AdditionInfo = null;
	}

	public SceneInfo(SceneType type, object obj)
	{
		Type = type;
		AdditionInfo = new List<object>(1);
		AdditionInfo.Add(obj);
	}
}
