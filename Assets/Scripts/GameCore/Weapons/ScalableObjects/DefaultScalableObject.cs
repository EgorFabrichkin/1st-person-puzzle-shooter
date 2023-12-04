#nullable enable

using GameCore.Weapons.Projectiles;
using UnityEngine;
using static GameCore.Weapons.Projectiles.ScaleState;

namespace GameCore.Weapons.ScalableObjects
{
    public class DefaultScalableObject : MonoBehaviour, IScalableObject
    {
        [SerializeField] private float minScale = 0.1f;
        [SerializeField] private float maxScale = 3f;
        [SerializeField] private float scaleFactor = 0.1f;
        
        private Vector3 _currentScale;

        private void Awake()
        {
            _currentScale = transform.localScale;
        }

        public void Apply(Scaling scaling)
        {
            var currentScale = scaling.StateScale == Decrease ? _currentScale.x -= scaleFactor : _currentScale.x += scaleFactor;
            
            currentScale = Mathf.Clamp(currentScale, minScale, maxScale);

            transform.localScale = new Vector3(currentScale, currentScale, currentScale);
        }
    }
}