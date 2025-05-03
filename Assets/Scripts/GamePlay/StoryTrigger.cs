using F8Framework.Launcher;
using UnityEngine;

namespace GamePlay
{
    public class StoryTrigger : MonoBehaviour
    {
        [TextArea]
        [SerializeField]
        string text;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision == null)
                return;
            if (collision.gameObject.tag == CharacterFSMConst.PlayerTag)
            {
                OnEnterPlayer();
            }
        }

        private void OnEnterPlayer()
        {
            gameObject.SetActive(false);
            FF8.UI.Open(UIID.UIDialog, new object[]{ new DialogViewArgs()
            {
                text = text,
            } });
        }
    }
}