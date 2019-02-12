using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameStateBase
{
	protected Game game;

	public GameStateBase(Game game)
	{
		this.game = game;
	}

	public virtual void Update()
	{
		
	}

	public virtual void OnActive(GameState lastState)
	{

	}

	public virtual void OnDeactive(GameState newState)
	{
		
	}
}
