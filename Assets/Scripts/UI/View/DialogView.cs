using UnityEngine;
using UnityEngine.UI;
using F8Framework.Core;
using TMPro;
using F8Framework.Launcher;
using System;

public class DialogView : BaseView
{
    [SerializeField]
    TMP_Text text;


    DialogViewArgs arg;
    Action completedCb;
    int contentIndex = 0;
    // Awake
    protected override void OnAwake()
    {
        
    }

    private void CloseSelf()
    {
        FF8.UI.Close(UIID.UIDialog);
        completedCb?.Invoke();
    }

    // 参数传入，每次打开UI都会执行
    protected override void OnAdded(int uiId, object[] args = null)
    {
        LevelSceneMgr.PauseState();

        arg = args[0] as DialogViewArgs;
        contentIndex = 0;
        completedCb = null;

        if (args.Length > 1)
            completedCb = args[1] as Action;

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
        LevelSceneMgr.ResumeState();
    }
    
    // 删除，UI关闭后调用
    protected override void OnRemoved()
    {
        
    }
    
    // 自动获取组件（自动生成，不能删除）

#if UNITY_EDITOR
    protected override void SetComponents()
    {
    }
#endif
    // 自动获取组件（自动生成，不能删除）


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnNext();
        }
    }

    private void OnNext()
    {
        ++contentIndex;
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
}

public class DialogViewArgs
{
    public string[] contents;
}