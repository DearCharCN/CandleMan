using UnityEngine;
using UnityEngine.UI;
using F8Framework.Core;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using F8Framework.Launcher;
using NodeCanvas.Tasks.Actions;

public class MainView : BaseView
{
    // Awake
    protected override void OnAwake()
    {
        btn_start_btn.AddButtonClickListener(OnClickStart);
        btn_setting_btn.AddButtonClickListener(OnClickSetting);
        btn_exit_btn.AddButtonClickListener(OnClickExit);
    }
    
    // 参数传入，每次打开UI都会执行
    protected override void OnAdded(int uiId, object[] args = null)
    {
        
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
        
    }
    
    // 删除，UI关闭后调用
    protected override void OnRemoved()
    {
        
    }

    // 自动获取组件（自动生成，不能删除）
    [SerializeField] private UnityEngine.UI.Button btn_start_btn;
    [SerializeField] private UnityEngine.UI.Button btn_setting_btn;
    [SerializeField] private UnityEngine.UI.Button btn_exit_btn;
    [SerializeField] private TMPro.TMP_Text TextTMP_TextTMP;
    [SerializeField] private TMPro.TMP_Text TextTMP_TextTMP_2;
    [SerializeField] private TMPro.TMP_Text TextTMP_TextTMP_3;

#if UNITY_EDITOR
    protected override void SetComponents()
    {
        btn_start_btn = transform.Find("btnPanel/btn_start").GetComponent<UnityEngine.UI.Button>();
        btn_setting_btn = transform.Find("btnPanel/btn_setting").GetComponent<UnityEngine.UI.Button>();
        btn_exit_btn = transform.Find("btnPanel/btn_exit").GetComponent<UnityEngine.UI.Button>();
        TextTMP_TextTMP = transform.Find("btnPanel/btn_start/Text (TMP)").GetComponent<TMPro.TMP_Text>();
        TextTMP_TextTMP_2 = transform.Find("btnPanel/btn_setting/Text (TMP)").GetComponent<TMPro.TMP_Text>();
        TextTMP_TextTMP_3 = transform.Find("btnPanel/btn_exit/Text (TMP)").GetComponent<TMPro.TMP_Text>();
    }
#endif
    // 自动获取组件（自动生成，不能删除）

    #region 事件
    private void OnClickStart(BaseEventData baseEventData)
    {
        LevelSceneMgr.LoadScene(1);
    }

    private void OnClickSetting(BaseEventData baseEventData)
    {
        LogF8.Log("未实现设置功能");
    }

    private void OnClickExit(BaseEventData baseEventData)
    {
        Application.Quit();
    }
    #endregion
}