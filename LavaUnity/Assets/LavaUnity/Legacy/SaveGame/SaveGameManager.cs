using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
using System.Threading;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SaveGameManager
{
    static string saveGamePath = Application.persistentDataPath + "/playerSave.json";
    string saveUIPath;
    public PlayerSavePack SavedPack { get { return savedPack; } }
    PlayerSavePack savedPack;
    bool isDirty;
    bool useThreadSave;
    bool isThreadSaving;
    string stringSavedPack;

    GameEvents gameE;

    public SaveGameManager(bool useThreadSave = true)
    {
        this.useThreadSave = useThreadSave;
        this.isThreadSaving = false;
        this.isDirty = false;
        gameE = Global.Instance.GameE;
        gameE.RequestSaveGame += OnRequestSaveGame;
        gameE.OnLateUpdate += OnLateUpdate;
    }

    ~SaveGameManager()
    {
        gameE.RequestSaveGame -= OnRequestSaveGame;
        gameE.OnLateUpdate -= OnLateUpdate;
    }

    public void DoSaveGame()
    {
        if(isThreadSaving)
        {
            isDirty = true;
            return;
        }
        //BinaryFormatter bf = new BinaryFormatter();
        //Save Game
        //FileStream file = File.Open(saveGamePath, FileMode.Create);
        //Global.Instance.SavedPack.SaveData.TrashCanLastClaimTime 
        //    = DateTime.Now.Ticks;
        savedPack.SaveData.LastSaveTimeInTick = DateTime.Now.Ticks;
        stringSavedPack = JsonUtility.ToJson(savedPack);

        if (useThreadSave)
        {
            Thread parseThread = new Thread(
                new ThreadStart(ThreadSave)
            );
            parseThread.Start();
        }
        else
        {
            ThreadSave();
        }

        //bf.Serialize(file, Global.Instance.SavedPack);

        //file.Close();
        isDirty = false;
    }

    public void DoLoadSavedGame()
    {
        if (File.Exists(saveGamePath))
        {
            //BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = File.Open(saveGamePath,FileMode.OpenOrCreate);
            //Global.Instance.SavedPack = (PlayerSavePack)bf.Deserialize(file);
            //file.Close();
            savedPack = JsonUtility.FromJson<PlayerSavePack>(File.ReadAllText(saveGamePath, System.Text.Encoding.ASCII));
        }
        else
        {
            savedPack = new PlayerSavePack();
        }
    }

    void OnRequestSaveGame()
    {
        isDirty = true;
    }

    void OnLateUpdate()
    {
        if (isDirty)
        {
            DoSaveGame();
        }
    }

    void ThreadSave()
    {
        isThreadSaving = true;
        File.WriteAllText(saveGamePath, stringSavedPack, System.Text.Encoding.ASCII);
        isThreadSaving = false;
    }
#if UNITY_EDITOR
    [MenuItem("Lava/Function/ClearSave")]
    static void DoSomething()
    {
        //File.WriteAllText(saveGamePath, "", System.Text.Encoding.ASCII);
        File.Delete(saveGamePath);
        PlayerPrefs.DeleteAll();
    }
#endif
}
