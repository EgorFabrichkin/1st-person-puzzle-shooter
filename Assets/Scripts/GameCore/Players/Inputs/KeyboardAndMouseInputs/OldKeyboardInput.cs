using System;
using UnityEngine;

namespace GameCore.Players.Inputs.KeyboardAndMouseInputs
{
    public class OldKeyboardInput : MonoBehaviour
    {
        private const float SENSITIVITY = 10f;

        private IMovable _movable;
        private IActionable _actionable;

        private void Awake()
        {
            _movable = GetComponent<IMovable>();
            _actionable = GetComponent<IActionable>();
            
            if (_movable == null && _actionable == null)
            {
                throw new Exception($"There is no IMovable and IActionable component on the object: {gameObject.name}");
            }
        }

        private void Update()
        {
            ReadMove();
            MouseDelta();
            
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();

            Shoot(Input.GetMouseButton(0));

            if (Input.GetMouseButtonDown(1))
            {
                ChangeScaleState();   
            }
        }

        private void ReadMove()
        {
            var direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _movable.Move(direction);
        }
        
        private void MouseDelta()
        {
            var delta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * SENSITIVITY;
            _movable.Rotation(delta);
        }

        private void Jump() => _movable.Jump();

        private void Shoot(bool flag) => _actionable.Shoot(flag);

        private void ChangeScaleState() => _actionable.ChangeScaleState();
    }
}