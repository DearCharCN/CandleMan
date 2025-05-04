using F8Framework.Launcher;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay
{
    
}

public class HurtBg : MonoBehaviour
{
    [SerializeField]
    CanvasGroup hurtImg;
    private void Update()
    {
        hurtImg.alpha = GetAlpha();
    }
    float thresold = 0.15f;
    private float GetAlpha()
    {
        if (LevelSceneMgr.CurrentScene == null || LevelSceneMgr.CurrentScene.Character == null)
            return 0f;
        var character = LevelSceneMgr.CurrentScene.Character;
        float length = character.RunTimeLength;
        return 1f - Mathf.Clamp01(length / thresold);
    }
}
