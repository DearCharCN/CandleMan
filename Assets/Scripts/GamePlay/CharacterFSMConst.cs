namespace GamePlay
{
    public class CharacterFSMConst
    {
        /// <summary>
        /// 右按键 持续
        /// </summary>
        public const string RightArrButton_Keep = "RightArrButton_Keep";
        /// <summary>
        /// 左按键 持续
        /// </summary>
        public const string LeftArrButton_Keep = "LeftArrButton_Keep";

        /// <summary>
        /// 跳跃键 按下
        /// </summary>
        public const string JumpButton_Down = "JumpButton_Down";

        /// <summary>
        /// 在地板上
        /// </summary>
        public const string OnGround = "OnGround";

        /// <summary>
        /// 地面 Tag
        /// </summary>
        public const string GroundTag = "Ground";

        /// <summary>
        /// 平台 Tag
        /// </summary>
        public const string MovePlatformTag = "MovePlatform";

        /// <summary>
        /// 玩家 Tag
        /// </summary>
        public const string PlayerTag = "Player";

        /// <summary>
        /// 跳跃 上升 到 下降
        /// </summary>
        public const string Jump_Up_To_Down = "Jump_Up_To_Down";


        /// <summary>
        /// 最短长度
        /// </summary>
        public const float MinLength = 0.2f;
    }
}