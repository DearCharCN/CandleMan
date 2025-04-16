using UnityEngine;

namespace GamePlay
{
    public class jumpUpState : CharacterStateBase
    {
        protected override void OnEnter()
        {
            Rigidbody.velocity += (Vector2.up * Character.GetJumpForce());
            Debug.Log($"<color=yellow>½øÈëÌøÔ¾ {Rigidbody.velocity}</color>");
        }

        protected override void OnFixedUpdate()
        {
            if (Rigidbody.velocity.y <= 0)
                Animator.SetBool(CharacterFSMConst.Jump_Up_To_Down, true);
        }

        protected override void OnExit()
        {
            Animator.SetBool(CharacterFSMConst.Jump_Up_To_Down, false);
            Debug.Log($"<color=red>ÍË³öÌøÔ¾ {Rigidbody.velocity}</color>");
        }
    }
}