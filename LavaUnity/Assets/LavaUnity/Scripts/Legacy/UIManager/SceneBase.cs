using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(CanvasGroup))]
public abstract class SceneBase : MonoBehaviour
{
	public SceneType Type;
	[BitMask(typeof(SceneGroup))]
	public SceneGroup Group;
	[BitMask(typeof(SceneSharedEle))]
	public SceneSharedEle SharedEle;
	public bool IsFullScene;
	public bool AllowPushDuplicate = false;

	public bool IsActivedLogic{get{return isActivedLogic;}}
	protected Canvas canvas;
	protected CanvasGroup canvasGroup;
	protected bool isInited = false;
	protected bool isActivedLogic = false;
	protected UIManager uiMgr;

	protected SceneInfo info;

	protected virtual void Awake()
	{
		Init();
	}

	protected virtual void _Init()
	{
		canvas = GetComponent<Canvas>();
		canvasGroup = GetComponent<CanvasGroup>();
		uiMgr = Global.Instance.UIMgr;
		canvas.overrideSorting = true;
	}

	public void Init()
	{
		if(!isInited)
		{
			isInited = true;
			_Init();
		}
	}

	public virtual void Show(SceneInfo info)
	{
		this.info = info;
		Init();
		SetInfo(info.AdditionInfo);
		SetEnableRender(true);
		OnActiveLogic();
	}
	
	public virtual void Close()
	{
		SetEnableRender(false);
		OnDeactiveLogic();
	}

	public virtual void SetEnableRender(bool isEnable)
	{
		gameObject.SetActive(isEnable);
		canvas.enabled = isEnable;
	}

	public virtual bool CanShowNow(List<SceneBase> curScene)
	{
		return true;
	}

	public virtual void SetRenderOrder(int order)
	{
		canvas.sortingOrder = order;
	}

	public virtual void SetRenderLayer(string layer)
	{
		canvas.sortingLayerName = layer;
	}

	public virtual void SetInfo(object[] infos)
	{

	}

	public virtual void OnActiveLogic()
	{
		isActivedLogic = true;
	}

	public virtual void OnDeactiveLogic()
	{
		isActivedLogic = false;
	}

	public virtual void OnButtonClose()
	{
		uiMgr.CloseScene(Type);
		if(info.OnCancelClick != null)
        {
            info.OnCancelClick();
        }
	}

	public virtual void OnButtonOK()
	{
		uiMgr.CloseScene(Type);
		if(info.OnOkClick != null)
        {
            info.OnOkClick();
        }
	}
}
