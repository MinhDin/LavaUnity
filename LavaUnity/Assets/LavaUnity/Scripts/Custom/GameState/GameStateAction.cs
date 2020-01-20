using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateAction : GameStateBase
{
<<<<<<< HEAD
    UIManager uiMgr;

    public GameStateAction()
        : base()
    {
        uiMgr = Global.Instance.UIMgr;
    }

    public override void OnActive(GameState lastState)
    {
        uiMgr.PushScene(SceneType.ACTION);
    }

    public override void OnDeactive(GameState newState)
    {
       
    }
=======
	public GameStateAction(Game game)
		:base(game)
	{		
	}

	public override void Update()
	{
	}

	public override void OnActive(GameState lastState)
	{
		//UIManager.Instance.PushScene(SceneType.ACTION_PHASE);
	}

	public override void OnDeactive(GameState newState)
	{
		//UIManager.Instance.CloseScene(SceneType.ACTION_PHASE);
	}
>>>>>>> a66b9f12cb2dbf6b01e1c2ef86560aca26b6132a
}
