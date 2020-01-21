using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SceneType : uint
{	
	ACTION = 1,
}

[Flags]
public enum SceneSharedEle
{
	NONE = 0,
	//special
	KEEP = 1,
	//normal
	NORMALSTART = 128,
}

[Flags]
public enum SceneGroup
{
	CLASSIC = 1,
	POPUP = 2,
}
