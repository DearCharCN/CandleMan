using F8Framework.Launcher;
using UnityEngine;

namespace GamePlay
{
    public class CharacterStateBase : StateMachineBehaviour
    {
        protected Character Character
        {
            get
            {
                return m_character;
            }
        }
        Character m_character;

        protected Animator Animator
        {
            get
            {
                return m_animator;
            }
        }

        Animator m_animator;

        protected Rigidbody2D Rigidbody
        {
            get
            {
                return m_rigidbody;
            }
        }

        Rigidbody2D m_rigidbody;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_character = animator.GetComponent<Character>();
            m_animator = animator;
            m_rigidbody = animator.GetComponent<Rigidbody2D>();
            OnEnter();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            OnExit();
            m_rigidbody = null;
            m_animator = null;
            m_character = null;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            OnUpdate();
        }

        protected virtual void OnEnter()
        {
            
        }

        protected virtual void OnExit()
        {

        }

        protected virtual void OnUpdate()
        {

        }

        protected virtual void OnFixedUpdate()
        {

        }

        private void OnEnable()
        {
            FF8.Message.AddEventListener(CharacterEvent.OnFixedUpdate, OnCharacterFixedUpdate, this);
        }

        private void OnDisable()
        {
            FF8.Message.RemoveEventListener(CharacterEvent.OnFixedUpdate, OnCharacterFixedUpdate, this);
        }

        private void OnCharacterFixedUpdate(object[] args)
        {
            if (args != null && args.Length > 0 && args[0] != null && args[0].Equals(Character))
            {
                OnFixedUpdate();
            }
        }
    }
}