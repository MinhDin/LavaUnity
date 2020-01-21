using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Game : MonoBehaviour
{
    public static Game Instance;
    public Global GameGlobal;
    public GameObject[] DelayInitialize;

    public GameState CurState { get { return state; } }
    public GameStateBase StateRunner{get{return stateRunner;}}
    Dictionary<GameState, GameStateBase> dStateMgr;

    Stack<GameState> stateStack;
    GameStateBase stateRunner;
    GameState state;
    GameState lastState;

    GameEvents gameE;

    int lastDay;
    int lastSecondTickCall;

    public void Awake()
    {
        Instance = this;
        gameE = GameGlobal.GameE;
        Global.Instance.GameMgr = this;
        //init state mgr
        stateStack = new Stack<GameState>(20);
        dStateMgr = new Dictionary<GameState, GameStateBase>();
        dStateMgr.Add(GameState.LOADINGINGAME, new GameStateLoadingInGame());
        
        Input.multiTouchEnabled = true;

        lastState = GameState.LOADINGINGAME;
        state = GameState.LOADINGINGAME;

        lastDay = DateTime.Now.Day;
        lastSecondTickCall = DateTime.Now.Second;
    }

    public void InitGameState()
    {
        dStateMgr.Add(GameState.ACTION, new GameStateAction());
    }

    private void Start()
    {
        SwitchState(GameState.LOADINGINGAME);
    }

    public void Update()
    {
        stateRunner.Update();
        if(gameE.OnUpdate != null)
        {
            gameE.OnUpdate();
        }
        
        DateTime now = DateTime.Now;

        if(lastDay != now.Day)
        {
            lastDay = now.Day;
            gameE.OnDayPass?.Invoke(now);
        }
        
        if(lastSecondTickCall != now.Second)
        {
            lastSecondTickCall = now.Second;
            gameE.OnSecondTick?.Invoke();
        }
    }

    //public void FixedUpdate()
    //{
        //stateRunner.FixUpdate();
    //}

    
    private void SwitchState(GameState newstate)
    {
        lastState = state;
        state = newstate;

        stateRunner?.OnDeactive(newstate);
        
        stateRunner = dStateMgr[state];
        stateRunner.OnActive(lastState);

        gameE.OnChangeState?.Invoke(newstate);
    }
    

    public void PushState(GameState newState)
    {
        stateStack.Push(newState);
        SwitchState(newState);
    }

    public void PopState(GameState newState)
    {
        if(stateStack.Count > 0)
        {
            if(stateStack.Peek() == newState)
            {
                stateStack.Pop();
                if(stateStack.Count > 0)
                {
                    SwitchState(stateStack.Peek());
                }
            }
        }
    }

    public void ActiveDelayInitialize(int index)
    {
        int len = DelayInitialize.Length;
        if (index >= len)
        {
            Debug.Log("Init out of range " + index);
            return;
        }

        DelayInitialize[index].SetActive(true);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (gameE.OnApplicationPause != null)
        {
            gameE.OnApplicationPause(pauseStatus);
        }

    }

    private void LateUpdate()
    {
        if (gameE.OnLateUpdate != null)
        {
            gameE.OnLateUpdate();
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


