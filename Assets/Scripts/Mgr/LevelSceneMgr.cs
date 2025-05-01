using F8Framework.Launcher;
using GamePlay;
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

    public Character Character { get; private set; }

    private void Start()
    {
        Interactive = new InteractiveModule();
        Interactive.OnInit();
        InitCharacter(FindAnyObjectByType<Character>());

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
        TryDewstroyCharacter();

        if (Interactive != null)
        {
            Interactive.OnDestroy();
            Interactive = null;
        }

        if (CurrentScene == this)
            CurrentScene = null;
    }

    static public void LoadScene(int level)
    {
        SceneManager.LoadScene(level.ToString());
        FF8.UI.Close(UIID.UIMain);
    }

    static public void BackToMain()
    {
        SceneManager.LoadScene("main");
        FF8.UI.Close(UIID.UILevel);
    }

    public void LoadNestScene()
    {
        LoadScene(Level + 1);
    }

    private void TryDewstroyCharacter()
    {
        if (Character != null)
        {
            Destroy(Character.gameObject);
            Character = null;
        }
    }

    private void InitCharacter(Character character, CharacterConfig? config = null)
    {
        Character = character;
        Character.Init(config);
        FF8.Message.DispatchEvent(LevelEvent.OnCharacterInited);
    }

    public void ChangedCharacter(CharacterConfig config, Vector3 position)
    {
        CreateBody(Character.GetCurConfig(), false, Character.transform.position);
        CreateNewCharacter(config, position);
    }

    private void CreateBody(CharacterConfig config, bool interactive, Vector3 position)
    {
        var bodyGo = FF8.Asset.Load("body") as GameObject;
        var bodyIns = GameObject.Instantiate(bodyGo, position: position, rotation: Quaternion.identity);
        var body = bodyIns.GetComponent<CandleBody>();
        var bodyInter = bodyIns.GetComponent<CandleInteractive>();

        body.SetLength(config.length);
        bodyInter.SetConfig(config, interactive);
    }

    private void CreateNewCharacter(CharacterConfig config, Vector3 position)
    {
        TryDestroyCharacter();
        var characterGo = FF8.Asset.Load("character") as GameObject;
        var chIns = GameObject.Instantiate(characterGo, position: position, rotation: Quaternion.identity);
        var character = chIns.GetComponent<Character>();
        character.transform.position = position;
        InitCharacter(character, config);
    }

    private void TryDestroyCharacter()
    {
        if (Character != null)
        {
            Destroy(Character.gameObject);
            Character = null;
        }
    }
}

public enum LevelEvent
{
    OnCharacterInited,
}