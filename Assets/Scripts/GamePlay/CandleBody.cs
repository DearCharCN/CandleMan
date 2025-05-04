using F8Framework.Core;
using GamePlay;
using UnityEngine;
using UnityEngine.UIElements;

public class CandleBody : MonoBehaviour
{
    [SerializeField]
    float length;
    [SerializeField]
    float unitHeight;
    [SerializeField]
    bool adjustCollider;
    [SerializeField]
    RectTransform rootTrans;
    [SerializeField]
    RectTransform bodySpriteTrans;
    [SerializeField]
    BoxCollider2D[] boxCollider2Ds;
    [SerializeField]
    GameObject bottomTexture;

    [SerializeField]
    SpriteRenderer[] lightSprites;

    private void OnValidate()
    {
        SetLength(length, true);
    }

    public void SetLength(float length, bool forceSet = false)
    {
        if (!forceSet && length == this.length)
            return;
        this.length = length;
        var len = Mathf.Clamp(length, CharacterFSMConst.MinLength, float.MaxValue);
        rootTrans.SetSizeDeltaHeight(unitHeight * len);
        bodySpriteTrans.SetLocalScaleY(len);
        if (adjustCollider)
        {
            for (int i = 0; i < boxCollider2Ds.Length; ++i)
            {
                var boxCollider2D = boxCollider2Ds[i];
                var @cSize = boxCollider2D.size;
                @cSize.y = unitHeight * len;
                boxCollider2D.size = @cSize;
                var @offset = boxCollider2D.offset;
                @offset.y = unitHeight * len * 0.5f;
                boxCollider2D.offset = @offset;
            }
        }

        bottomTexture.SetActive(len > 0.6f);

        if (lightSprites != null)
        {
            float alpha = 1f;
            if (length < 0.6f)
            {
                alpha = length / 0.6f;
            }
            for (int i = 0; i < lightSprites.Length; ++i)
            {
                var @color = lightSprites[i].color;
                @color.a = alpha;
                lightSprites[i].color = @color;
            }
        }
    }
}
