using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Managers
{
    public class InputManager : MonoManager<InputManager>
    {
        public event Action RightMouseClicked;
        public event Action LeftMouseClicked;
        
        [SerializeField] private PlayerInput playerInput;

        private Dictionary<string, Action> inputCallbacks;

        protected override void OnAwake()
        {
            inputCallbacks = new Dictionary<string, Action>
            {
                { "LeftClick", OnLeftClick },
                { "RightClick", OnRightClick }
            };
            
            playerInput.onActionTriggered += OnActionTriggered;
        }

        private void OnActionTriggered(InputAction.CallbackContext context)
        {
            if (!context.action.WasReleasedThisFrame()) return; // TODO: ONLY CALL ON BUTTON DOWN TYPE OF ACTIONS - REMEMBER ABOUT THIS
            if (!inputCallbacks.TryGetValue(context.action.name, out var callback)) return;
            callback?.Invoke();
        }

        private void OnLeftClick()
        {
            LeftMouseClicked?.Invoke();
        }
        
        private void OnRightClick()
        {
            RightMouseClicked?.Invoke();
        }
    }
}