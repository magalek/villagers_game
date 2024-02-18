using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Managers
{
    public class InputManager : MonoManager<InputManager>
    {
        [SerializeField] private PlayerInput playerInput;

        private Dictionary<string, Action> inputCallbacks;

        protected override void OnAwake()
        {
            inputCallbacks = new Dictionary<string, Action>
            {
                { "LeftClick", OnLeftClick }
            };
            
            playerInput.onActionTriggered += OnActionTriggered;
        }

        private void OnActionTriggered(InputAction.CallbackContext context)
        {
            if (!inputCallbacks.TryGetValue(context.action.name, out var callback)) return;
            callback?.Invoke();
        }

        private void OnLeftClick()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            var worldMousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            //EventSystem.current.currentSelectedGameObject
            
            Debug.Log(worldMousePosition);

            var tile = MapManager.Current.Grid.GetTile(worldMousePosition);
            if (tile == null) return;

            Debug.Log(tile);
            Debug.DrawLine(tile.transform.position, tile.transform.position + (Vector3.up * 3), Color.magenta, 2);
            tile.Click();
        }
    }
}