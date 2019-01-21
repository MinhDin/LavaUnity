using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class TutorialText
{
    public string Text;
    public float XStart;
    public float XEnd;
}

public class Game : MonoBehaviour
{
    public static Game Instance;
    public Global GameGlobal;
    public GameObject DelayInitialize;    
    //public IslandElement[] Islands;//HACK
    public GameState CurState { get { return state; } }
    [Header("Pool")]
    //public Item ItemPrefab;
    //public IncreaseFx DamageAnimPrefab;

    [Space]
    public RectTransform TargetToCollectItem;
    
    XInt secondSinceOpen;

    Dictionary<GameState, GameStateBase> dStateMgr;
    GameStateBase stateRunner;
    GameEvents gameE;
    
    GameState state;
    GameState lastState;

    float timeSinceOpen;

    public void Awake()
    {
        Instance = this;
        gameE = GameGlobal.GameE;
        //get target to collect
        Vector3[] targetToCollectCorners = new Vector3[4];
        TargetToCollectItem.GetWorldCorners(targetToCollectCorners);
        Vector3 center;
        center.x = (targetToCollectCorners[0].x + targetToCollectCorners[1].x + targetToCollectCorners[2].x + targetToCollectCorners[3].x) / 4;
        center.y = (targetToCollectCorners[0].y + targetToCollectCorners[1].y + targetToCollectCorners[2].y + targetToCollectCorners[3].y) / 4;
        center.z = 0;
        GameGlobal.CollectItemTarget = center;
        //========
        
        //init state mgr
        dStateMgr = new Dictionary<GameState, GameStateBase>();
        //dStateMgr.Add(State.ACTION, new GameStateAction(this));
        //dStateMgr.Add(State.LOADING, new GameStateLoading(this));
        //dStateMgr.Add(State.COLLECTION, new GameStateCollection(this));
        //=============

        Input.multiTouchEnabled = true;
        timeSinceOpen = 0;
        secondSinceOpen = Global.Instance.SecondSinceOpen;
        secondSinceOpen.Value = 0;
        lastState = GameState.LOADING;
        state = GameState.LOADING;
    }

    private void Start()
    {
        SwitchState(GameState.LOADING);
    }

    public void Update()
    {
        //timer
        timeSinceOpen += Time.unscaledDeltaTime;
        int convertedTimeSinceOpen = Mathf.FloorToInt(timeSinceOpen);
        if(convertedTimeSinceOpen != secondSinceOpen.Value)
        {
            secondSinceOpen.Value = convertedTimeSinceOpen;
            if(secondSinceOpen.OnValueChange != null)
            {
                secondSinceOpen.OnValueChange();
            }
        }
        //=======

        stateRunner.Update();        
    }

    public void SwitchState(GameState newstate)
    {
        lastState = state;
        state = newstate;
        if(stateRunner != null)
        {
            stateRunner.OnDeactive(newstate);
        }
        stateRunner = dStateMgr[state];
        stateRunner.OnActive(lastState);
        if(gameE.OnChangeState != null)
        {
            gameE.OnChangeState(newstate);
        }
    }

    public void ActiveDelayInitialize()
    {
        DelayInitialize.SetActive(true);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if(pauseStatus)
        {
            if(gameE.RequestSaveGame != null)
            {
                gameE.RequestSaveGame();
            }
        }
    }

    private void OnApplicationQuit()
    {
        if(gameE.RequestSaveGame != null)
        {
            gameE.RequestSaveGame();
        }
    }
}


