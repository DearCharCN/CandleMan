using UnityEngine;

namespace GamePlay
{
    public class jumpUpState : CharacterStateBase
    {
        protected override void OnEnter()
        {
            Rigidbody.velocity += (Vector2.up * Character.GetJumpForce());
            //Debug.Log($"<color=yellow>½øÈëÌøÔ¾ {Rigidbody.velocity}</color>");
        }

        protected override void OnFixedUpdate()
        {
            Move();

            if (Rigidbody.velocity.y <= 0)
                Animator.SetBool(CharacterFSMConst.Jump_Up_To_Down, true);
        }

        protected override void OnExit()
        {
            Animator.SetBool(CharacterFSMConst.Jump_Up_To_Down, false);
            //Debug.Log($"<color=red>ÍË³öÌøÔ¾ {Rigidbody.velocity}</color>");
        }

        private void Move()
        {
            float horizontalInput = 0;
            if (Animator.GetBool(CharacterFSMConst.LeftArrButton_Keep))
            {
                horizontalInput = -1f;
            }
            else if (Animator.GetBool(CharacterFSMConst.RightArrButton_Keep))
            {
                horizontalInput = 1f;
            }

            CharacterUtils.ManualDecelerationControlOnJump(horizontalInput, Character, Rigidbody);
        }
    }
}