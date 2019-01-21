using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SharedUIManager : MonoBehaviour 
{	
	public RectTransform HandRectTrans;

	UIManager uiMgr;
	PlayerSaveData saveData;
    GameEvents gameE;
	Factory gameFactory;

	Vector3 handPos;

	private void Awake()
	{
		saveData = Global.Instance.SaveGameMgr.SavedPack.SaveData;
        gameE = Global.Instance.GameE;
		gameFactory = Global.Instance.GameFactory;
		uiMgr = Global.Instance.UIMgr;

		Vector3[] corner = new Vector3[4];
		HandRectTrans.GetWorldCorners(corner);
		handPos = (corner[0] + corner[1] + corner[2] + corner[3]) / 4;
	}

	public void ShowSharedUI(SceneSharedEle ele)
	{
		gameObject.SetActive(true);
	}

	public void OnCollectionBtnPress()
	{
		BtnGroupClassicPress(SceneType.COLLECTION);
	}

	public void OnIslandBtnPress()
	{
		BtnGroupClassicPress(SceneType.ISLAND);
	}

	public void OnSettingBtnPress()
	{
		BtnGroupClassicPress(SceneType.SETTING);
	}

	public void OnShopBtnPress()
	{
		BtnGroupClassicPress(SceneType.SHOP);
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
