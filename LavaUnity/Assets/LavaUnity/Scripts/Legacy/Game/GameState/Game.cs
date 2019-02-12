using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game Instance;
    public Global GameGlobal;
    public GameObject[] DelayInitialize;

    public GameState CurState { get { return state; } }

    Dictionary<GameState, GameStateBase> dStateMgr;

    GameStateBase stateRunner;
    GameState state;
    GameState lastState;

    GameEvents gameE;

    public void Awake()
    {
        Instance = this;
        gameE = GameGlobal.GameE;

        //init state mgr
        dStateMgr = new Dictionary<GameState, GameStateBase>();
        dStateMgr.Add(GameState.LOADING, new GameStateLoading(this));
        dStateMgr.Add(GameState.ACTION, new GameStateAction(this));

        Input.multiTouchEnabled = true;

        lastState = GameState.LOADING;
        state = GameState.LOADING;
    }

    private void Start()
    {
        SwitchState(GameState.LOADING);
    }

    public void Update()
    {
        stateRunner.Update();
    }

    public void SwitchState(GameState newstate)
    {
        lastState = state;
        state = newstate;
        
        if (stateRunner != null)
        {
            stateRunner.OnDeactive(newstate);
        }
        
        stateRunner = dStateMgr[state];
        stateRunner.OnActive(lastState);

        if (gameE.OnChangeState != null)
        {
            gameE.OnChangeState(newstate);
        }
    }

    public void ActiveDelayInitialize()
    {
        int len = DelayInitialize.Length;
        for (int i = 0; i < len; ++i)
        {
            DelayInitialize[i].SetActive(true);
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            if (gameE.RequestSaveGame != null)
            {
                gameE.RequestSaveGame();
            }
        }
    }

    private void OnApplicationQuit()
    {
        if (gameE.RequestSaveGame != null)
        {
            gameE.RequestSaveGame();
        }
    }
}


