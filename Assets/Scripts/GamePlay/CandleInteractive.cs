using F8Framework.Launcher;
using GamePlay;
using UnityEngine;

public class CandleInteractive : MonoBehaviour, IInteractable
{
    [SerializeField]
    CharacterConfig characterConfig;
    [SerializeField]
    bool interactive = true;

    [SerializeField]
    GameObject interactiveUI;


    [Header("传火时触发对话开关")]
    [SerializeField]
    bool openStory;
    [Header("传火时触发的对话")]
    [TextArea]
    [SerializeField]
    string[] contents;

    private void OnValidate()
    {
#if UNITY_EDITOR
        GetComponent<CandleBody>().SetLength(characterConfig.length);
#endif
    }

    private void OnEnable()
    {
        interactiveUI.SetActive(false);
    }

    private void OnDisable()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
            return;
        if (collision.gameObject.tag == CharacterFSMConst.PlayerTag)
        {
            OnEnterPlayer();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == null)
            return;
        if (collision.gameObject.tag == CharacterFSMConst.PlayerTag)
        {
            OnExitPlayer();
        }
    }

    private void OnEnterPlayer()
    {
        if (!interactive)
            return;
        LevelSceneMgr.CurrentScene.Interactive.SetCurrentInteractiveObject(this);
    }

    private void OnExitPlayer()
    {
        if (!interactive)
            return;

        if (LevelSceneMgr.CurrentScene.Interactive.Object == this as IInteractable)
            LevelSceneMgr.CurrentScene.Interactive.SetCurrentInteractiveObject(null);
    }
    public void OnInteractiveAction()
    {
        if (openStory && (contents != null || contents.Length > 0))
        {
            FF8.UI.Open(UIID.UIDialog, new object[]{ new DialogViewArgs()
            {
                contents = contents,
            },new System.Action(OnInterac) });
        }
        else
        {
            OnInterac();
        }
    }

    private void OnInterac()
    {
        LevelSceneMgr.CurrentScene.ChangedCharacter(characterConfig, transform.position);
        FF8.GameObjectPool.Despawn(this);
    }


    public void OnInteractiveEnter()
    {
        interactiveUI.SetActive(true);
    }

    public void OnInteractiveExit()
    {
        interactiveUI.SetActive(false);
    }
    public void SetConfig(CharacterConfig config,bool interactive)
    {
        this.interactive = interactive;
        characterConfig = config;
    }
}
