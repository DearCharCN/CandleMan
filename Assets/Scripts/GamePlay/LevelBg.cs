using F8Framework.Launcher;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBg : MonoBehaviour
{
    private void Awake()
    {
        FF8.Message.AddEventListener(LevelEvent.OnCharacterInited, OnCharacterInited, this);
    }

    private void OnDestroy()
    {
        FF8.Message.RemoveEventListener(LevelEvent.OnCharacterInited, OnCharacterInited, this);
    }

    private void OnCharacterInited()
    {
        
    }
}