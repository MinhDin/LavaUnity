using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameStateBase
{
	public GameStateBase()
	{
	}

	public virtual void Update()
	{
		
	}

	//public virtual void FixUpdate()
	//{

	//}

	public virtual void OnActive(GameState lastState)
	{

	}

	public virtual void OnDeactive(GameState newState)
	{
		
	}
}
