#define SAVE_BY_BINARY

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SaveGameManager
{
#if SAVE_BY_JSON
    public static string saveGamePath = Application.persistentDataPath + "/playerSave.json";
#elif SAVE_BY_BINARY
    public static string saveGamePath = Application.persistentDataPath + "/playerSave.saved";
#endif
    
    string saveUIPath;
    public PlayerSavePack SavedPack { get { return savedPack; } }
    PlayerSavePack savedPack;
    bool isDirty;
    bool isRequestSave;
    bool useThreadSave;
    bool isThreadSaving;
#if SAVE_BY_JSON
    string stringSavedPack;
#endif
    GameEvents gameE;

    public SaveGameManager(bool useThreadSave = true)
    {
        this.useThreadSave = useThreadSave;
        this.isThreadSaving = false;
        this.isDirty = false;
        this.isRequestSave = false;
        gameE = Global.Instance.GameE;
        gameE.RequestSaveGame += OnRequestSaveGame;
        gameE.MarkDirtySaveGame += OnMarkDirty;
        gameE.OnLateUpdate += OnLateUpdate;
        gameE.OnApplicationPause += OnApplicationPause;
    }

    ~SaveGameManager()
    {
        gameE.RequestSaveGame -= OnRequestSaveGame;
        gameE.MarkDirtySaveGame -= OnMarkDirty;
        gameE.OnLateUpdate -= OnLateUpdate;
        gameE.OnApplicationPause -= OnApplicationPause;
    }

    public void DoSaveGame()
    {
        if (isThreadSaving)
        {
            isDirty = true;
            return;
        }

        savedPack.SaveData.LastSaveTimeInTick = DateTime.Now.Ticks;

#if SAVE_BY_JSON
        stringSavedPack = JsonUtility.ToJson(savedPack);
#elif SAVE_BY_BINARY
#endif
#if IGNORE_SAVEGAME
        isDirty = false;
        isRequestSave = false;
        return;
#endif
        if (useThreadSave)
        {
            ThreadPool.QueueUserWorkItem(ThreadSave);
        }
        else
        {
            ThreadSave(null);
        }


        isDirty = false;
        isRequestSave = false;
    }

    public void DoLoadSavedGame()
    {
        if (File.Exists(saveGamePath))
        {

#if SAVE_BY_JSON
            savedPack = JsonUtility.FromJson<PlayerSavePack>(File.ReadAllText(saveGamePath, System.Text.Encoding.ASCII));
#elif SAVE_BY_BINARY
            using (FileStream stream = new FileStream(saveGamePath, FileMode.OpenOrCreate))
            {
                using (BinaryReader binReader = new BinaryReader(stream))
                {
                    savedPack = new PlayerSavePack(binReader);// .SaveToBinary(binReader);
                }
            }
#endif
        }
        else
        {
            savedPack = new PlayerSavePack();
        }
    }

    public void OnRequestSaveGame()
    {
        isRequestSave = true;
    }

    public void OnMarkDirty()
    {
        isDirty = true;
    }

    public void SaveIfDirty()
    {
        if (isDirty)
        {
            isRequestSave = true;
        }
    }
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            if (isDirty)
            {
                DoSaveGame();
            }
        }
    }

    void OnLateUpdate()
    {
        if (isRequestSave)
        {
            DoSaveGame();
        }
    }

    void ThreadSave(object stateInfo)
    {
        isThreadSaving = true;

#if SAVE_BY_JSON
        File.WriteAllText(saveGamePath, stringSavedPack, System.Text.Encoding.ASCII);
#elif SAVE_BY_BINARY
        using (FileStream stream = new FileStream(saveGamePath, FileMode.Create))
        {
            using (BinaryWriter binWriter = new BinaryWriter(stream))
            {
                savedPack.SaveToBinary(binWriter);
            }
        }
#endif

        isThreadSaving = false;
    }

#if UNITY_EDITOR
    [MenuItem("Lava/Function/ClearSave")]
    static void DoSomething()
    {
        File.Delete(saveGamePath);
        PlayerPrefs.DeleteAll();
    }
#endif
}
