using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Managers
{
    public class InputManager : MonoManager
    {
        [SerializeField] private PlayerInput playerInput;

        protected override void OnAwake()
        {
            playerInput.onActionTriggered += OnActionTriggered;
        }

        private void OnActionTriggered(InputAction.CallbackContext context)
        {
            if (context.action.name == "LeftClick")
            {
                Debug.Log(Mouse.current.position.value);
            }
        }

        private void Update()
        {
            
        }
    }
}