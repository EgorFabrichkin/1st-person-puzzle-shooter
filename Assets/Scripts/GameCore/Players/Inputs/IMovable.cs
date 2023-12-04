using UnityEngine;

namespace GameCore.Players.Inputs
{
    public interface IMovable
    {
        public void Move(Vector3 direction);

        public void Rotation(Vector2 delta);
        
        public void Jump();
    }
}