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
	public Factory GameFactory;	
	public XInt SecondSinceOpen;
	public UIManager UIMgr;

	//Runtime Data section . Need Asign
	[HideInInspector]
	
	
	//Managers
	public SaveGameManager SaveGameMgr;
	
	//Constant
	public Vector3 CollectItemTarget;
	
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
