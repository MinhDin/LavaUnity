using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateAction : GameStateBase
{
	public GameStateAction(Game game)
		:base(game)
	{		
	}

	public override void Update()
	{
	}

	public override void OnActive(GameState lastState)
	{
		UIManager.Instance.PushScene(SceneType.ACTION_PHASE);
	}

	public override void OnDeactive(GameState newState)
	{
		UIManager.Instance.CloseScene(SceneType.ACTION_PHASE);
	}
}
