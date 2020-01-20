using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{
    const int BaseUIIndex = 10;
    const string SortingLayerName = "UI";
    public SceneBase[] AllScenes;
    public SharedUIManager SharedUIMgr;
    public SceneBase this[SceneType id]
    {
        get
        {
            return dScene[id];
        }
    }
    Dictionary<SceneType, SceneBase> dScene;
    List<SceneInfo> requestedScenes;
    List<SceneBase> lActiveScene;

    public List<SceneBase> ActiveScenes { get { return lActiveScene; } }
    int fullSceneIndex;
    SceneInfo baseInfo;

    //
    GameEvents gameE;
    bool isInited = false;

    public void Init()
    {
        if (!isInited)
        {
            isInited = true;
            baseInfo = new SceneInfo(SceneType.ACTION);
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
    }

    

    public void PushScene(SceneType type)
    {
        baseInfo.Type = type;
        PushScene(baseInfo);
    }

    public void PushScene(SceneType type, params object[] infos)
    {
        SceneInfo info = new SceneInfo(type, infos);
        PushScene(info);
    }

    public void PushScene(SceneInfo info)
    {
        bool rs = TryPushScreen(info);
        if (!rs)
        {
            requestedScenes.Add(info);
        }
    }

    public void PushSceneDelay(SceneInfo info, float delay)
    {
        StartCoroutine(PushSceneDelayCoroutine(info, delay));
    }
    
    IEnumerator PushSceneDelayCoroutine(SceneInfo info, float delay)
    {
        yield return new WaitForSeconds(delay);
        PushScene(info);
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
                    if (!aScene.AllowPushDuplicate)
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

            for (int i = 0; i < requestedScenes.Count; ++i)
            {
                bool rs = TryPushScreen(requestedScenes[i]);
                if (rs)
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
<<<<<<< HEAD:LavaUnity/Assets/LavaUnity/Scripts/Legacy/UIManager/UIManager.cs
        if (!dScene.TryGetValue(info.Type, out aScene))
=======
        if(!dScene.TryGetValue(info.Type, out aScene))
>>>>>>> a66b9f12cb2dbf6b01e1c2ef86560aca26b6132a:LavaUnity/Assets/LavaUnity/Scripts/Legacy/UIManager/UIManager.cs
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

        if(lActiveScene.Count > 0)
        {
            lActiveScene[lActiveScene.Count - 1].OnDeactiveLogic();
        }
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

        //before scene so scene has a chance to override
        if ((scene.SharedEle >= SceneSharedEle.NORMALSTART)
            || (scene.SharedEle == SceneSharedEle.NONE))
        {
            SharedUIMgr.ShowSharedUI(scene.SharedEle, (index) * 10 + 5, SortingLayerName);
        }

        scene.SetRenderOrder(index * 10);
        scene.SetRenderLayer(SortingLayerName);

        scene.Show(info);   

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

        if(lActiveScene.Count > 0)
        {
            SceneBase scene = lActiveScene[lActiveScene.Count - 1];
            //before scene so scene has a chance to override
            if ((scene.SharedEle >= SceneSharedEle.NORMALSTART)
                || (scene.SharedEle == SceneSharedEle.NONE))
            {
                SharedUIMgr.ShowSharedUI(scene.SharedEle, (index) * 10 + 5, SortingLayerName);
            }
            
            scene.OnActiveLogic();
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
