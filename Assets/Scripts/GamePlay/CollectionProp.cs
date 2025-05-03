using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public class CollectionProp : MonoBehaviour
    {
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
            LevelSceneMgr.CurrentScene.AddCollectionCount();
        }
    }
}