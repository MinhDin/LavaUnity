using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateAction : GameStateBase
{
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

    public override void Update()
    {
    }

    
}
