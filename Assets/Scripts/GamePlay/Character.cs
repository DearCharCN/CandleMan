using F8Framework.Launcher;
using UnityEngine;

namespace GamePlay
{
    public class Character : MonoBehaviour
    {
        [SerializeField]
        float movementSpeed = 2f;
        [SerializeField]
        float jumpForce = 4f;
        Animator fsmAnimator;

        private void Awake()
        {
            fsmAnimator = GetComponent<Animator>();
        }

        private void Update()
        {
            fsmAnimator.SetBool(CharacterFSMConst.LeftArrButton_Keep,
                Input.GetKey(KeyCode.A));
            fsmAnimator.SetBool(CharacterFSMConst.RightArrButton_Keep,
                Input.GetKey(KeyCode.D));
            fsmAnimator.SetBool(CharacterFSMConst.JumpButton_Down,
                Input.GetKeyDown(KeyCode.Space));
            fsmAnimator.SetBool(CharacterFSMConst.OnGround,
                CheckOnGround());
        }

        const float groundCheckDIs = 0.1f;

        private bool CheckOnGround()
        {
            Vector2 originLeft = new Vector2(transform.position.x, transform.position.y) + Vector2.left * new Vector2(0.5f, 0.5f);
            var hits = Physics2D.RaycastAll(originLeft, Vector2.down, groundCheckDIs);
            for (int i = 0; i < hits.Length; ++i)
            {
                var hit = hits[i];
                if (hit.collider != null && hit.collider.tag.Equals(CharacterFSMConst.GroundTag))
                {
                    return true;
                }
            }
            
            Vector2 originRight = new Vector2(transform.position.x, transform.position.y) + Vector2.right * new Vector2(0.5f, 0.5f);
            hits = Physics2D.RaycastAll(originRight, Vector2.down, groundCheckDIs);
            for (int i = 0; i < hits.Length; ++i)
            {
                var hit = hits[i];
                if (hit.collider != null && hit.collider.tag.Equals(CharacterFSMConst.GroundTag))
                {
                    return true;
                }
            }
            return false;
        }

        public float GetMovementSpeed()
        {
            return movementSpeed;
        }

        public float GetJumpForce()
        {
            return jumpForce;
        }

        private void FixedUpdate()
        {
            FF8.Message.DispatchEvent(CharacterEvent.OnFixedUpdate, this);
        }
    }

    public enum CharacterEvent
    {
        OnFixedUpdate,
    }
}