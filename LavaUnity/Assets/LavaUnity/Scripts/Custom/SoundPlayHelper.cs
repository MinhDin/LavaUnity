using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayHelper
{
    SoundManager soundMgr;
    
    public void Init()
    {
        soundMgr = Global.Instance.SoundMgr;
    }

    ~SoundPlayHelper()
    {
    }

    
}
