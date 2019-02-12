using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Flags]
public enum SceneType : uint
{	
	ACTION_PHASE = 1,
	IAP = 2,		
	SETTING = 4,
	SHOP = 8,
	NONE = 0,
	ALL = uint.MaxValue,
}

[Flags]
public enum SceneSharedEle
{
	COIN = 1,
}

[Flags]
public enum SceneGroup
{
	CLASSIC = 1,
	POPUP = 2,
}
