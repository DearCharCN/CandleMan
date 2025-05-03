using F8Framework.Launcher;
using GamePlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPoint : MonoBehaviour,IInteractable
{
    [SerializeField]
    GameObject interactiveUI;

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
        LevelSceneMgr.CurrentScene.Interactive.SetCurrentInteractiveObject(this);
    }

    private void OnExitPlayer()
    {
        if(LevelSceneMgr.CurrentScene.Interactive.Object == this as IInteractable)
            LevelSceneMgr.CurrentScene.Interactive.SetCurrentInteractiveObject(null);
    }

    public void OnInteractiveAction()
    {
        FF8.UI.Open(UIID.IJiesuan);
    }

    public void OnInteractiveEnter()
    {
        interactiveUI.SetActive(true);
    }

    public void OnInteractiveExit()
    {
        interactiveUI.SetActive(false);
    }
}
