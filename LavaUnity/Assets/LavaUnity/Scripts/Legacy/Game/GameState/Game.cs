using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
<<<<<<< HEAD
using System;
=======
>>>>>>> a66b9f12cb2dbf6b01e1c2ef86560aca26b6132a

public class Game : MonoBehaviour
{
    public static Game Instance;
    public Global GameGlobal;
    public GameObject[] DelayInitialize;

    public GameState CurState { get { return state; } }
<<<<<<< HEAD
    public GameStateBase StateRunner{get{return stateRunner;}}
    Dictionary<GameState, GameStateBase> dStateMgr;

    Stack<GameState> stateStack;
=======

    Dictionary<GameState, GameStateBase> dStateMgr;

>>>>>>> a66b9f12cb2dbf6b01e1c2ef86560aca26b6132a
    GameStateBase stateRunner;
    GameState state;
    GameState lastState;

    GameEvents gameE;

<<<<<<< HEAD
    int lastDay;
    int lastSecondTickCall;

=======
>>>>>>> a66b9f12cb2dbf6b01e1c2ef86560aca26b6132a
    public void Awake()
    {
        Instance = this;
        gameE = GameGlobal.GameE;
<<<<<<< HEAD
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
=======

        //init state mgr
        dStateMgr = new Dictionary<GameState, GameStateBase>();
        dStateMgr.Add(GameState.LOADING, new GameStateLoading(this));
        dStateMgr.Add(GameState.ACTION, new GameStateAction(this));

        Input.multiTouchEnabled = true;

        lastState = GameState.LOADING;
        state = GameState.LOADING;
>>>>>>> a66b9f12cb2dbf6b01e1c2ef86560aca26b6132a
    }

    private void Start()
    {
<<<<<<< HEAD
        SwitchState(GameState.LOADINGINGAME);
=======
        SwitchState(GameState.LOADING);
>>>>>>> a66b9f12cb2dbf6b01e1c2ef86560aca26b6132a
    }

    public void Update()
    {
        stateRunner.Update();
<<<<<<< HEAD
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
=======
    }

    public void SwitchState(GameState newstate)
    {
        lastState = state;
        state = newstate;
        
        if (stateRunner != null)
        {
            stateRunner.OnDeactive(newstate);
        }
>>>>>>> a66b9f12cb2dbf6b01e1c2ef86560aca26b6132a
        
        stateRunner = dStateMgr[state];
        stateRunner.OnActive(lastState);

<<<<<<< HEAD
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
=======
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
>>>>>>> a66b9f12cb2dbf6b01e1c2ef86560aca26b6132a
    }

    private void OnApplicationPause(bool pauseStatus)
    {
<<<<<<< HEAD
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
=======
        if (pauseStatus)
        {
            if (gameE.RequestSaveGame != null)
            {
                gameE.RequestSaveGame();
            }
>>>>>>> a66b9f12cb2dbf6b01e1c2ef86560aca26b6132a
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


