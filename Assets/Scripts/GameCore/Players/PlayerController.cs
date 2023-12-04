#nullable enable

using GameCore.Players.Inputs;
using GameCore.Players.Inputs.CameraInput;
using GameCore.Weapons;
using GameCore.Weapons.Projectiles;
using UnityEngine;
using Utils;

namespace GameCore.Players
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour, IMovable, IActionable
    {
        [SerializeField] private Weapon weapon = null!;
        [SerializeField] private CameraRotationHandler cameraRotationHandler = null!;
        [SerializeField] private Transform groundCheckerPivot = null!;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private PlayerModel playerModel = new ();

        private ScaleState _scaleState;
        
        private float _velocity;
        private Vector3 _direction;
        private bool _isGrounded, _currentScaleState;

        private CharacterController _controller = null!;
        
        private void Awake()
        {
            _controller = GetComponent<CharacterController>()!;
            weapon.EnsureNotNull("Weapon not specified");
            cameraRotationHandler.EnsureNotNull("CameraRotationHandler not specified");
            groundCheckerPivot.EnsureNotNull("GroundCheckerPivot not specified");
            _currentScaleState = true;
        }

        private void FixedUpdate()
        {
            _isGrounded = IsOnTheGround();
            
            if (_isGrounded && _velocity < 0)
                _velocity = -2f;

            Movement();
            DoGravity();
        }

        public void Move(Vector3 direction)
        {
            _direction = direction;
        }

        public void Rotation(Vector2 delta)
        {
            cameraRotationHandler.OnRotationInputReceived(delta, playerModel.Sensitivity);
        }

        public void Jump()
        {
            if (_isGrounded)
                _velocity = Mathf.Sqrt(playerModel.JumpHeight * -2f * playerModel.Gravity);
        }

        public void Shoot(bool value)
        {
            _scaleState = _currentScaleState ? ScaleState.Decrease : ScaleState.Increase;
            weapon.TryShoot(value, _scaleState);
        }

        public void ChangeScaleState()
        {
            _currentScaleState = !_currentScaleState;
        }

        private void Movement()
        {
            var direction = transform.right * _direction.x + transform.forward * _direction.z;
            _controller.Move(direction * (playerModel.MovementSpeed * Time.fixedDeltaTime));
        }

        private bool IsOnTheGround()
        {
            return Physics.CheckSphere(groundCheckerPivot.position, playerModel.CheckGroundRadius, groundMask);
        }
        
        private void DoGravity()
        {
            _velocity += playerModel.Gravity * Time.fixedDeltaTime;
            _controller.Move(Vector3.up * (_velocity * Time.fixedDeltaTime));
        }
    }
}