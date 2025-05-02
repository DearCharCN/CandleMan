using UnityEngine;

namespace GamePlay
{
    public class RunState : CharacterStateBase
    {
        protected override void OnFixedUpdate()
        {
            float horizontalInput = 0;
            if (Animator.GetBool(CharacterFSMConst.LeftArrButton_Keep))
            {
                horizontalInput = -1f;
                //Rigidbody.velocity = (Vector2.left * Character.GetMovementSpeed());
            }
            else if (Animator.GetBool(CharacterFSMConst.RightArrButton_Keep))
            {
                horizontalInput = 1f;
                //Rigidbody.velocity = (Vector2.right * Character.GetMovementSpeed());
            }

            CharacterUtils.ManualDecelerationControl(horizontalInput, Character, Rigidbody);
        }
    }
}