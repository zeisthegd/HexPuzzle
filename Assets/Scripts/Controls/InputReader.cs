using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

using Penwyn.Tools;

namespace Penwyn.Game
{
    [CreateAssetMenu(menuName = "Util/Input Reader")]
    public class InputReader : SingletonScriptableObject<InputReader>, PlayerInput.IGameActions
    {
        public event UnityAction SpinLeftPressed;
        public event UnityAction SpinLeftCancelled;
        public event UnityAction SpinRightPressed;
        public event UnityAction SpinRightCancelled;

        public event UnityAction LeftMousePressed;
        public event UnityAction LeftMouseCancelled;
        public event UnityAction RightMousePressed;
        public event UnityAction RightMouseCancelled;

        private PlayerInput _playerInput;


        private void OnEnable()
        {
            if (_playerInput == null)
            {
                _playerInput = new PlayerInput();
                _playerInput.Game.SetCallbacks(this);
            }
            _playerInput.Game.Enable();
        }

        public void OnSpinLeft(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            InvokeContextStartedOrCancelled(context, SpinLeftPressed, SpinLeftCancelled);
        }

        public void OnSpinRight(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            InvokeContextStartedOrCancelled(context, SpinRightPressed, SpinRightCancelled);
        }

        public void OnScroll(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            Vector2 value = context.ReadValue<Vector2>();
            if (IsScrollUp(value.y) && context.started)
            {
                SpinLeftPressed?.Invoke();
            }
            if (IsScrollDown(value.y) && context.started)
            {
                SpinRightPressed?.Invoke();
            }

        }

        private bool IsScrollUp(float scrollVal)
        {
            return scrollVal > 0.1F;
        }

        private bool IsScrollDown(float scrollVal)
        {
            return scrollVal < -0.1F;
        }

        public void OnLeftMouseClick(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            InvokeContextStartedOrCancelled(context, LeftMousePressed, LeftMouseCancelled);
        }

        public void OnRightMouseClick(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            InvokeContextStartedOrCancelled(context, RightMousePressed, RightMouseCancelled);
        }

        private void InvokeContextStartedOrCancelled(UnityEngine.InputSystem.InputAction.CallbackContext context, UnityAction pressedAction, UnityAction cancelledAction)
        {
            if (context.started)
                pressedAction?.Invoke();
            if (context.canceled)
                cancelledAction?.Invoke();
        }
    }
}
