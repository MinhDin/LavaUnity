using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDataManager
{
    const string KEY_SESSION_COUNT = "sessionCount";
    const string KEY_PURCHASE_IAP_COUNT = "KEY_PURCHASE_IAP_COUNT";

    public int SessionCount{get{return sessionCount;}}
    int sessionCount = 0;
    public int PurchaseIAPTime{get{return purchaseIAPTime;}}
    int purchaseIAPTime = 0;

    const string KEY_COMPLETED_PICTURE_COUNT = "KEY_COMPLETED_PICTURE_COUNT";
    public int CompletedPictureCount{get{return completedPictureCount;}}
    int completedPictureCount = 0;

    GameEvents gameE;

    public PlayDataManager()
    {
        
    }
    
    ~PlayDataManager()
    {
    }

    public void Init()
    {
        sessionCount = PlayerPrefs.GetInt("sessionCount", 0);
        sessionCount++;
        PlayerPrefs.SetInt("sessionCount", sessionCount);

        purchaseIAPTime = PlayerPrefs.GetInt(KEY_PURCHASE_IAP_COUNT, 0);
        completedPictureCount = PlayerPrefs.GetInt(KEY_COMPLETED_PICTURE_COUNT, 0);
    }
}
