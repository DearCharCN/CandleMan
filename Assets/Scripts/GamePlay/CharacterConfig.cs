using System;
using UnityEngine;

[Serializable]
public struct CharacterConfig
{
    public float movementSpeed;
    public float jumpForce;
    [Tooltip("ȼ�յĵ�λʱ�䣨����Ϊ1ʱ��Ҫn�����꣩")]
    public float burningTime;
    public float length;

    public static CharacterConfig Clone(ref CharacterConfig config)
    {
        CharacterConfig c = new CharacterConfig();
        c.movementSpeed = config.movementSpeed;
        c.jumpForce = config.jumpForce;
        c.burningTime = config.burningTime;
        c.length = config.length;
        return c;
    }
}
