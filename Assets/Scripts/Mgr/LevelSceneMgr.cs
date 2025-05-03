using F8Framework.Core;
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
        TryDestroyCharacter();

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
        FF8.GameObjectPool.DespawnAllClone();
        SceneManager.LoadScene(level.ToString());
        FF8.UI.Close(UIID.UIMain);
        FF8.UI.Close(UIID.IJiesuan);
    }

    static public void BackToMain()
    {
        FF8.GameObjectPool.DespawnAllClone();
        SceneManager.LoadScene("main");
        FF8.UI.Close(UIID.UILevel);
        FF8.UI.Close(UIID.IJiesuan);
    }

    public void LoadNestScene()
    {
        LoadScene(Level + 1);
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
        var bodyIns = FF8.GameObjectPool.Spawn("body", position, Quaternion.identity);
        var body = bodyIns.GetComponent<CandleBody>();
        var bodyInter = bodyIns.GetComponent<CandleInteractive>();

        body.SetLength(config.length);
        bodyInter.SetConfig(config, interactive);
    }

    private void CreateNewCharacter(CharacterConfig config, Vector3 position)
    {
        TryDestroyCharacter();
        var chIns = FF8.GameObjectPool.Spawn("character", position, Quaternion.identity);
        var character = chIns.GetComponent<Character>();
        character.transform.position = position;
        InitCharacter(character, config);
    }

    private void TryDestroyCharacter()
    {
        if (Character != null)
        {
            FF8.GameObjectPool.Despawn(Character);
            Character = null;
        }
    }

    public void SeparateCharacter()
    {
        //分裂蜡烛
        //在当前人物位置创建Body
        var characterConfig = Character.GetCurConfig();
        if (characterConfig.length < CharacterFSMConst.MinLength)
            return;

        float halfLen = characterConfig.length / 2f;
        characterConfig.length = halfLen;
        var characterPos = Character.transform.position;
        CreateBody(characterConfig, false, characterPos);
        //将人物长度缩短一半，并矫正位置
        Character.Init(characterConfig);
        Character.transform.position = characterPos + Vector3.up * halfLen + Vector3.up * 0.1f;
    }

    public static void SetPassLevel(int level)
    {
        int oldLevel = FF8.Storage.GetInt(StorageKey.LEVEL_PASS_KEY);
        if (oldLevel < level)
        {
            FF8.Storage.SetInt(StorageKey.LEVEL_PASS_KEY, level);
            FF8.Storage.Save();
        }
    }

    public static int GetMaxLevel()
    {
        return SceneManager.sceneCountInBuildSettings - 2;
    }
}

public enum LevelEvent
{
    OnCharacterInited,
}