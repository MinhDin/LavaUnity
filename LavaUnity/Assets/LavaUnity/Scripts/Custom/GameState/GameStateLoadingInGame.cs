using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateLoadingInGame : GameStateBase
{
    public static int WaitingForLoading
    {
        get
        {
            return waitingLoading;
        }
        set
        {
            waitingLoading = value;
        }
    }

    public const int MAX_ORDER_GAME_READY = 3;

    static int waitingLoading = 0;

    bool isLoaded;
    public GameStateLoadingInGame()
        : base()
    {
        waitingLoading = 0;
        isLoaded = false;
    }

    public override void Update()
    {
    }

    public override void OnActive(GameState lastState)
    {
        Global.Instance.CoreGame.StartCoroutine(LoadingCoroutine());
    }

    IEnumerator LoadingCoroutine()
    {
        //static init
        //DG.Tweening.DOTween.Init();
        //create instance
        Global.Instance.PlayStats = new SinglePlayStatistics();
        Global.Instance.SaveGameMgr = new SaveGameManager();
        Global.Instance.SoundMgr = new SoundManager();
        Global.Instance.GameFactory = new Factory();
        Global.Instance.FXMgr = new FXManager();
        Global.Instance.SoundPlayHlp = new SoundPlayHelper();

        Global.Instance.SaveGameMgr.DoLoadSavedGame();
        Global.Instance.GameFactory.Init();
        Global.Instance.CoreGame.InitGameState();
        Global.Instance.SoundMgr.Init();
        Global.Instance.UIMgr.Init();
        Global.Instance.FXMgr.Init();
        Global.Instance.SoundPlayHlp.Init();
        
        while (waitingLoading > 0)
        {
            yield return null;
        }

        //yield return new WaitForSeconds(5);
        
        for (int i = 0; i < Global.Instance.GameMgr.DelayInitialize.Length; ++i)
        {
            Global.Instance.GameMgr.ActiveDelayInitialize(i);
        }

        Global.Instance.CoreGame.PushState(GameState.ACTION);

        if (Global.Instance.GameE.OnGameReady != null)
        {
            Global.Instance.GameE.OnGameReady();
        }

        if (Global.Instance.GameE.OnGameReadyOrder != null)
        {
            for (int i = 0; i <= MAX_ORDER_GAME_READY; ++i)
            {
                Global.Instance.GameE.OnGameReadyOrder(i);
            }
        }

        isLoaded = true;
    }
}
