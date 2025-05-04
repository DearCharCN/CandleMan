using UnityEngine;

namespace GamePlay
{
    public class CharacterUtils
    {
        public static void ManualDecelerationControl(float horizontalInput, Character character, Rigidbody2D rb, float factor = 1f)
        {
            // ���ı���
            float moveSpeed = character.GetMovementSpeed() * factor;
            float acceleration = 30f * factor;
            float deceleration = 30f * factor;

            float targetSpeed = horizontalInput * moveSpeed;
            float speedDiff = targetSpeed - rb.velocity.x;

            // ����/�������ȣ���ͬ����״̬��ʩ�Ӳ�ͬ��
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;

            // ʹ��Lerp��MoveTowards������
            float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, 0.9f) * Mathf.Sign(speedDiff);

            // ʩ������ForceMode2D.Force ������ƽ�����٣�
            rb.AddForce(Vector2.right * movement);
        }
    }
}