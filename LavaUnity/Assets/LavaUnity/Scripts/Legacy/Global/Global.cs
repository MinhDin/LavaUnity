using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Global : MonoBehaviour 
{
	public const string FirstIsland = "Leaffe";

	public static Global Instance;
	public Game CoreGame;
	public TextureLibrary TexLib;
	public GameEvents GameE;
	public Camera MainCamera;
	[Space]
	public Factory GameFactory;	
	public SaveGameManager SaveGameMgr;
	public UIManager UIMgr;

	private void Awake()
	{
		Instance = this;
	}

	//public StringIntSave FindIdInSave(List<StringIntSave>)
	//public int GetSecondFromMarkedDay()
	//{
	//	return (int)System.DateTime.Now.Subtract(Global.MARKED_DAY).TotalSeconds;
	//}
}
