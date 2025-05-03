using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using F8Framework.Core;
using F8Framework.Launcher;
using TMPro;

public class UIStoryView : BaseView
{
    [SerializeField]
    TMP_Text text;

    UIStoryViewArg arg;
    int contentIndex = 0;

    // Awake
    protected override void OnAwake()
    {
        
    }
    
    // 参数传入，每次打开UI都会执行
    protected override void OnAdded(int uiId, object[] args = null)
    {
        LevelSceneMgr.PauseState();

        arg = args[0] as UIStoryViewArg;
        contentIndex = 0;
        UpdateUI();
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
        LevelSceneMgr.ResumeState();
    }

    // 自动获取组件（自动生成，不能删除）
    [SerializeField] private TMPro.TMP_Text TextTMP_TextTMP;

#if UNITY_EDITOR
    protected override void SetComponents()
    {
        TextTMP_TextTMP = transform.Find("layout/pressTIps/Text (TMP)").GetComponent<TMPro.TMP_Text>();
    }
#endif
    // 自动获取组件（自动生成，不能删除）

    private void CloseSelf()
    {
        FF8.UI.Close(UIID.UIStory);
    }

    private void OnNext()
    {
        contentIndex++;
        if (contentIndex >= arg.contents.Length)
        {
            CloseSelf();
        }
        else
        {
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        text.text = arg.contents[contentIndex];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnNext();
        }
    }
}

public class UIStoryViewArg
{
    public string[] contents;
}