using F8Framework.Core;
using F8Framework.Launcher;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// UI�Ķ��壬ö��
public enum UIID
{
    UIMain = 1, // ��Ϸ������
    UILevel = 2,// �ؿ�����
    UILevelSelect = 3,//�ؿ�ѡ��
    UIJiesuan = 4,//�ؿ�����
    UIDialog = 5,//�Ի�����
    UIStory = 6,//��Ļ����
}
public class UIMgr
{
    static private Dictionary<UIID, UIConfig> configs = new Dictionary<UIID, UIConfig>
    {
        // ����int��ö����ΪKey
        { UIID.UIMain, new UIConfig(LayerType.UI, "UIMain") }, // �ֶ����UI����
        { UIID.UILevel, new UIConfig(LayerType.UI, "UILevel") },
        { UIID.UILevelSelect, new UIConfig(LayerType.UI, "UILevelSelect") },
        { UIID.UIJiesuan, new UIConfig(LayerType.UI, "UIJiesuan") },
        { UIID.UIDialog, new UIConfig(LayerType.UI, "UIDialog") },
        { UIID.UIStory, new UIConfig(LayerType.UI, "UIStory") }
        
    };
    public static void Init()
    {
        FF8.UI.Initialize(configs);
        FF8.UI.SetCanvas(LayerType.UI, 1, "Default", RenderMode.ScreenSpaceOverlay, true);
        FF8.UI.SetCanvasScaler(LayerType.UI, CanvasScaler.ScaleMode.ScaleWithScreenSize,
            referenceResolution: new Vector2(1920, 1080), matchWidthOrHeight: 1);
        FF8ISInited = true;
    }
    static public bool FF8ISInited { get; private set; } = false;
}
public class LevelViewArg
{
    public int lvID;
}