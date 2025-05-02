using UnityEngine;

namespace GamePlay
{
    public class IdleState : CharacterStateBase
    {

        protected override void OnEnter()
        {
            //Rigidbody.velocity = new Vector2(0, Rigidbody.velocity.y);
        }

        protected override void OnFixedUpdate()
        {
            CharacterUtils.ManualDecelerationControl(0, Character, Rigidbody);
        }
    }
}