using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitHelper : MonoBehaviour 
{
	public MonoBehaviour[] WhoNeedInit;

	private void Awake()
	{
		int len = WhoNeedInit.Length;
		for(int i = 0; i < len; ++i)
		{
			INeedInit ele = WhoNeedInit[i] as INeedInit;
			if(ele != null)
			{
				ele.Init();
			}
		}
	}
}
