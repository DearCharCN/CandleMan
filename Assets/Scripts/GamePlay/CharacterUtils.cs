using UnityEngine;

namespace GamePlay
{
    public class CharacterUtils
    {
        public static void ManualDecelerationControl(float horizontalInput, Character character, Rigidbody2D rb)
        {
            // 核心变量
            float moveSpeed = character.GetMovementSpeed();
            float acceleration = 15f;
            float deceleration = 10f;

            float targetSpeed = horizontalInput * moveSpeed;
            float speedDiff = targetSpeed - rb.velocity.x;

            // 加速/减速力度：不同按键状态下施加不同力
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;

            // 使用Lerp或MoveTowards都可以
            float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, 0.9f) * Mathf.Sign(speedDiff);

            // 施加力（ForceMode2D.Force 适用于平滑变速）
            rb.AddForce(Vector2.right * movement);
        }

        public static void ManualDecelerationControlOnJump(float horizontalInput, Character character, Rigidbody2D rb)
        {
            // 核心变量
            float factor = 0.5f;
            float moveSpeed = character.GetMovementSpeed() * factor;
            float acceleration = 15f * factor;
            float deceleration = 10f * factor;

            float targetSpeed = (horizontalInput * moveSpeed *0.1f) + rb.velocity.x;
            float speedDiff = targetSpeed - rb.velocity.x;

            // 加速/减速力度：不同按键状态下施加不同力
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;

            // 使用Lerp或MoveTowards都可以
            float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, 0.9f) * Mathf.Sign(speedDiff);

            // 施加力（ForceMode2D.Force 适用于平滑变速）
            rb.AddForce(Vector2.right * movement);
        }
    }
}