using F8Framework.Launcher;
using UnityEngine;

public class ScreenOverExposureEffect : MonoBehaviour
{
    public Material effectMaterial;
    public Animator animator;
    [Range(0, 2)] public float threshold = 1.0f;
    [Range(0, 5)] public float intensity = 1.0f;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (effectMaterial != null)
        {
            effectMaterial.SetFloat("_Threshold", threshold);
            effectMaterial.SetFloat("_Intensity", intensity);
            Graphics.Blit(src, dest, effectMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }

    private void OnEnable()
    {
        FF8.Message.AddEventListener(EventEnum.OnChangedCharacter, OnChangedCharacter, this);
    }

    private void OnDisable()
    {
        FF8.Message.RemoveEventListener(EventEnum.OnChangedCharacter, OnChangedCharacter, this);
    }

    private void OnChangedCharacter()
    {
        animator.Play("CameraEffect");
    }
}
