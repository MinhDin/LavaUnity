using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SharedUIManager : MonoBehaviour 
{
	UIManager uiMgr;
	PlayerSaveData saveData;
    GameEvents gameE;
	Factory gameFactory;

	private void Awake()
	{
		saveData = Global.Instance.SaveGameMgr.SavedPack.SaveData;
        gameE = Global.Instance.GameE;
		gameFactory = Global.Instance.GameFactory;
		uiMgr = Global.Instance.UIMgr;
	}

	public void ShowSharedUI(SceneSharedEle ele)
	{
		gameObject.SetActive(true);
	}

	public void OnSettingBtnPress()
	{
		BtnGroupClassicPress(SceneType.SETTING);
	}

	public void BtnGroupClassicPress(SceneType type)
	{
		SceneBase curScene = uiMgr.FindScene(SceneGroup.CLASSIC);
		if(curScene == null)
		{
			uiMgr.PushScene(type);
		}
		else
		{
			if(curScene.Type == type)
			{
				uiMgr.CloseScene(type);
			}
			else
			{
				uiMgr.CloseScene(curScene.Type);
				uiMgr.PushScene(type);
			}
		}
	}
}
