using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public SceneBase[] AllScenes;
    public SharedUIManager SharedUIMgr;
    Dictionary<SceneType, SceneBase> dScene;
    List<SceneInfo> requestedScenes;
    List<SceneBase> lActiveScene;

    public List<SceneBase> ActiveScenes { get { return lActiveScene; } }
    int fullSceneIndex;
    SceneInfo baseInfo;

    //
    GameEvents gameE;

    public void Init()
    {
        Instance = this;
        baseInfo = new SceneInfo(SceneType.ACTION_PHASE);
        fullSceneIndex = -1;
        int len = AllScenes.Length;
        lActiveScene = new List<SceneBase>(len * 2);
        dScene = new Dictionary<SceneType, SceneBase>(len * 2);
        requestedScenes = new List<SceneInfo>(len * 2);
        for (int i = 0; i < len; ++i)
        {
            dScene.Add(AllScenes[i].Type, AllScenes[i]);
            AllScenes[i].Init();
        }

        gameE = Global.Instance.GameE;
    }

    private void Awake()
    {
        
    }

    public void PushScene(SceneType type)
    {
        baseInfo.Type = type;
        PushScene(baseInfo);
    }

    public void PushScene(SceneInfo info)
    {
        /*
        if (lActiveScene.Count == 0)
        {
            ShowNewScene(info);
        }
        else
        {
            int index = FindTypeInCurrent(info.Type);

            if (index == -1)
            {
                ShowNewScene(info);
            }
            else
            {
                SceneBase aScene = lActiveScene[index];

                if (aScene.CanShowNow(lActiveScene))
                {
                    if(aScene.ForceCloseWhenPushSameInstance)
                    {
                        CloseAScene(index);
                        ShowAScene(aScene, info);
                    }
                    else
                    {
                        qRequestScene.Enqueue(info);
                    }
                }
                else
                {
                    qRequestScene.Enqueue(info);
                }
            }
        }
         */
        bool rs = TryPushScreen(info);
        if(!rs)
        {
            requestedScenes.Add(info);
        }
    }

    bool TryPushScreen(SceneInfo info)
    {
        if (lActiveScene.Count == 0)
        {
            ShowNewScene(info);
            return true;
        }
        else
        {
            int index = FindTypeInCurrent(info.Type);

            if (index == -1)
            {
                ShowNewScene(info);
                return true;
            }
            else
            {
                SceneBase aScene = lActiveScene[index];

                if (aScene.CanShowNow(lActiveScene))
                {
                    if(aScene.ForceCloseWhenPushSameInstance)
                    {
                        CloseAScene(index);
                        ShowAScene(aScene, info);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
    }

    public void CloseScene(SceneType type)
    {
        int index = FindTypeInCurrent(type);
        if (index != -1)
        {
            CloseAScene(index);

            for(int i = 0; i < requestedScenes.Count; ++i)
            {
                bool rs = TryPushScreen(requestedScenes[i]);
                if(rs)
                {
                    requestedScenes.RemoveAt(i);
                    i--;
                }
            }
        }
    }

    void ShowNewScene(SceneInfo info)
    {
        SceneBase aScene;
        if(!dScene.TryGetValue(info.Type, out aScene))
        {
            Debug.LogError("Cant find scene type :" + info.Type.ToString());
            return;
        }

        if (aScene.CanShowNow(lActiveScene))
        {
            ShowAScene(aScene, info);
        }
        else
        {
            requestedScenes.Add(info);
        }
    }

    void ShowAScene(SceneBase scene, SceneInfo info)
    {
        StackChangeInfo changeInfo = new StackChangeInfo();
        changeInfo.IsAdd = true;
        changeInfo.IsLast = true;
        changeInfo.Type = info.Type;

        lActiveScene.Add(scene);
        int index = lActiveScene.Count - 1;
        //update full Scene Index
        if (scene.IsFullScene)
        {
            fullSceneIndex = index;
            for (int i = 0; i < fullSceneIndex; ++i)
            {
                lActiveScene[i].SetEnableRender(false);
            }
        }

        scene.Show(info);
        scene.SetRenderOrder(index * 10);
        SharedUIMgr.ShowSharedUI(scene.SharedEle);

        if (gameE.OnUIStackChange != null)
        {
            gameE.OnUIStackChange(changeInfo);
        }
    }    

    public SceneBase FindScene(SceneType type)
    {
        int index = FindTypeInCurrent(type);
        if (index != -1)
        {
            return lActiveScene[index];
        }
        else
        {
            return null;
        }
    }

    public SceneBase FindScene(SceneGroup group)
    {
        int index = FindGroupInCurrent(group);
        if (index != -1)
        {
            return lActiveScene[index];
        }
        else
        {
            return null;
        }
    }

    void CloseAScene(int index)
    {
        StackChangeInfo info = new StackChangeInfo();
        info.IsLast = index == lActiveScene.Count - 1;
        info.IsAdd = false;

        SceneBase aScene = lActiveScene[index];
        info.Type = aScene.Type;
        aScene.Close();

        lActiveScene.RemoveAt(index);
        if (index == fullSceneIndex)
        {
            for (int i = fullSceneIndex - 1; i >= 0; --i)
            {
                lActiveScene[i].SetEnableRender(true);
                if (lActiveScene[i].IsFullScene)
                {
                    fullSceneIndex = i;
                    break;
                }
            }
        }

        if (gameE.OnUIStackChange != null)
        {
            gameE.OnUIStackChange(info);
        }
    }

    int FindTypeInCurrent(SceneType type)
    {
        int len = lActiveScene.Count;
        for (int i = 0; i < len; ++i)
        {
            if (lActiveScene[i].Type == type)
            {
                return i;
            }
        }

        return -1;
    }

    int FindGroupInCurrent(SceneGroup group)
    {
        int len = lActiveScene.Count;
        for (int i = 0; i < len; ++i)
        {
            if (lActiveScene[i].Group == group)
            {
                return i;
            }
        }

        return -1;
    }
}

public struct StackChangeInfo
{
    public bool IsAdd;//false = remove
    public bool IsLast;
    public SceneType Type;
}
