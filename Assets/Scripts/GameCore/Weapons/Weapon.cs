#nullable enable

using GameCore.Weapons.Projectiles;
using GameCore.Weapons.ScalableObjects;
using UnityEngine;
using Utils;

namespace GameCore.Weapons
{
    [RequireComponent(typeof(LineRenderer))]
    public class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private Transform shootPoint = null!;
        [SerializeField] private Camera camera = null!;
        [SerializeField] private float maxLength = 50f;
        
        private LineRenderer _lineRenderer = null!;
        
        private void Awake()
        {
            shootPoint.EnsureNotNull("ShootPoint not specified");
            _lineRenderer = GetComponent<LineRenderer>()!;
            _lineRenderer.enabled = false;
            camera = Camera.main!;
        }

        public void TryShoot(bool flag, ScaleState scaleState)
        {
            if (!flag)
            {
                Deactivate();
                return;
            }
            
            Shoot(scaleState);
        }
        
        private void Shoot(ScaleState scaleState)
        {
            _lineRenderer.enabled = true;

            if (!Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out var hitInfo, maxLength)) return;
            
            var hitPosition = hitInfo.collider ? hitInfo.point : shootPoint.position + shootPoint.forward * maxLength;
            _lineRenderer.SetPosition(0, shootPoint.position);
            _lineRenderer.SetPosition(1, hitPosition);

            if (hitInfo.collider.TryGetComponent(out IScalableObject scalableObject))
            {
                scalableObject.Apply(new Scaling(scaleState));
            }
        }

        private void Deactivate()
        {
            _lineRenderer.enabled = false;
            _lineRenderer.SetPosition(0, shootPoint.position);
            _lineRenderer.SetPosition(1, shootPoint.position);
        }
    }
}