using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public class JumpDownState : CharacterStateBase
    {
        protected override void OnFixedUpdate()
        {
            Move();
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

            CharacterUtils.ManualDecelerationControl(horizontalInput, Character, Rigidbody, 0.5f);
        }
    }
}


