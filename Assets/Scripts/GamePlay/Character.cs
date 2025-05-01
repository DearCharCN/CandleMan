using F8Framework.Launcher;
using UnityEngine;
using static F8Framework.Core.Util;

namespace GamePlay
{
    public class Character : MonoBehaviour
    {
        [SerializeField]
        float movementSpeed = 2f;
        [SerializeField]
        float jumpForce = 4f;
        [Tooltip("燃烧的单位时间（长度为1时需要n秒烧完）")]
        [SerializeField]
        float burningTime = 10f;
        [SerializeField]
        float length;

        Animator fsmAnimator;
        CandleBody candleBody;

        private void OnValidate()
        {
#if UNITY_EDITOR
            GetComponent<CandleBody>().SetLength(length);
#endif
        }

        private void Awake()
        {
            fsmAnimator = GetComponent<Animator>();
            candleBody = GetComponent<CandleBody>();
        }

        public bool IsDead => runTimeLength <= 0;

        float runTimeLength;
        private void Start()
        {
            runTimeLength = length;
            UpdateLengthRender();
        }

        private void Update()
        {
            UpdateBurning();

            fsmAnimator.SetBool(CharacterFSMConst.LeftArrButton_Keep,
                IsDead?false: Input.GetKey(KeyCode.A));
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

        private void UpdateBurning()
        {
            float needBurningLen = UnityEngine.Time.deltaTime * (1f / burningTime);
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