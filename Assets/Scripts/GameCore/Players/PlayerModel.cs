using System;

namespace GameCore.Players
{
    [Serializable]
    public class PlayerModel
    {
        public float MovementSpeed  = 5f;
        public float Sensitivity  = 20f;
        public float Gravity  = -9.81f;
        public float JumpHeight  = 2f;
        public float CheckGroundRadius { get; private set; } = 0.4f;
    }
}