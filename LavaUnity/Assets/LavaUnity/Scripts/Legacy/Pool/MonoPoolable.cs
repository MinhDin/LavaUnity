using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoPoolable : MonoBehaviour , IPoolable
{

	public virtual void Init()
	{
		
	}

	public virtual void OnReturnToPool()
	{
		gameObject.SetActive(false);
	}
	
	public virtual void OnOutOfPool()
	{
		gameObject.SetActive(true);
	}

	public virtual GameObject GetGameObject()
	{
		return gameObject;
	}
}
