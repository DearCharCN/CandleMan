using UnityEngine;

namespace GamePlay
{
    public class CharacterUtils
    {
        public static void ManualDecelerationControl(float horizontalInput, Character character, Rigidbody2D rb)
        {
            // ���ı���
            float moveSpeed = character.GetMovementSpeed();
            float acceleration = 15f;
            float deceleration = 10f;

            float targetSpeed = horizontalInput * moveSpeed;
            float speedDiff = targetSpeed - rb.velocity.x;

            // ����/�������ȣ���ͬ����״̬��ʩ�Ӳ�ͬ��
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;

            // ʹ��Lerp��MoveTowards������
            float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, 0.9f) * Mathf.Sign(speedDiff);

            // ʩ������ForceMode2D.Force ������ƽ�����٣�
            rb.AddForce(Vector2.right * movement);
        }

        public static void ManualDecelerationControlOnJump(float horizontalInput, Character character, Rigidbody2D rb)
        {
            // ���ı���
            float factor = 0.5f;
            float moveSpeed = character.GetMovementSpeed() * factor;
            float acceleration = 15f * factor;
            float deceleration = 10f * factor;

            float targetSpeed = (horizontalInput * moveSpeed *0.1f) + rb.velocity.x;
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