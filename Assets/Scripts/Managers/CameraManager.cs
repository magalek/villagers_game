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
        
        private UIInfoManager uiInfoManager;

        private Camera camera;
        
        protected override void OnAwake()
        {
            camera = GetComponent<Camera>();
            uiInfoManager = ManagerLoader.Get<UIInfoManager>();
        }

        private void LateUpdate()
        {
            MoveCamera();
        }

        private void MoveCamera()
        {
            var viewportMousePosition = (Vector2)camera.ScreenToViewportPoint(Mouse.current.position.value);
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