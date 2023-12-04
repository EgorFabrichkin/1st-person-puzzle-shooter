using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameCore.Players.Inputs.KeyboardAndMouseInputs
{
    public class KeyboardInput : MonoBehaviour
    {
        private IMovable _movable;
        private IActionable _actionable;
        private InputMap _inputMap;

        #region MONO

        private void Awake()
        {
            _inputMap = new InputMap();
            _inputMap.Enable();

            _movable = GetComponent<IMovable>();
            _actionable = GetComponent<IActionable>();
            
            if (_movable == null)
            {
                throw new Exception($"There is no IMovable component on the object: {gameObject.name}");
            }
            
            if (_actionable == null)
            {
                throw new Exception($"There is no IActionable component on the object: {gameObject.name}");
            }
        }

        private void OnEnable()
        {
            _inputMap.Gameplay.Jump.performed += OnJumpPerformed;
            _inputMap.Gameplay.MouseDelta.performed += OnMouseDeltaPerformed;
            _inputMap.Gameplay.Decrease.performed += OnDecreasePerformed;
            _inputMap.Gameplay.Increase.performed += OnIncreasePerformed;
        }

        private void OnDisable()
        {
            _inputMap.Gameplay.Jump.performed -= OnJumpPerformed;
            _inputMap.Gameplay.Decrease.performed -= OnDecreasePerformed;
            _inputMap.Gameplay.Increase.performed -= OnIncreasePerformed;
        }
        
        #endregion
        
        private void Update()
        {
            ReadMove();
        }
        
        private void OnMouseDeltaPerformed(InputAction.CallbackContext context)
        {
            var delta = context.ReadValue<Vector2>();
            _movable.Rotation(delta);
        }

        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            _movable.Jump();
        }
        
        private void OnDecreasePerformed(InputAction.CallbackContext context)
        {
            _actionable.Shoot(context.phase == InputActionPhase.Performed);
        }

        private void OnIncreasePerformed(InputAction.CallbackContext context)
        {
            _actionable.ChangeScaleState();
        }

        private void ReadMove()
        {
            var inputDirection = _inputMap.Gameplay.Movement.ReadValue<Vector2>();
            var direction = new Vector3(inputDirection.x, 0, inputDirection.y);
            
            _movable.Move(direction);
        }
    }
}