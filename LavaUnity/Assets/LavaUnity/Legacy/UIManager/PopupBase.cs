using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupBase : SceneBase
{
	public override bool CanShowNow(List<SceneBase> curScene)
	{
		int len = curScene.Count;
		for(int i = 0; i < len; ++i)
		{
			if(curScene[i].Group == SceneGroup.POPUP)
			{
				return false;
			}
		}

		return true;
	}

	public void OnBtnClose()
	{
		uiMgr.CloseScene(Type);
	}
}
