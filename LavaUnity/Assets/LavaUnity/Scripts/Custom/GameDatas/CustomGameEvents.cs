using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class GameEvents
{
    public Action<float> OnInputMove;//delta x percent
    
    public Action RequestRestartSong;
    public Action OnStartSong;
    public Action<float> OnSongProgress;//delta time
}
