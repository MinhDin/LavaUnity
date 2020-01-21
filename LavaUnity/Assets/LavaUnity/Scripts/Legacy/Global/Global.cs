using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public partial class Global : MonoBehaviour 
{
	public static Global Instance;
	public Game CoreGame;
	public GameEvents GameE;
	public SinglePlayStatistics PlayStats;
	public GameReference GameRef;	
	public Factory GameFactory;	
	public SaveGameManager SaveGameMgr;
	public PlayDataManager PlayDataMgr;
	public UIManager UIMgr;
	public SoundManager SoundMgr;
	public FXManager FXMgr;
	public SoundConfig SoundConfig;
	public Config GameConfig;
	public FXConfig FXConfig;
	[Space]
	public SoundPlayHelper SoundPlayHlp;
	[Space]
	public GraphicRaycaster UIRootRaycaster;
	public GraphicRaycaster SharedUIRaycaster;

	private void Awake()
	{
		Instance = this;

		PlayStats = new SinglePlayStatistics();
	}
}
