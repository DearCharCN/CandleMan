using UnityEngine;
using F8Framework.Core;
using TMPro;
using F8Framework.Launcher;
using GamePlay;
using UnityEngine.EventSystems;

public class LevelView : BaseView
{
    [SerializeField]
    TextMeshProUGUI lvTxt;
    [SerializeField]
    GameObject deadPanel;
    // Awake
    protected override void OnAwake()
    {
        Button_Button.AddButtonClickListener(OnClickBack);
    }
    
    // 参数传入，每次打开UI都会执行
    protected override void OnAdded(int uiId, object[] args = null)
    {
        LevelViewArg arg = (LevelViewArg)args[0];
        lvTxt.text = $"关卡{arg.lvID}";
        deadPanel.SetActive(false);

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
    [SerializeField] private TMPro.TMP_Text TextTMP_TextTMP;
    [SerializeField] private UnityEngine.UI.Button Button_Button;
    [SerializeField] private TMPro.TMP_Text TextTMP_TextTMP_2;

#if UNITY_EDITOR
    protected override void SetComponents()
    {
        TextTMP_TextTMP = transform.Find("dead/bg/Text (TMP)").GetComponent<TMPro.TMP_Text>();
        Button_Button = transform.Find("dead/bg/Button").GetComponent<UnityEngine.UI.Button>();
        TextTMP_TextTMP_2 = transform.Find("dead/bg/Button/Text (TMP)").GetComponent<TMPro.TMP_Text>();
    }
#endif
    // 自动获取组件（自动生成，不能删除）


    private void OnClickBack(BaseEventData baseEventData)
    {
        LevelSceneMgr.BackToMain();
    }

    private void OnPlayerDead()
    {
        deadPanel.SetActive(true);
    }
}

