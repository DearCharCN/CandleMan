using F8Framework.Launcher;
using UnityEngine;

namespace GamePlay
{
    public class Character : MonoBehaviour
    {
        [SerializeField]
        CharacterConfig characterConfig;

        Animator fsmAnimator;
        CandleBody candleBody;
        bool isInited = false;

        private void OnValidate()
        {
#if UNITY_EDITOR
            GetComponent<CandleBody>().SetLength(characterConfig.length);
#endif
        }

        public bool IsDead => runTimeLength <= 0;

        float runTimeLength;

        private void Update()
        {
            if (!isInited) return;

            UpdateBurning();

            fsmAnimator.SetBool(CharacterFSMConst.LeftArrButton_Keep,
                IsDead ? false : Input.GetKey(KeyCode.A));
            fsmAnimator.SetBool(CharacterFSMConst.RightArrButton_Keep,
                IsDead ? false : Input.GetKey(KeyCode.D));
            fsmAnimator.SetBool(CharacterFSMConst.JumpButton_Down,
                IsDead ? false : Input.GetKeyDown(KeyCode.Space));
            fsmAnimator.SetBool(CharacterFSMConst.OnGround,
                CheckOnGround());

            if (!IsDead && Input.GetKeyDown(KeyCode.E))
            {
                LevelSceneMgr.CurrentScene.Interactive.TryTriggerObject();
            }

            if (!IsDead && Input.GetKeyDown(KeyCode.Q))
            {
                LevelSceneMgr.CurrentScene.SeparateCharacter();
            }
        }

        const float groundCheckDIs = 0.1f;

        private bool CheckOnGround()
        {
            Vector2 originLeft = new Vector2(transform.position.x, transform.position.y) + Vector2.left * new Vector2(0.25f, 0.25f);
            var hits = Physics2D.RaycastAll(originLeft, Vector2.down, groundCheckDIs);
            Debug.DrawRay(originLeft, Vector2.down);
            for (int i = 0; i < hits.Length; ++i)
            {
                var hit = hits[i];
                if (hit.collider != null && hit.collider.tag.Equals(CharacterFSMConst.GroundTag))
                {
                    return true;
                }
            }

            Vector2 originRight = new Vector2(transform.position.x, transform.position.y) + Vector2.right * new Vector2(0.25f, 0.25f);
            hits = Physics2D.RaycastAll(originRight, Vector2.down, groundCheckDIs);
            Debug.DrawRay(originRight, Vector2.down);
            for (int i = 0; i < hits.Length; ++i)
            {
                var hit = hits[i];
                if (hit.collider != null && hit.collider.tag.Equals(CharacterFSMConst.GroundTag))
                {
                    return true;
                }
            }

            Vector2 originMid = new Vector2(transform.position.x, transform.position.y);
            hits = Physics2D.RaycastAll(originMid, Vector2.down, groundCheckDIs);
            Debug.DrawRay(originMid, Vector2.down);
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

        public void Init(CharacterConfig? config)
        {
            fsmAnimator = GetComponent<Animator>();
            candleBody = GetComponent<CandleBody>();

            if (config != null)
                characterConfig = config.Value;

            runTimeLength = characterConfig.length;
            UpdateLengthRender();

            isInited = true;
        }

        public CharacterConfig GetCurConfig()
        {
            var newConfig = CharacterConfig.Clone(ref characterConfig);
            newConfig.length = runTimeLength;
            return newConfig;
        }

        public float GetMovementSpeed()
        {
            return characterConfig.movementSpeed;
        }

        public float GetJumpForce()
        {
            return characterConfig.jumpForce;
        }

        private void FixedUpdate()
        {
            FF8.Message.DispatchEvent(CharacterEvent.OnFixedUpdate, this);
        }

        private void UpdateBurning()
        {
            float needBurningLen = UnityEngine.Time.deltaTime * (1f / characterConfig.burningTime);
            float newLength = Mathf.Clamp(runTimeLength - needBurningLen, 0, float.MaxValue);
            if (runTimeLength == newLength)
                return;

            runTimeLength = newLength;
            UpdateLengthRender();

            if (IsDead)
            {
                FF8.Message.DispatchEvent(CharacterEvent.Dead);
            }
        }

        private void UpdateLengthRender()
        {
            candleBody.SetLength(runTimeLength);
        }
    }

    public enum CharacterEvent
    {
        OnFixedUpdate,
        Dead,
    }
}