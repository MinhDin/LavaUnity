using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InitHelper))]
public class InitHelperEditor : Editor 
{
	InitHelper helper;

	private void OnEnable()
	{
		helper = (InitHelper)target;
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if(GUI.changed)
		{
			int len = helper.WhoNeedInit.Length;
			for(int i = 0; i < len; ++i)
			{
				INeedInit init = helper.WhoNeedInit[i] as INeedInit;
				if(init == null)
				{
					helper.WhoNeedInit[i] = (MonoBehaviour) helper.WhoNeedInit[i].GetComponent<INeedInit>();
				}
			}
		}
	}
}
