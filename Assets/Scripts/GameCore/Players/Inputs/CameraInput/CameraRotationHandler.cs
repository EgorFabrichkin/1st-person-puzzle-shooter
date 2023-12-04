#nullable enable

using UnityEngine;
using Utils;

namespace GameCore.Players.Inputs.CameraInput
{
    public class CameraRotationHandler : MonoBehaviour
    {
        [SerializeField] private Transform rotationTarget = null!;
        [SerializeField] private float minVerticalAngle = -60f;
        [SerializeField] private float maxVerticalAngle = 60f;

        private float _horizontal = 0f;
        private float _vertical = 0f;

        private void Awake()
        {
            rotationTarget.EnsureNotNull("RotationCamera not specified");
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void OnRotationInputReceived(Vector2 delta, float sensitivity)
        {
            var dt = Time.deltaTime;
            _vertical -= sensitivity * delta.y * dt;
            _horizontal += sensitivity * delta.x * dt;

            _vertical = Mathf.Clamp(_vertical, minVerticalAngle, maxVerticalAngle);
            
            rotationTarget.eulerAngles = new Vector3(_vertical, _horizontal, 0f);
            transform.eulerAngles = new Vector3(0f, _horizontal, 0f);
        }
    }
}