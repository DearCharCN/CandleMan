using UnityEngine;

namespace GamePlay
{
    public class RunState : CharacterStateBase
    {
        protected override void OnFixedUpdate()
        {
            if (Animator.GetBool(CharacterFSMConst.LeftArrButton_Keep))
            {
                Rigidbody.velocity = (Vector2.left * Character.GetMovementSpeed());
            }
            else if (Animator.GetBool(CharacterFSMConst.RightArrButton_Keep))
            {
                Rigidbody.velocity = (Vector2.right * Character.GetMovementSpeed());
            }
        }
    }
}