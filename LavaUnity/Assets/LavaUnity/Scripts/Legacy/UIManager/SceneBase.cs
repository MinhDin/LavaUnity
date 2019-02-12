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
	public bool ForceCloseWhenPushSameInstance = false;

	protected Canvas canvas;
	protected CanvasGroup canvasGroup;
	protected bool isInited;
	protected UIManager uiMgr;

	protected virtual void Awake()
	{
		_Init();
	}

	private void _Init()
	{
		if(!isInited)
		{
			isInited = true;
			Init();
		}
	}

	public virtual void Init()
	{
		canvas = GetComponent<Canvas>();
		canvasGroup = GetComponent<CanvasGroup>();
		uiMgr = Global.Instance.UIMgr;
	}

	public virtual void Show(SceneInfo info)
	{
		SetEnableRender(true);
	}

	public virtual void Close()
	{
		SetEnableRender(false);
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

	public virtual void SetInfo(List<object> info)
	{

	}
}
