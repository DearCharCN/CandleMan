using TMPro;

using UnityEngine;
using UnityEngine.UI;
namespace UI
{
    public class ImageSwitch : MonoBehaviour
    {
        public Image m_image;
        public Sprite[] spriteArray;
        public Color[] imageColorArray;

        public TMP_Text targetTmpText;
        public Color[] textColorArray;
        public GameObject[] GameObjectArray;

        public int curIndex = -1;

        public void Switch(int index, bool setNativeSize = false)
        {
            if (m_image != null)
            {
                if (spriteArray != null && index < spriteArray.Length && index >= 0)
                {
                    m_image.sprite = spriteArray[index];
                    if (setNativeSize)
                    {
                        m_image.SetNativeSize();
                    }
                }
                if (imageColorArray != null && index < imageColorArray.Length && index >= 0)
                    m_image.color = imageColorArray[index];
            }

            curIndex = index;
            if (targetTmpText != null)
            {
                if (textColorArray != null && index < textColorArray.Length && index >= 0)
                    targetTmpText.color = textColorArray[index];
            }

            if (GameObjectArray != null)
            {
                for (int i = 0; i < GameObjectArray.Length; i++)
                {
                    bool isActive = i == index;
                    GameObjectArray[i].SetActive(isActive);
                }
            }

        }

        public void SetActive(bool active)
        {
            m_image.gameObject.SetActive(active);
            if (targetTmpText != null)
                targetTmpText.gameObject.SetActive(active);
        }
    }
}
