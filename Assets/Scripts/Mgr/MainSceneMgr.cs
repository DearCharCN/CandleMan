using F8Framework.Launcher;
using UnityEngine;

public class MainSceneMgr : MonoBehaviour
{
    private void Start()
    {
        if (!UIMgr.FF8ISInited)
            UIMgr.Init();
        FF8.UI.Open(UIID.UIMain);
    }
}