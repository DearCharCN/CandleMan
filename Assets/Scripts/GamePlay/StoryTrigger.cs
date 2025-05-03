using F8Framework.Launcher;
using UnityEngine;

namespace GamePlay
{
    public class StoryTrigger : MonoBehaviour
    {
        [TextArea]
        [SerializeField]
        string[] contents;

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
            if (contents == null || contents.Length == 0)
                return;
            FF8.UI.Open(UIID.UIDialog, new object[]{ new DialogViewArgs()
            {
                contents = contents,
            } });
        }
    }
}