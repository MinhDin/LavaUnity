using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Flags]
public enum SceneType : uint
{
	COLLECTION = 1 << 0,	
	ACTION_PHASE = 1 << 2,
	IAP = 1 << 3,
	ISLAND = 1 << 4,		
	SETTING = 1 << 5,
	SHOP = 1 << 6,
	//popup
	POPUP_UNLOCK_ITEM = 1 << 15,
	//EXIT = 128,
	NONE = 0,
	ALL = uint.MaxValue,
}

[Flags]
public enum SceneSharedEle
{
	COIN_SCALLOP = 1,	
	BASIC_ICONSET = 2,
}

[Flags]
public enum SceneGroup
{
	CLASSIC = 1,
	POPUP = 2,
}
