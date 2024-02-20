using System;
using Map.Tiles;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Grid = Map.Grid;

namespace Managers
{
    public class CameraManager : MonoManager<CameraManager>
    {
        [SerializeField] private Vector2 borderSensitivity;
        [SerializeField] private float speed;

        public static Camera Camera => Current.camera;

        public Vector2 ViewportMousePosition { get; private set; }
        public Vector2 WorldSpaceMousePosition { get; private set; }
        
        private Camera camera;

        protected override void OnAwake()
        {
            camera = GetComponent<Camera>();
        }

        private void Update()
        {
            ViewportMousePosition = camera.ScreenToViewportPoint(Mouse.current.position.value);
            WorldSpaceMousePosition = camera.ScreenToWorldPoint(Mouse.current.position.value);
            MoveCamera(Mouse.current.position.value);
        }

        private void MoveCamera(Vector2 mousePosition)
        {
            if (!Application.isPlaying) return;
            if (mousePosition.x < 0 || mousePosition.x > Screen.width || mousePosition.y < 0 ||
                mousePosition.y > Screen.height) mousePosition = Vector2.zero;
            
            if (mousePosition == Vector2.zero) return;

            
            float xMove = 0, yMove = 0;
            if (ViewportMousePosition.x <= borderSensitivity.x)
            {
                xMove = -1;
            }
            else if (ViewportMousePosition.x >= 1 - borderSensitivity.x)
            {
                xMove = 1;
            }
            
            if (ViewportMousePosition.y <= borderSensitivity.y)
            {
                yMove = -1;
            }
            else if (ViewportMousePosition.y >= 1 - borderSensitivity.y)
            {
                yMove = 1;
            }

            camera.transform.position += (new Vector3(xMove, yMove, 0) * (Time.unscaledDeltaTime * speed));
        }
    }
}