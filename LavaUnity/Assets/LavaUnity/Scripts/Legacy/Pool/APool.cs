using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APool
{
	Stack<IPoolable> NotUsing;

	int size;
	int curSize;//optimize speed
	IPoolable prefab;

	public APool(IPoolable prefab, int size)
	{
		this.size = size;
		curSize = size;
		this.prefab = prefab;

		NotUsing = new Stack<IPoolable>(size);
		
		for(int i = 0; i < size; ++i)
		{
			IPoolable obj = GameObject.Instantiate<GameObject>(prefab.GetGameObject()).GetComponent<IPoolable>();
			obj.Init();
			NotUsing.Push(obj);
		}
	}

	public IPoolable GetObject()
	{
		if(curSize > 0)
		{
			curSize--;
			IPoolable obj = NotUsing.Pop();
			obj.OnOutOfPool();
			return obj;
		}
		else
		{
			IPoolable obj = GameObject.Instantiate<GameObject>(prefab.GetGameObject()).GetComponent<IPoolable>();
			obj.OnReturnToPool();
			
			size++;

			obj.OnOutOfPool();
			return obj;
		}
	}

	public void ReturnObject(IPoolable obj)
	{
		curSize++;
		obj.OnReturnToPool();
		
		NotUsing.Push(obj);
	}
}


public interface IPoolable
{
	void OnReturnToPool();
	void OnOutOfPool();
	void Init();
	GameObject GetGameObject();
}