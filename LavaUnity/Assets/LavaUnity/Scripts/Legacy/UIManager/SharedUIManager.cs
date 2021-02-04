using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SharedUIManager : MonoBehaviour
{
    public Canvas SharedCanvas;

    public Action<SceneSharedEle> OnElementTap;
    
    public SceneSharedEle LastShowEle
    {
        get
        {
            return lastShowEle;
        }
    }
    UIManager uiMgr;
    GameEvents gameE;
    Factory gameFactory;

    SceneSharedEle lastShowEle;

    bool inited = false;

    private void Awake()
    {
		Init();
    }

    void Init()
    {
        if (!inited)
        {
            inited = true;
            
            gameE = Global.Instance.GameE;
            gameFactory = Global.Instance.GameFactory;
            uiMgr = Global.Instance.UIMgr;
        }
    }

    public void ShowSharedUI(SceneSharedEle ele)
    {
        Init();
		gameObject.SetActive(true);
        
        lastShowEle = ele;
    }

    public void ShowSharedUI(SceneSharedEle ele, int order, string layerName)
    {
		ShowSharedUI(ele);
        SharedCanvas.sortingOrder = order;
        SharedCanvas.sortingLayerName = layerName;
    }

    public void BtnGroupClassicPress(SceneType type)
    {
        SceneBase curScene = uiMgr.FindScene(SceneGroup.CLASSIC);
        if (curScene == null)
        {
            uiMgr.PushScene(type);
        }
        else
        {
            if (curScene.Type == type)
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

    public void OnElementBtnDown(int ele)
    {
        if (OnElementTap != null)
        {
            OnElementTap((SceneSharedEle)ele);
        }
    }
}