using UnityEngine;
using F8Framework.Core;
using TMPro;
using F8Framework.Launcher;
using GamePlay;
using UnityEngine.EventSystems;
using UI;

public class LevelView : BaseView
{
    [SerializeField]
    TextMeshProUGUI lvTxt;
    [SerializeField]
    GameObject deadPanel;
    [SerializeField]
    GameObject pausePanel;
    [SerializeField]
    ImageSwitch pauseSwitch;

    // Awake
    protected override void OnAwake()
    {
        btn_back_btn.AddButtonClickListener(OnClickBack);
        btn_backmenu_btn.AddButtonClickListener(OnClickBack);
        btn_continue_btn.AddButtonClickListener(OnClickContinue);
        btn_pause_btn.AddButtonClickListener(OnClickPause);
        btn_restart_btn.AddButtonClickListener(OnClickRestart);
        btn_restart_btn_2.AddButtonClickListener(OnClickRestart);
    }
    
    // 参数传入，每次打开UI都会执行
    protected override void OnAdded(int uiId, object[] args = null)
    {
        LevelViewArg arg = (LevelViewArg)args[0];
        lvTxt.text = $"关卡{arg.lvID}";
        deadPanel.SetActive(false);
        pausePanel.SetActive(false);
        pauseSwitch.Switch(0);
        FF8.Message.AddEventListener(CharacterEvent.Dead, OnPlayerDead, this);
    }
    
    // Start
    protected override void OnStart()
    {
        
    }
    
    protected override void OnViewTweenInit()
    {
        //transform.localScale = Vector3.one * 0.7f;
    }
    
    // 自定义打开界面动画
    protected override void OnPlayViewTween()
    {
        //transform.ScaleTween(Vector3.one, 0.1f).SetEase(Ease.Linear).SetOnComplete(OnViewOpen);
    }
    
    // 打开界面动画完成后
    protected override void OnViewOpen()
    {
        
    }
    
    // 删除之前，每次UI关闭前调用
    protected override void OnBeforeRemove()
    {
        FF8.Message.RemoveEventListener(CharacterEvent.Dead, OnPlayerDead, this);
    }
    
    // 删除，UI关闭后调用
    protected override void OnRemoved()
    {
        
    }
    
    // 自动获取组件（自动生成，不能删除）
    [SerializeField] private UnityEngine.UI.Button btn_pause_btn;
    [SerializeField] private UnityEngine.UI.Button btn_setting_btn;
    [SerializeField] private UnityEngine.UI.Image Image1_Image;
    [SerializeField] private UnityEngine.UI.Button btn_continue_btn;
    [SerializeField] private UnityEngine.UI.Button btn_restart_btn;
    [SerializeField] private UnityEngine.UI.Button btn_backmenu_btn;
    [SerializeField] private UnityEngine.UI.Button btn_restart_btn_2;
    [SerializeField] private UnityEngine.UI.Button btn_back_btn;
    [SerializeField] private TMPro.TMP_Text TextTMP_TextTMP;
    [SerializeField] private TMPro.TMP_Text TextTMP_TextTMP_2;
    [SerializeField] private TMPro.TMP_Text TextTMP_TextTMP_3;
    [SerializeField] private TMPro.TMP_Text TextTMP_TextTMP_4;
    [SerializeField] private TMPro.TMP_Text TextTMP_TextTMP_5;

#if UNITY_EDITOR
    protected override void SetComponents()
    {
        btn_pause_btn = transform.Find("page/btn_pause").GetComponent<UnityEngine.UI.Button>();
        btn_setting_btn = transform.Find("page/btn_setting").GetComponent<UnityEngine.UI.Button>();
        Image1_Image = transform.Find("stopPanel/Image (1)").GetComponent<UnityEngine.UI.Image>();
        btn_continue_btn = transform.Find("stopPanel/btn_continue").GetComponent<UnityEngine.UI.Button>();
        btn_restart_btn = transform.Find("stopPanel/btn_restart").GetComponent<UnityEngine.UI.Button>();
        btn_backmenu_btn = transform.Find("stopPanel/btn_backmenu").GetComponent<UnityEngine.UI.Button>();
        btn_restart_btn_2 = transform.Find("dead/bg/btn_restart").GetComponent<UnityEngine.UI.Button>();
        btn_back_btn = transform.Find("dead/bg/btn_back").GetComponent<UnityEngine.UI.Button>();
        TextTMP_TextTMP = transform.Find("stopPanel/btn_continue/Text (TMP)").GetComponent<TMPro.TMP_Text>();
        TextTMP_TextTMP_2 = transform.Find("stopPanel/btn_restart/Text (TMP)").GetComponent<TMPro.TMP_Text>();
        TextTMP_TextTMP_3 = transform.Find("stopPanel/btn_backmenu/Text (TMP)").GetComponent<TMPro.TMP_Text>();
        TextTMP_TextTMP_4 = transform.Find("dead/bg/btn_restart/Text (TMP)").GetComponent<TMPro.TMP_Text>();
        TextTMP_TextTMP_5 = transform.Find("dead/bg/btn_back/Text (TMP)").GetComponent<TMPro.TMP_Text>();
    }
#endif
    // 自动获取组件（自动生成，不能删除）


    private void OnClickBack(BaseEventData baseEventData)
    {
        LevelSceneMgr.BackToMain();
    }

    private void OnClickPause(BaseEventData baseEventData)
    {
        pausePanel.SetActive(true);
        pauseSwitch.Switch(1);
    }

    private void OnClickContinue(BaseEventData baseEventData)
    {
        pausePanel.SetActive(false);
        pauseSwitch.Switch(0);
    }

    private void OnPlayerDead()
    {
        deadPanel.SetActive(true);
    }

    private void OnClickRestart(BaseEventData baseEventData)
    {
        
        LevelSceneMgr.FakeLoadScene(LevelSceneMgr.CurrentScene.Level);
    }
}

