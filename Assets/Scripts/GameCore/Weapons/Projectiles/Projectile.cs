using GameCore.Weapons.ScalableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace GameCore.Weapons.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    { 
        public UnityEvent onHit = new ();
        
        [SerializeField] private new Rigidbody rigidbody = null!;

        public ScaleState ScaleState { get; set; }
        public float speed = 10f;
        
        public Rigidbody Rigidbody => rigidbody ??= GetComponent<Rigidbody>()!;

        private void OnCollisionEnter(Collision col)
        {
            if (!col.collider.TryGetComponent(out IScalableObject scalableObject)) return;
            scalableObject.Apply(new Scaling(ScaleState));
            onHit?.Invoke();
        }
    }
}