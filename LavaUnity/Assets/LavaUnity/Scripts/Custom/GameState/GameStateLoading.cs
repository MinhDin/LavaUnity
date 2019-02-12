using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateLoading : GameStateBase
{
    public const int MAX_ORDER_GAME_READY = 3;

    public GameStateLoading(Game game) : base(game)
    {
        Global.Instance.SaveGameMgr = new SaveGameManager();
        Global.Instance.GameFactory = new Factory();
    }

    public override void Update()
    {
        //start loading
        Global.Instance.SaveGameMgr.DoLoadSavedGame();

        Global.Instance.GameFactory.Init(game);

        Game.Instance.ActiveDelayInitialize();

        Global.Instance.UIMgr.Init();
        
        //done loading
        game.SwitchState(GameState.ACTION);

        if (Global.Instance.GameE.OnGameReady != null)
        {
            Global.Instance.GameE.OnGameReady();
        }

        if (Global.Instance.GameE.OnGameReadyOrder != null)
        {
            for (int i = 0; i < MAX_ORDER_GAME_READY; ++i)
            {
                Global.Instance.GameE.OnGameReadyOrder(i);
            }
        }
    }

    public override void OnActive(GameState lastState)
    {

    }
}
