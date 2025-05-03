using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using F8Framework.Core;
using UnityEngine.EventSystems;

public class JiesuanView : BaseView
{
    // Awake
    protected override void OnAwake()
    {
        btn_next_btn.AddButtonClickListener(OnClickEnter);
    }
    
    // 参数传入，每次打开UI都会执行
    protected override void OnAdded(int uiId, object[] args = null)
    {
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
        
    }
    
    // 自动获取组件（自动生成，不能删除）
    [SerializeField] private UnityEngine.UI.Button btn_next_btn;
    [SerializeField] private TMPro.TMP_Text TextTMP_TextTMP;

#if UNITY_EDITOR
    protected override void SetComponents()
    {
        btn_next_btn = transform.Find("panel/btn_next").GetComponent<UnityEngine.UI.Button>();
        TextTMP_TextTMP = transform.Find("panel/btn_next/Text (TMP)").GetComponent<TMPro.TMP_Text>();
    }
#endif
    // 自动获取组件（自动生成，不能删除）

    private void UpdateUI()
    {
        bool isFinalLv = CheckIsFinalLv();
        if (isFinalLv)
        {
            TextTMP_TextTMP.text = "返回主菜单";
        }
        else
        {
            TextTMP_TextTMP.text = "下一关";
        }
    }

    private void OnClickEnter(BaseEventData baseEventData)
    {
        bool isFinalLv = CheckIsFinalLv();
        if (isFinalLv)
        {
            LevelSceneMgr.BackToMain();
        }
        else
        {
            LevelSceneMgr.SetPassLevel(LevelSceneMgr.CurrentScene.Level);
            LevelSceneMgr.CurrentScene.LoadNestScene();
        } 
    }

    private bool CheckIsFinalLv()
    {
        int maxLv = LevelSceneMgr.GetMaxLevel();
        return (LevelSceneMgr.CurrentScene.Level >= maxLv);
    }
}