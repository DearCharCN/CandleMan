using F8Framework.Core;
using F8Framework.Launcher;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// UI的定义，枚举
public enum UIID
{
    UIMain = 1, // 游戏主界面
    UILevel = 2,// 关卡界面
    UILevelSelect = 3,//关卡选择
    UIJiesuan = 4,//关卡结算
    UIDialog = 5,//对话界面
    UIStory = 6,//黑幕剧情
}
public class UIMgr
{
    static private Dictionary<UIID, UIConfig> configs = new Dictionary<UIID, UIConfig>
    {
        // 兼容int和枚举作为Key
        { UIID.UIMain, new UIConfig(LayerType.UI, "UIMain") }, // 手动添加UI配置
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