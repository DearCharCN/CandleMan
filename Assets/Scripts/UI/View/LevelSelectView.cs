using UnityEngine;
using F8Framework.Core;
using F8Framework.Launcher;
using UI;
using UnityEngine.EventSystems;

public class LevelSelectView : BaseView
{
    [SerializeField]
    ImageSwitch iconSwitch;
    [SerializeField] 
    TMPro.TMP_Text levelTxt;

    int passLevel;

    int currentShowLv = 1;
    int maxLevel;
    // Awake
    protected override void OnAwake()
    {
        btn_left_btn.AddButtonClickListener(OnClickLeft);
        btn_right_btn.AddButtonClickListener(OnClickRight);
        btn_enter_btn.AddButtonClickListener(OnClickEnter);
    }

    // 参数传入，每次打开UI都会执行
    protected override void OnAdded(int uiId, object[] args = null)
    {
        passLevel = FF8.Storage.GetInt(StorageKey.LEVEL_PASS_KEY);
        currentShowLv = 1;
        maxLevel = LevelSceneMgr.GetMaxLevel();
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
    [SerializeField] private UnityEngine.UI.Button btn_left_btn;
    [SerializeField] private UnityEngine.UI.Button btn_right_btn;
    [SerializeField] private UnityEngine.UI.Button btn_enter_btn;
    [SerializeField] private TMPro.TMP_Text TextTMP_TextTMP;

#if UNITY_EDITOR
    protected override void SetComponents()
    {
        btn_left_btn = transform.Find("btn_left").GetComponent<UnityEngine.UI.Button>();
        btn_right_btn = transform.Find("btn_right").GetComponent<UnityEngine.UI.Button>();
        btn_enter_btn = transform.Find("btn_enter").GetComponent<UnityEngine.UI.Button>();
        TextTMP_TextTMP = transform.Find("btn_enter/Text (TMP)").GetComponent<TMPro.TMP_Text>();
    }
#endif
    // 自动获取组件（自动生成，不能删除）

    private void CLoseSelf()
    {
        FF8.UI.Close(UIID.UILevelSelect);
    }

    private void OnClickEnter(BaseEventData baseEventData)
    {
        bool curLevelUnlock = CheckUnLock();
        if (curLevelUnlock)
        {
            LevelSceneMgr.LoadScene(currentShowLv);
            CLoseSelf();
        }
    }

    private void OnClickLeft(BaseEventData baseEventData)
    {
        currentShowLv = Mathf.Clamp(currentShowLv - 1, 1, maxLevel + 1);
        UpdateUI();
    }

    private void OnClickRight(BaseEventData baseEventData)
    {
        currentShowLv = Mathf.Clamp(currentShowLv + 1, 1, maxLevel + 1);
        UpdateUI();
    }

    private void UpdateUI()
    {
        bool isFirst = currentShowLv <= 1;
        bool isLast = currentShowLv >= maxLevel;
        btn_left_btn.gameObject.SetActive(!isFirst);
        btn_right_btn.gameObject.SetActive(!isLast);
        bool curLevelUnlock = CheckUnLock();
        iconSwitch.Switch(curLevelUnlock ? 0 : 1);
        TextTMP_TextTMP.text = curLevelUnlock ? "确认" : "待解锁";
        TextTMP_TextTMP.color = curLevelUnlock ? Color.white : new Color(0.196f, 0.196f, 0.196f, 1f);
        levelTxt.text = curLevelUnlock ? currentShowLv.ToString("00") : string.Empty;
    }

    private bool CheckUnLock()
    {
#if UNITY_EDITOR
        return true;
#endif
        return currentShowLv <= (passLevel + 1);
    }
}