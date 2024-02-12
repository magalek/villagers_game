using System;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class CameraManager : MonoManager
    {
        [SerializeField] private Vector2 borderSensitivity;
        [SerializeField] private float speed;

        public static Camera Camera => ManagerLoader.Get<CameraManager>().camera;
        
        private Camera camera;
        
        protected override void OnAwake()
        {
            camera = GetComponent<Camera>();
            //camera.transform.position = Vector3.zero + (Vector3.back * 10);
        }

        private void Update()
        {
            MoveCamera();
        }

        private void MoveCamera()
        {
            if (!Application.isPlaying) return;
            var mousePosition = Mouse.current.position.value;
            if (mousePosition == Vector2.zero) return;
            //if (mousePosition.x < 0 || mousePosition.x > Screen.width || mousePosition.y < 0 || mousePosition.y > Screen.height) return;
            var viewportMousePosition = (Vector2)camera.ScreenToViewportPoint(mousePosition);
            float xMove = 0, yMove = 0;
            if (viewportMousePosition.x <= borderSensitivity.x)
            {
                xMove = -1;
            }
            else if (viewportMousePosition.x >= 1 - borderSensitivity.x)
            {
                xMove = 1;
            }
            
            if (viewportMousePosition.y <= borderSensitivity.y)
            {
                yMove = -1;
            }
            else if (viewportMousePosition.y >= 1 - borderSensitivity.y)
            {
                yMove = 1;
            }

            camera.transform.position += (new Vector3(xMove, yMove, 0) * (Time.unscaledDeltaTime * speed));
        }
    }
}