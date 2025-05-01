using F8Framework.Launcher;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSceneMgr : MonoBehaviour
{
    [SerializeField]
    int lvId;
    [SerializeField]
    Camera lvCamera;

    public int Level => lvId;
    public InteractiveModule Interactive { get; private set; }
    public static LevelSceneMgr CurrentScene { get; private set; }

    private void Start()
    {
        Interactive = new InteractiveModule();
        Interactive.OnInit();

        CurrentScene = this;
        FF8.UI.Close(UIID.UILevel);
        FF8.UI.Open(UIID.UILevel, new object[]
        {
            new LevelViewArg()
            {
                lvID = lvId,
            }
        });
    }

    private void OnDestroy()
    {
        if (CurrentScene == this)
            CurrentScene = null;

        if (Interactive != null)
        {
            Interactive.OnDestroy();
            Interactive = null;
        }
        
    }

    static public void LoadScene(int level)
    {
        SceneManager.LoadScene(level.ToString());
        FF8.UI.Close(UIID.UIMain);
    }

    public void LoadNestScene()
    {
        LoadScene(Level + 1);
    }
}