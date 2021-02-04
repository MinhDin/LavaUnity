using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DebugMenuUI : MonoBehaviour
{
    public GameObject CheatMenu;
    Config config{get{return Global.Instance.GameConfig;}}

    private void Awake()
    {
#if !UNITY_EDITOR
#if !ENABLE_CHEAT
        Destroy(this.gameObject);
        return;
#endif
#endif
    }

    public void OnToggle(bool active)
    {
        CheatMenu.SetActive(active);
    }

#if UNITY_EDITOR
    private void OnDestroy()
    {
    }
#endif
}
